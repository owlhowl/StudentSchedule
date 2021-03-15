using Mvvm1125;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentSchedule
{
    public class EditStudentVM : MvvmNotify, IPageVM
    {
        Model model;

        public Student SelectedStudent { get => model.SelectedStudent; set => model.SelectedStudent = value; }
        public Student SelectedStudentCopy { get; set; }

        public MvvmCommand BackToList { get; set; }
        public MvvmCommand SaveStudent { get; set; }

        public void SetModel(Model model)
        {
            this.model = model;
            BackToList = new MvvmCommand(() => PageManager.ChangePageTo(PageType.StudentList), () => true);
            //SaveStudent = new MvvmCommand(() => model.SaveStudent(SelectedStudent), () => true);

            model.SelectedStudentChanged += Model_SelectedStudentChanged;
        }

        private void Model_SelectedStudentChanged(object sender, EventArgs e)
        {
            NotifyPropertyChanged("SelectedStudent");
            NotifyPropertyChanged("SelectedStudentCopy");
        }
    }
}
