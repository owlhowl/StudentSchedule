using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Mvvm1125;

namespace StudentSchedule
{
    public class MainVM : MvvmNotify
    {
        Model model;

        public Page CurrentPage { get; set; }

        public MainVM()
        {
            model = new Model();
            PageManager.SetModel(model);
            CurrentPage = PageManager.GetPageByType(PageType.StudentList);
            PageManager.CurrentPageChanged += PageContainer_CurrentPageChanged;
            Application.Current.Exit += (o, e) => model.Save();
        }

        void PageContainer_CurrentPageChanged(object sender, PageType e)
        {
            CurrentPage = PageManager.GetPageByType(e);
            NotifyPropertyChanged("CurrentPage");
        }
    }
}
