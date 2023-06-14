﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
