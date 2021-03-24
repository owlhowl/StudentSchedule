using Mvvm1125;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentSchedule
{
    public class DutyListVM : MvvmNotify, IPageVM
    {
        Model model;

        public Student FirstStudent { get; set; }
        public Student SecondStudent { get; set; }

        public MvvmCommand BackToList { get; set; }
        public MvvmCommand Agree { get; set; }

        public void SetModel(Model model)
        {
            this.model = model;

            BackToList = new MvvmCommand(
                () => PageManager.ChangePageTo(PageType.StudentList), 
                () => true);
            Agree = new MvvmCommand(
                () => { PageManager.ChangePageTo(PageType.StudentList); model.SetDuty(FirstStudent, SecondStudent); }, 
                () => true);

            model.OnDutyChanged += Model_OnDutyChanged;
        }

        private void Model_OnDutyChanged(object sender, EventArgs e)
        {
            var duties = model.GetDuty();
            FirstStudent = duties.Item1;
            SecondStudent = duties.Item2;
            NotifyPropertyChanged("FirstStudent");
            NotifyPropertyChanged("SecondStudent");
        }
    }
}
