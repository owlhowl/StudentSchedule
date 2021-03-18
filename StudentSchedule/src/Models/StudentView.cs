using System;
using System.Collections.Generic;
using System.Text;

namespace StudentSchedule
{
    public class StudentView : Student
    {
        public int Index { get; set; }
        public bool IsCheckedToday { get; set; }
        public string Name { get => $"{LastName} {FirstName} {FatherName}"; }
    }
}
