using System;
namespace LinkNetApi.Models
{
	public class Comment
	{
        public Guid id { get; set; }
        public string ?content { get; set; }
        public Guid user_id { get; set; }
        //public User User { get; set; }
        public Guid article_id { get; set; }
        //public Article Article { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}

