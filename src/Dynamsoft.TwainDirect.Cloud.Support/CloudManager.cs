using System.Collections.Generic;
using System.Configuration;
using Dynamsoft.TwainDirect.Cloud.Registration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Dynamsoft.TwainDirect.Cloud.Support
{
    /// <summary>
    /// 
    /// </summary>
    public class CloudManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetCloudApiRoot()
        {
            var apiRoot = ConfigurationManager.AppSettings["CloudApiRoot"];
            return apiRoot;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_scanner"></param>
        /// <returns></returns>
        public static string GetScannerCloudUrl(ScannerInformation a_scanner)
        {
            var url = $"{GetCloudApiRoot()}/scanners/{a_scanner.Id}";
            return url;
        }

        /// <summary>
        /// 
        /// </summary>
        public static JsonSerializerSettings SerializationSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }

    /// <summary>
    /// 
    /// </summary>
    public class CloudMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CloudDeviceResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int StatusCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StatusDescription { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }
    }
}
