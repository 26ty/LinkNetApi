using System;
namespace LinkNetApi.Models
{
	public class Response
	{
        public int statusCode { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}

