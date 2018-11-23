using System.Threading.Tasks;
using Dynamsoft.TwainDirect.Cloud.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dynamsoft.TwainDirect.Cloud.Registration
{
    /// <summary>
    /// Class that handles device registration with TWAIN Cloud infrastructure.
    /// </summary>
    public class RegistrationManager
    {
        #region Private Fields

        private readonly TwainCloudClient _client;
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

    #endregion

    #region Ctors

    /// <summary>
    /// Initializes a new instance of the <see cref="RegistrationManager"/> class.
    /// </summary>
    /// <param name="client">Initialized TWAIN Cloud client.</param>
    public RegistrationManager(TwainCloudClient client)
        {
            _client = client;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initiates registration sequence for specified device.
        /// </summary>
        /// <param name="scanner">The device information to register.</param>
        /// <returns>Registration response.</returns>
        public async Task<RegistrationResponse> Register(ScannerInformation scanner)
        {
            var jsonString = JsonConvert.SerializeObject(scanner, Formatting.None, JsonSettings);
            string mediaType = "application/json";

            return await _client.Post<RegistrationResponse>("register", jsonString, mediaType);
        }

        /// <summary>
        /// Polls TWAIN Cloud infrastucture for registration completion.
        /// </summary>
        /// <param name="pollUrl">The poll URL.</param>
        /// <returns>Polling reponse.</returns>
        public async Task<PollResponse> Poll(string pollUrl)
        {
            return await _client.Get<PollResponse>(pollUrl);
        }

        #endregion
    }
}