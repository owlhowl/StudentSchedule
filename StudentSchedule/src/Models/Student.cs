using System;
using System.Collections.Generic;

namespace StudentSchedule
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public List<DateTime> DutyList { get; set; } = new List<DateTime>();
        public string Name { get => $"{LastName} {FirstName} {FatherName}"; }
    }
}