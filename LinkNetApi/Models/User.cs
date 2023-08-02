using System;
namespace LinkNetApi.Models
{
	public class User
	{
        public Guid id { get; set; }
        public string ?username { get; set; }
        public string ?password { get; set; }
        public string ?email { get; set; }
        public string ?avatar { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}

