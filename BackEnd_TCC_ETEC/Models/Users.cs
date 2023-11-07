﻿using System.ComponentModel.DataAnnotations;

namespace BackEndAplication.Models
{
    public class Users
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(100)]
        public string email { get; set; }
    }

    public class CreateUserId
    {
        public int Id { get; set;}
    }
}
