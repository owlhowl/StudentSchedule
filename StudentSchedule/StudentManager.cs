using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace StudentSchedule
{
    public class StudentManager
    {
        public List<Student> Students { get; set; } = new List<Student>();

        public StudentManager()
        {
            Students.Add(new Student { FirstName = "Ммм...", LastName = "Хммм...", FatherName = "Хихи..."});
            Students.Add(new Student { FirstName = "Хаха...", LastName = "Хехе...", FatherName = "Хихи..."});
            Load();
        }

        internal void SaveStudent(Student orig, Student copy)
        {
            int index = Students.IndexOf(orig);
            Students[index] = copy;
            Save();
        }

        internal Student CreateStudent()
        {
            Student newStudent = new Student();
            Students.Add(newStudent);
            return newStudent;
        }

        internal void SetOnDuty(Student firstStudent, Student secondStudent)
        {
            throw new NotImplementedException();
        }

        internal void RemoveStudent(Student student)
        {
            Students.Remove(student);
            Save();
        }

        internal void SetDuty()
        {
            int duties = 0;
            Random rand = new Random();
            foreach(Student student in Students)
                duties += student.DutyList.Count;
            int average = duties / Students.Count;

            List<Student> dutyStudents = new List<Student>();
            foreach (Student student in Students)
                if (student.DutyList.Count <= average)
                    dutyStudents.Add(student);

            Student firstStudent = dutyStudents[rand.Next(0, dutyStudents.Count-1)];
            dutyStudents.Remove(firstStudent);
            Student secondStudent = dutyStudents[rand.Next(0, dutyStudents.Count - 1)];

            Save();
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(Students, typeof(List<Student>));
            File.WriteAllText("clients.db", json);
        }

        public void Load()
        {
            string file = "clients.db";
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
