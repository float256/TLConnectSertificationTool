using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TLConnectServiceReference;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RequestSender.Controllers
{
    [Route("api")]
    [ApiController]
    public class RequestSenderController : ControllerBase
    {
        private readonly ITLConnectService _tlConnectService;
        private ILogger _logger;

        public RequestSenderController(ITLConnectService tLConnectService, ILogger<RequestSenderController> logger)
        {
            _tlConnectService = tLConnectService;
            _logger = logger;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<string>> AddAsync(string serializedRequest)
        {
            HotelAvailNotifRQRequest request;
            XmlSerializer formatter = new XmlSerializer(typeof(HotelAvailNotifRQRequest));

            using (TextReader reader = new StringReader(serializedRequest))
            {
                request = (HotelAvailNotifRQRequest)formatter.Deserialize(reader);
            }

            HotelAvailNotifRQResponse response = await _tlConnectService.HotelAvailNotifRQAsync(request);
            string serializedResponse;

            using (StringWriter textWriter = new StringWriter())
            {
                formatter.Serialize(textWriter, response);
                serializedResponse = textWriter.ToString();
            }
            _logger.LogInformation(serializedResponse);

            return serializedResponse;
        }
    }
}
