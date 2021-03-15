using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace StudentSchedule
{
    public class Model
    {
        StudentManager studentManager = new StudentManager();
        Student selectedStudent;

        public event EventHandler StudentsChanged;
        public event EventHandler SelectedStudentChanged;

        public Student SelectedStudent
        {
            get => selectedStudent;
            set { selectedStudent = value; SelectedStudentChanged?.Invoke(this, null); }
        }

        internal List<Student> GetStudents()
        {
            return studentManager.Students;
        }

        internal void SetOnDuty(Student firstStudent, Student secondStudent)
        {
            studentManager.SetOnDuty(firstStudent, secondStudent);
        }

        internal void Save()
        {
            studentManager.Save();
        }

        //internal void SaveStudent(Student orig, Student copy)
        //{
        //    studentManager.SaveStudent(orig, copy);
        //    StudentsChanged?.Invoke(this, null);
        //}

        internal void CreateStudent()
        {
            SelectedStudent = studentManager.CreateStudent();
            PageManager.ChangePageTo(PageType.EditStudent);
            StudentsChanged?.Invoke(this, null);
        }

        internal void EditStudent()
        {
            PageManager.ChangePageTo(PageType.EditStudent);
        }

        internal void RemoveStudent()
        {
            studentManager.RemoveStudent(SelectedStudent);
            StudentsChanged?.Invoke(this, null);
        }

        internal void SetDuty()
        {
            studentManager.SetDuty();
        }
    }
}
