using Mvvm1125;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentSchedule
{
    public class EditStudentVM : MvvmNotify, IPageVM
    {
        Model model;

        public Student SelectedStudent { get; set; }

        public MvvmCommand BackToList { get; set; }
        public MvvmCommand SaveStudent { get; set; }

        public void SetModel(Model model)
        {
            this.model = model;

            BackToList = new MvvmCommand(
                () => { PageManager.ChangePageTo(PageType.StudentList); model.NoSaveStudent(); }, 
                () => true);
            SaveStudent = new MvvmCommand(
                () => { PageManager.ChangePageTo(PageType.StudentList); model.SaveStudent(); }, 
                () => model.CanSave(SelectedStudent));

            model.SelectedStudentChanged += Model_SelectedStudentChanged;
        }

        private void Model_SelectedStudentChanged(object sender, Student e)
        {
            SelectedStudent = e;
            NotifyPropertyChanged("SelectedStudent");
        }
    }
}
