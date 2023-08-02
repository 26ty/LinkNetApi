using System;
namespace LinkNetApi.Models
{
	public class Article
	{
        public Guid id { get; set; }
        public string ?title { get; set; }
        public string ?content { get; set; }
        public Guid user_id { get; set; }
        //public User ?user { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}

