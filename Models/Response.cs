﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserServiceHub.Models
{
    public class Response
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public int IsApporved { get; set; }
        public string Status { get; set; }
        public int TotalCnt { get; set; }
        public string Message { get; set; }
    }
}
