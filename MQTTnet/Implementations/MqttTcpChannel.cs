#if !WINDOWS_UWP
using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Threading;
using MQTTnet.Channel;
using MQTTnet.Client;

namespace MQTTnet.Implementations
{
    public class MqttTcpChannel : IMqttChannel
    {
        private readonly IMqttClientOptions _clientOptions;
        private readonly MqttClientTcpOptions _options;
        
        private Socket _socket;
        private Stream _stream;

        /// <summary>
        /// called on client sockets are created in connect
        /// </summary>
        public MqttTcpChannel(IMqttClientOptions clientOptions)
        {
            if (clientOptions == null) throw new ArgumentNullException(nameof(clientOptions));
            _clientOptions = clientOptions;
            _options = (MqttClientTcpOptions)clientOptions.ChannelOptions;
        }

        /// <summary>
        /// called on server, sockets are passed in
        /// connect will not be called
        /// </summary>
        public MqttTcpChannel(Socket socket, SslStream sslStream)
        {
            if (socket == null) throw new ArgumentNullException(nameof(socket));
            _socket = socket;

            CreateStream(sslStream);
        }

        [Obsolete("There is a new callback at the TLS options. This one will be deleted soon.")]
        public static Func<X509Certificate, X509Chain, SslPolicyErrors, MqttClientTcpOptions, bool> CustomCertificateValidationCallback { get; set; }

        public string Endpoint => _socket?.RemoteEndPoint?.ToString();

        public async Task ConnectAsync(CancellationToken cancellationToken)
        {
            if (_socket == null)
            {
                _socket = new Socket(SocketType.Stream, ProtocolType.Tcp) { NoDelay = true };
            }

            await Task.Factory.FromAsync(_socket.BeginConnect, _socket.EndConnect, _options.Server, _options.GetPort(), null).ConfigureAwait(false);

            SslStream sslStream = null;
            if (_options.TlsOptions.UseTls)
            {
                sslStream = new SslStream(new NetworkStream(_socket, true), false, InternalUserCertificateValidationCallback);
                await sslStream.AuthenticateAsClientAsync(_options.Server, LoadCertificates(), SslProtocols.Tls12, _options.TlsOptions.IgnoreCertificateRevocationErrors).ConfigureAwait(false);
            }

            CreateStream(sslStream);
        }

        public Task DisconnectAsync()
        {
            Dispose();
            return Task.FromResult(0);
        }

        public Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _stream.ReadAsync(buffer, offset, count, cancellationToken);
        }

        public Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return _stream.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public void Dispose()
        {
            TryDispose(_stream, () => _stream = null);
            TryDispose(_socket, () => _socket = null);
        }

        private bool InternalUserCertificateValidationCallback(object sender, X509Certificate x509Certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            // Try the instance callback.
            if (_options.TlsOptions.CertificateValidationCallback != null)
            {
                return _options.TlsOptions.CertificateValidationCallback(x509Certificate, chain, sslPolicyErrors,_clientOptions);
            }

            // Try static callback.
            if (MqttTcpChannel.CustomCertificateValidationCallback != null)
            {
                return MqttTcpChannel.CustomCertificateValidationCallback(x509Certificate, chain, sslPolicyErrors, _options);
            }

            if (sslPolicyErrors == SslPolicyErrors.None)
            {
                return true;
            }

            if (chain.ChainStatus.Any(c => c.Status == X509ChainStatusFlags.RevocationStatusUnknown || c.Status == X509ChainStatusFlags.Revoked || c.Status == X509ChainStatusFlags.RevocationStatusUnknown))
            {
                if (!_options.TlsOptions.IgnoreCertificateRevocationErrors)
                {
                    return false;
                }
            }

            if (chain.ChainStatus.Any(c => c.Status == X509ChainStatusFlags.PartialChain))
            {
                if (!_options.TlsOptions.IgnoreCertificateChainErrors)
                {
                    return false;
                }
            }

            return _options.TlsOptions.AllowUntrustedCertificates;
        }

        private X509CertificateCollection LoadCertificates()
        {
            var certificates = new X509CertificateCollection();
            if (_options.TlsOptions.Certificates == null)
            {
                return certificates;
            }

            foreach (var certificate in _options.TlsOptions.Certificates)
            {
                certificates.Add(new X509Certificate2(certificate));
            }

            return certificates;
        }

        private void CreateStream(Stream stream)
        {
            if (stream != null)
            {
                _stream = stream;
            }
            else
            {
                _stream = new NetworkStream(_socket, true);
            }
        }

        private static void TryDispose(IDisposable disposable, Action afterDispose)
        {
            try
            {
                disposable?.Dispose();
            }
            catch (ObjectDisposedException)
            {
            }
            catch (NullReferenceException)
            {
            }
            finally
            {
                afterDispose();
            }
        }
    }
}
#endif
