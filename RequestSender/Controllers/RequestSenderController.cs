using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RequestSender.Services.Models;
using RequestSender.Services.Repositories;
using TLConnectServiceReference;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RequestSender.Controllers
{
    [Route("api")]
    [ApiController]
    public class RequestSenderController : ControllerBase
    {
        private readonly ITLConnectService _tlConnectService;
        private readonly IRequestInfoRepository _requestInfoRepository;
        private ILogger _logger;

        public RequestSenderController(ITLConnectService tLConnectService, ILogger<RequestSenderController> logger, IRequestInfoRepository requestInfoRepository)
        {
            _tlConnectService = tLConnectService;
            _logger = logger;
            _requestInfoRepository = requestInfoRepository;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<string>> AddAsync([FromBody]string serializedRequest)
        {
            HotelAvailNotifRQRequest request;
            SoapReflectionImporter importer = new SoapReflectionImporter("https://www.travelline.ru/Api/TLConnect");
            var mapping = importer.ImportTypeMapping(typeof(HotelAvailNotifRQRequest));
            XmlSerializer xmlSerializer = new XmlSerializer(mapping);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                {
                    streamWriter.Write(serializedRequest);
                    streamWriter.Flush();
                    memoryStream.Position = 0;
                    request = (HotelAvailNotifRQRequest)xmlSerializer.Deserialize(memoryStream);
                }
            }

            HotelAvailNotifRQResponse response = await _tlConnectService.HotelAvailNotifRQAsync(request);
            string serializedResponse;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlSerializer.Serialize(memoryStream, response);
                serializedResponse = Encoding.ASCII.GetString(memoryStream.ToArray());
            }

            _logger.LogInformation(serializedResponse);
            _requestInfoRepository.Add(new RequestInfo {
                RequestBody = serializedRequest,
                ResponseBody = serializedResponse
            });

            return serializedResponse;
        }
    }
}
