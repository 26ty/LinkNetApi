using System;
namespace LinkNetApi.Models
{
	public class Collection
	{
        public Guid id { get; set; }
        public Guid user_id { get; set; }
        public Guid article_id { get; set; }
        public DateTime created_at { get; set; }
    }
}

