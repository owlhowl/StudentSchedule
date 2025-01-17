﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading;
using Mvvm1125;

namespace StudentSchedule
{
    public class StudentListVM : MvvmNotify, IPageVM
    {
        Model model;
        public ObservableCollection<Student> Students { get; set; }
        public Student SelectedStudent { get; set; }
        public string BirthdayText { get; set; }

        public MvvmCommand CreateStudent { get; set; }
        public MvvmCommand EditStudent { get; set; }
        public MvvmCommand RemoveStudent { get; set; }
        public MvvmCommand Duty { get; set; }

        public void SetModel(Model model)
        {
            this.model = model;
            Students = new ObservableCollection<Student>(model.GetStudents());

            CreateStudent = new MvvmCommand(
                () => { PageManager.ChangePageTo(PageType.EditStudent); model.EditStudent(model.CreateStudent()); },
                () => true);

            EditStudent = new MvvmCommand(
                () => { PageManager.ChangePageTo(PageType.EditStudent); model.EditStudent(SelectedStudent); },
                () => SelectedStudent != null);

            RemoveStudent = new MvvmCommand(
                () => model.RemoveStudent(SelectedStudent),
                () => SelectedStudent != null);

            Duty = new MvvmCommand(
                () => { PageManager.ChangePageTo(PageType.DutyList); model.Duty(); }, 
                () => Students.Count >= 2);

            model.StudentsChanged += Model_StudentsChanged;
            model.BirthdayStudentChanged += Model_BirthdayStudentChanged;
            model.GetBirthdayStudent();
        }

        private void Model_BirthdayStudentChanged(object sender, Student e)
        {
            BirthdayText = $"Скоро ДР: {e.LastName} {e.FirstName} - {e.Birthday.ToShortDateString()}";
            NotifyPropertyChanged("BirthdayText");
        }

        private void Model_StudentsChanged(object sender, EventArgs e)
        {
            Students = new ObservableCollection<Student>(model.GetStudents());
            NotifyPropertyChanged("Students");
        }
    }
}
