using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace StudentSchedule
{
    public class StudentManager
    {
        public List<Student> Students { get; set; }

        public StudentManager()
        {
            LoadStudentList();
        }

        //public List<StudentView> GetStudentViews()
        //{
        //    List<StudentView> studentView = new List<StudentView>();
        //    foreach(Student s in Students)
        //    {
        //        studentView.Add(new StudentView { LastName = s.LastName, FirstName = s.FirstName, FatherName = s.FatherName, Birthday = s.Birthday, DutyList = s.DutyList });
        //    }
        //    return studentView;
        //}

        internal Student CreateStudent()
        {
            Student newStudent = new Student();
            Students.Add(newStudent);
            return newStudent;
        }

        internal void RemoveStudent(Student student)
        {
            Students.Remove(student);
            SaveStudentList();
        }

        internal Student GetBirthdayStudent()
        {
            // Calculate closest birthday student
            int today = DateTime.Today.DayOfYear;
            var birthdays = Students.Select(s => s.Birthday.DayOfYear - today);
            var birthdaysSorted = new List<int>(birthdays.Select(b => b >= 0 ? b : int.MaxValue));
            int indexOfMin = birthdaysSorted.IndexOf(birthdaysSorted.Min());
            return Students[indexOfMin];
        }

        internal (Student, Student) GetDuty()
        {
            Student firstStudent;
            Student secondStudent;
            Random rand = new Random();

            int totalDuties = 0;
            foreach (Student student in Students)
                totalDuties += student.DutyList.Count;
            double avg = (double)totalDuties / Students.Count;

            List<Student> dutyStudents = new List<Student>();
            foreach (Student student in Students)
                if (student.DutyList.Count <= avg)
                    dutyStudents.Add(student);

            firstStudent = dutyStudents[new Random().Next(0, dutyStudents.Count - 1)];
            dutyStudents.Remove(firstStudent);

            if (dutyStudents.Count == 0)
                secondStudent = Students[rand.Next(0, Students.Count - 1)];
            else
                secondStudent = dutyStudents[rand.Next(0, dutyStudents.Count - 1)];

            return (firstStudent, secondStudent);
        }

        internal void SetDuty(Student firstStudent, Student secondStudent)
        {
            firstStudent.DutyList.Add(DateTime.Today);
            secondStudent.DutyList.Add(DateTime.Today);
        }

        private const string path = "students.db";

        public void SaveStudentList()
        {
            Students.Sort((s1, s2) => s1.LastName.CompareTo(s2.LastName));
            var json = JsonSerializer.Serialize(Students, typeof(List<Student>));
            File.WriteAllText(path, json);
        }

        public void LoadStudentList()
        {
            string file = path;
            if (!File.Exists(file) || new FileInfo(file).Length == 0)
            {
                Students = new List<Student>();
                return;
            }
            string json = File.ReadAllText(file);
            Students = (List<Student>)JsonSerializer.Deserialize(json, typeof(List<Student>));
        }
    }
}
