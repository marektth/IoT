using Newtonsoft.Json;
using System;
using System.Net;

namespace ZadanieZCT.Backend
{
    public static class AWSController
    {
        private const String AWSUrl = "";

        public static EnvSensorAWS GetLatestDataFromAWS()
        {
            WebClient client = new WebClient();
            string dlString = client.DownloadString(AWSUrl);
            return JsonConvert.DeserializeObject<EnvSensorAWS>(dlString);
        }


    }
}