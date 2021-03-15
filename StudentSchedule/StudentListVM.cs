using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Mvvm1125;

namespace StudentSchedule
{
    public class StudentListVM : MvvmNotify, IPageVM
    {
        Model model;
        public ObservableCollection<Student> Students { get; set; }
        public Student SelectedStudent { get => model.SelectedStudent; set => model.SelectedStudent = value; }

        public MvvmCommand CreateStudent { get; set; }
        public MvvmCommand EditStudent { get; set; }
        public MvvmCommand RemoveStudent { get; set; }
        public MvvmCommand ChooseDuty { get; set; }

        public void SetModel(Model model)
        {
            this.model = model;
            Students = new ObservableCollection<Student>(model.GetStudents());

            CreateStudent = new MvvmCommand(() => model.CreateStudent(), () => true);
            EditStudent = new MvvmCommand(() => model.EditStudent(), () => SelectedStudent != null);
            RemoveStudent = new MvvmCommand(() => model.RemoveStudent(), () => SelectedStudent != null);
            ChooseDuty = new MvvmCommand(() => model.SetDuty(), () => Students.Count >= 2);

            model.StudentsChanged += Model_StudentsChanged;
            PageManager.CurrentPageChanged += PageManager_CurrentPageChanged;
        }

        private void PageManager_CurrentPageChanged(object sender, PageType e)
        {
            Students = new ObservableCollection<Student>(model.GetStudents());
            NotifyPropertyChanged("Students");
        }

        private void Model_StudentsChanged(object sender, EventArgs e)
        {
            Students = new ObservableCollection<Student>(model.GetStudents());
            NotifyPropertyChanged("Students");
        }
    }
}
