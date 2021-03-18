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
        public event EventHandler<Student> BirthdayStudentChanged;

        public Student SelectedStudent 
        { 
            get => selectedStudent; 
            set 
            { 
                selectedStudent = value; 
                SelectedStudentChanged?.Invoke(this, null); 
            }
        }

        internal void GetBirthdayStudent()
        {
            BirthdayStudentChanged?.Invoke(this, studentManager.GetBirthdayStudent());
        }

        internal List<Student> GetStudents()
        {
            return studentManager.Students;
        }

        internal void Save()
        {
            studentManager.SaveStudentList();
        }

        internal bool CanSave()
        {
            return SelectedStudent != null && !(string.IsNullOrWhiteSpace(SelectedStudent.FirstName) ||
                string.IsNullOrWhiteSpace(SelectedStudent.LastName) ||
                DateTime.Today < SelectedStudent.Birthday ||
                SelectedStudent.Birthday < new DateTime(1900, 1, 1));
        }

        internal void SaveStudent()
        {
            PageManager.ChangePageTo(PageType.StudentList);
            studentManager.SaveStudentList();
            StudentsChanged?.Invoke(this, null);
            GetBirthdayStudent();
        }

        internal void NoSaveStudent()
        {
            PageManager.ChangePageTo(PageType.StudentList);
            studentManager.LoadStudentList();
            SelectedStudent = null;
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
            GetBirthdayStudent();
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
