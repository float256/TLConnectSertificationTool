using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RequestSender.Services.Models
{
    public class RequestInfo
    {
        [Column("id_request_info")]
        public int Id { get; set; }

        [Column("request_body")]
        public string RequestBody { get; set; }

        [Column("response_body")]
        public string ResponseBody { get; set; }
    }
}
