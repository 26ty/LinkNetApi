using System;
namespace LinkNetApi.Models
{
	public class Response
	{
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}

