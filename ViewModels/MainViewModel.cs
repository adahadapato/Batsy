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
using System.Windows;

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
                SetProperty(ref _navigation, value);
                OnPropertyChanged(nameof(Navigation));
            }
        }

       
        public RelayCommand NavigateToHomeCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<HomeViewModel>(); }, canExecute: o => true);
            }
        }


        public RelayCommand NavigateToDashborCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<DashbordViewModel>(); }, canExecute: o => true);
            }
        }

        public RelayCommand NavigateToCandidateCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<CandidateViewModel>(); }, canExecute: o => true);
            }
        }

        public RelayCommand NavigateToCandidateListCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<CandidateListViewModel>(); }, canExecute: o => true);
            }
        }
        public RelayCommand CloseApplicationCommand
        {
            get
            {
                return new RelayCommand(async o => await SutdownAsync());
            }

        }

        public RelayCommand<object> ExamsTypeCommand
        {
            get
            {
                return new RelayCommand<object>(async(object e) =>  await SetExamsTypeAsync(e) );
            }
        }

        //public RelayCommand<object> ExamsTypeCommand
        //{
        //    get
        //    {
        //        return new RelayCommand<object>(async (object e) => await SetExamsTypeAsync(e));
        //    }
        //}


        private bool _isFocused;
        public bool IsFocused
        {
            get => _isFocused;
            set
            {
                SetProperty(ref _isFocused, value);
                OnPropertyChanged(nameof(IsFocused));
            }
        }
        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            FullName = "Adahada ET";
            PersonnelNumber = "P 2020";
            Operations = "Awaitinng Input";
            IsFocused= true;
        }
        private string _fullName = string.Empty;
        public string FullName
        {
            get => _fullName;
            set
            {
                SetProperty(ref _fullName, value);
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string _personnelNumber = string.Empty;
        public string PersonnelNumber
        {
            get => _personnelNumber;
            set
            {
               SetProperty(ref _personnelNumber, value);
               OnPropertyChanged(nameof(PersonnelNumber));
            }
        }

        private string _operations = string.Empty;
        public string Operations
        {
            get => _operations;
            set
            {
                SetProperty(ref _operations, value);
                OnPropertyChanged(nameof(Operations));
            }
        }

        private string _year = string.Empty;
        public string Year
        {
            get => _year;
            set
            {
                SetProperty(ref _year, value);
                OnPropertyChanged(nameof(Year));
                App.ExamYear = _year;
            }
        }


        private string _examsDetails = string.Empty;
        public string ExamsDetails
        {
            get => _examsDetails;
            set
            {
                SetProperty(ref _examsDetails, value);
                OnPropertyChanged(nameof(ExamsDetails));
                //App.ExamYear = _year;
            }
        }

        public Task SutdownAsync()
        {
            Application.Current.Shutdown();
            return Task.CompletedTask;
        }

        //public Task SetExamsYearAsync()
        //{
        //    Application.Current.Shutdown();
        //    return Task.CompletedTask;
        //}

        public Task SetExamsTypeAsync(object e)
        {
            if(e is null)
            {
                MessageBox.Show("Invalid exams type");
                return Task.CompletedTask;
            }
            if (string.IsNullOrWhiteSpace(App.ExamYear) || App.ExamYear.Length < 4)
            {
                MessageBox.Show("Please provide the examination year");
                return Task.CompletedTask;
            }

            var _today = DateTime.Today.Year;
            if (Convert.ToInt32(App.ExamYear) > _today)
            {
                MessageBox.Show("The examination year provided is greater the current year");
                return Task.CompletedTask;
            }

            
            if (string.IsNullOrWhiteSpace(App.ExamYear) || App.ExamYear.Length < 4)
            {
                MessageBox.Show("Examination year must ne 4 digit long: 2020");
                return Task.CompletedTask;
            }


            GetExamType(e);

            if (Convert.ToInt32(App.ExamYear) < 2002  && App.ExamType.ToLower().Contains("ext"))
            {
                MessageBox.Show("The examination year provided is not valid for SSCE External");
                return Task.CompletedTask;
            }

            GetExamDetails(e);
            return Task.CompletedTask;
        }

        private void GetExamType(object e)
        {
            var exam = e as string;
            if (exam == null) 
            {
                App.ExamType = string.Empty;
            }

            App.ExamType =  exam.Trim();

            if (exam.ToLower().Contains("int") || exam.ToLower().Contains("ext"))
            {
                var _exam = exam.AsSpan()[..3];
                App.ExamType = _exam.ToString();
            }
        }

        private void GetExamDetails(object e)
        {
            var exam = e as string;
            if (exam == null)
            {
                ExamsDetails = string.Empty;
            }

            //App.ExamType = exam.Trim();
            //ExamsDetails = (exam.ToLower().Contains("int") || exam.ToLower().Contains("ext")) ? $"SSCE{App.ExamType}{Year}" : $"{App.ExamType}{Year}";
            if (exam.ToLower().Contains("int") || exam.ToLower().Contains("ext"))
            {
                ExamsDetails = $"SSCE{App.ExamType}{Year}";
            }
            else
            {
                ExamsDetails = $"{App.ExamType}{Year}";
            }
        }
    }
}
