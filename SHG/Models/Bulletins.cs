using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SHG.Data;
using SHG.Models.Entites;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace SHG.Models
{
    public class Bulletins
    {
        private Config config;
        private ElectContext electContext;

        public Bulletins(Config config, ElectContext electContext)
        {
            this.config = config;
            this.electContext = electContext;
        }

        public bool saveBulletin (int voterId, string bulletinData)
        {
            if (electContext.Bulletins.SingleOrDefault(b => b.VoterId == voterId) == null)
            {
                Bulletin bulletin = new Bulletin();
                bulletin.VoterId = voterId;
                bulletin.Data = bulletinData;
                electContext.Bulletins.Add(bulletin);
                electContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool sendBulletinsToMix()
        {
            List<Bulletin> bulletins = electContext.Bulletins.ToList();
            List<Object> bulletinsToExport = new List<Object>();
            foreach (Bulletin bulletin in bulletins)
            {
                EncryptedData encryptedData = JsonConvert.DeserializeObject<EncryptedData>(bulletin.Data);
                bulletinsToExport.Add(new { data = encryptedData });
            }
            string serializedBulletins = JsonConvert.SerializeObject(bulletinsToExport, Formatting.Indented);
            if (requestToMix(serializedBulletins, config.FIRST_MIX_URL, "api/handle-bulletins").IsSuccessStatusCode)
                return true;
            return false;
            
        }

        private HttpResponseMessage requestToMix(string serializedObjectData, string baseUrl, string actionUri)
        {
            var client = new HttpClient();
            var content = new StringContent(serializedObjectData, Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client.PostAsync(actionUri, content).Result;
        }
    }
}
