﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrSystemLib.Models
{
    public class Bank : Interfaces.IBank
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
