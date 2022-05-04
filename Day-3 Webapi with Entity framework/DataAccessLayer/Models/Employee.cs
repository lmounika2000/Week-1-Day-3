﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Salary { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
