using MaterialDesignThemes.Wpf.Transitions;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Batsy.Infrastructures;
using Batsy.Views;
using Batsy.Resources.Interfaces;

namespace Batsy.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged(nameof(Navigation));
            }
        }

        public RelayCommand NavigateToHomeCommand {get; set;}
       
        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            NavigateToHomeCommand = new RelayCommand(execute: o =>
            { 
                Navigation.NavigateTo<HomeViewModel>();  
            }, canExecute: o => true);
            //NavigateToHomeCommand.Execute(Navigation.CurrentView);
        }
    }
}
