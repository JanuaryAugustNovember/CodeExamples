
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAngular.Infrastructure;
using System.ServiceProcess;
using TestAngular.Domain;
using System.Runtime.Caching;
using TestAngular.Repositories;

namespace TestAngular.Controllers
{
    [Route("api/[controller]")]
    public class UtilityController : Controller
    {
        private readonly ICryptoApplication _cryptoApplication;
        private readonly IElasticRepository _elasticRepository;
        private readonly IDealerGridRepository _dealerGridRepository;
        

        public UtilityController(ICryptoApplication crypto, IElasticRepository elastic, IDealerGridRepository dealerGridRepo)
        {
            _cryptoApplication = crypto;
            _elasticRepository = elastic;
            _dealerGridRepository = dealerGridRepo;
        }

        [HttpGet("[action]")]
        public string Ping()
        {
            return "successful ping";
        }

        [HttpGet("[action]")]
        public string Decrypt(string inputText)
        {
            if (inputText.Contains(' '))
                inputText = inputText.Replace(' ', '+');

            var decryptedText = _cryptoApplication.DeryptText(inputText);

            return decryptedText;
        }

        [HttpGet("[action]")]
        public bool GetElasticStatus()
        {
            var isRunningStatus = _elasticRepository.IsElasticRunning();

            // call to _elasticApplication.GetIndices().Where(x => x.Index == ".kibana").Health
            // if (health != red) return true;

            return isRunningStatus;
        }

        [HttpGet("[action]")]
        public void StartElasticService()
        {
            ServiceController service = new ServiceController("Elasticsearch");

            try
            {
                if (service.Status != ServiceControllerStatus.Running)
                    if (service.Status == ServiceControllerStatus.Stopped || service.Status == ServiceControllerStatus.Paused)
                        service.Start();
            }
            catch(Exception exception)
            {
                throw;
                // log
            }
        }

        [HttpGet("[action]")]
        public void StopElasticService()
        {

        }

        [HttpGet("[action]")]
        public List<DealerDataSource> GetConnections(string searchText)
        {
            if (searchText == null || searchText.Trim() == "")
                throw new Exception("Must provide search text");

            var dataSources = new List<DealerDataSource>();

            var cache = DealerGridCache.RetrieveItems<DealerDataSource>(searchText);

            if (cache != null)
            {
                dataSources = cache;
            }
            else
            {
                dataSources  = _dealerGridRepository.GetConnectionStrings(searchText);

                foreach (var source in dataSources)
                    source.DecryptedPassword = _cryptoApplication.DeryptText(source.Password);

                DealerGridCache.StoreItems(dataSources, searchText);
            }

            return dataSources; 
        }
    }
}
