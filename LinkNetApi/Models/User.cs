﻿using System;
namespace LinkNetApi.Models
{
    public class PostUserRes
    {
        //public int StatusCode { get; set; }
        public int statusCode { get; set; }
        public User? user { get; set; }
    }

    public class User
	{
        //public int StatusCode { get; set; }
        public Guid id { get; set; }
        public string ?username { get; set; }
        public string ?password { get; set; }
        public string ?email { get; set; }
        public string ?avatar { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }

    public class PostUser
    {
        public Guid id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
    }

    public class Login
    {
        public string? username { get; set; }
        public string? password { get; set; }
    }

    
}

