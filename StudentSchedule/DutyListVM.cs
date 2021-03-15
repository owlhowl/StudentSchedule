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
            BackToList = new MvvmCommand(() => PageManager.ChangePageTo(PageType.StudentList), () => true);
            Agree = new MvvmCommand(() => model.SetOnDuty(FirstStudent, SecondStudent), () => true);
        }
    }
}
