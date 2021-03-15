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
        public event EventHandler OnDutyChanged;

        public Student SelectedStudent { get => selectedStudent; set { selectedStudent = value; SelectedStudentChanged?.Invoke(this, null); }}

        internal List<Student> GetStudents()
        {
            return studentManager.Students;
        }

        internal void Save()
        {
            studentManager.SaveStudentList();
        }

        internal void SaveStudent()
        {
            PageManager.ChangePageTo(PageType.StudentList);
            studentManager.SaveStudentList();
            StudentsChanged?.Invoke(this, null);
        }

        internal void NoSaveStudent()
        {
            PageManager.ChangePageTo(PageType.StudentList);
            studentManager.LoadStudentList();
            StudentsChanged?.Invoke(this, null);
        }

        internal void CreateStudent()
        {
            SelectedStudent = studentManager.CreateStudent();
            PageManager.ChangePageTo(PageType.EditStudent);
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

        internal (Student, Student) GetDuty() 
        {
            return studentManager.GetDuty();
        }

        internal void Duty()
        {
            PageManager.ChangePageTo(PageType.DutyList);
            OnDutyChanged?.Invoke(this, null);
        }

        internal void SetDuty(Student firstStudent, Student secondStudent)
        {
            PageManager.ChangePageTo(PageType.StudentList);
            studentManager.SetDuty(firstStudent, secondStudent);
        }
    }
}
