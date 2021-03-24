using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace StudentSchedule
{
    public class Model
    {
        StudentManager studentManager = new StudentManager();

        public event EventHandler StudentsChanged;
        public event EventHandler<Student> SelectedStudentChanged;
        public event EventHandler OnDutyChanged;
        public event EventHandler<Student> BirthdayStudentChanged;

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

        internal bool CanSave(Student selectedStudent)
        {
            return selectedStudent != null && !(string.IsNullOrWhiteSpace(selectedStudent.FirstName) ||
                string.IsNullOrWhiteSpace(selectedStudent.LastName) ||  
                DateTime.Today < selectedStudent.Birthday ||
                selectedStudent.Birthday < new DateTime(1900, 1, 1));
        }

        internal void SaveStudent()
        {
            studentManager.SaveStudentList();
            StudentsChanged?.Invoke(this, null);
            GetBirthdayStudent();
        }

        internal void NoSaveStudent()
        {
            studentManager.LoadStudentList();
            StudentsChanged?.Invoke(this, null);
        }

        internal Student CreateStudent()
        {
            return studentManager.CreateStudent();
        }

        internal void EditStudent(Student selectedStudent)
        {
            SelectedStudentChanged?.Invoke(this, selectedStudent);
        }

        internal void RemoveStudent(Student student)
        {
            studentManager.RemoveStudent(student);
            StudentsChanged?.Invoke(this, null);
            GetBirthdayStudent();
        }

        internal (Student, Student) GetDuty() 
        {
            return studentManager.GetDuty();
        }

        internal void Duty()
        {
            OnDutyChanged?.Invoke(this, null);
        }

        internal void SetDuty(Student firstStudent, Student secondStudent)
        {
            studentManager.SetDuty(firstStudent, secondStudent);
        }
    }
}
