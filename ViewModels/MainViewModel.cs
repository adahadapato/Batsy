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
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Batsy.Resources.Services;

namespace Batsy.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private INavigationService _navigation;
        private readonly RegistryService _registryService;
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

        public RelayCommand NavigateToLoginCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); }, canExecute: o => true);
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

        public RelayCommand LogoutCommand
        {
            get
            {
                return new RelayCommand(o => LogOut());
            }

        }

        public RelayCommand<object> ExamsTypeCommand
        {
            get
            {
                return new RelayCommand<object>(async(object e) =>  await SetExamsTypeAsync(e) );
            }
        }

       
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
        public MainViewModel(INavigationService navService,
                             RegistryService registryService)
        {
            Navigation = navService;
            _registryService = registryService;
            Initialize();
            Reset();
            
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
                _registryService.ExamYear = _year;
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


        private string _examination = string.Empty;
        public string Examination
        {
            get => _examination;
            set
            {
                SetProperty(ref _examination, value);
                OnPropertyChanged(nameof(Examination));
                _registryService.Examination = _examination;
            }
        }


        private bool _showLogin;
        public bool ShowLogIn
        {
            get => _showLogin;
            set
            {
                SetProperty(ref _showLogin, value);
                OnPropertyChanged(nameof(ShowLogIn));
               
            }
        }

        private bool _showLogOut;
        public bool ShowLogOut
        {
            get => _showLogOut;
            set
            {
                SetProperty(ref _showLogOut, value);
                OnPropertyChanged(nameof(ShowLogOut));
               
            }
        }

        private ImageSource _Picture;
        public ImageSource Picture
        {
            get=> _Picture ;
            set
            {
                SetProperty(ref _Picture, value);
                OnPropertyChanged(nameof(Picture));
            }
        }

        public Task SutdownAsync()
        {
            Application.Current.Shutdown();
            return Task.CompletedTask;
        }

      

        public Task SetExamsTypeAsync(object e)
        {
            if(e is null)
            {
                MessageBox.Show("Invalid exams type");
                return Task.CompletedTask;
            }
            if (string.IsNullOrWhiteSpace(_registryService.ExamYear) || _registryService.ExamYear.Length < 4)
            {
                MessageBox.Show("Please provide the examination year");
                return Task.CompletedTask;
            }

            var _today = DateTime.Today.Year;
            if (Convert.ToInt32(_registryService.ExamYear) > _today)
            {
                MessageBox.Show("The examination year provided is greater the current year");
                return Task.CompletedTask;
            }

            
            if (string.IsNullOrWhiteSpace(_registryService.ExamYear) || _registryService.ExamYear.Length < 4)
            {
                MessageBox.Show("Examination year must ne 4 digit long: 2020");
                return Task.CompletedTask;
            }

            Examination = e as string;
            GetExamType(e);

            if (Convert.ToInt32(_registryService.ExamYear) < 2002  && _registryService.ExamType.ToLower().Contains("ext"))
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
                _registryService.ExamType = string.Empty;
            }

            _registryService.ExamType =  exam.Trim();

            if (exam.ToLower().Contains("int") || exam.ToLower().Contains("ext"))
            {
                var _exam = exam.AsSpan()[..3];
                _registryService.ExamType = _exam.ToString();
            }
        }

        private void GetExamDetails(object e)
        {
            var exam = e as string;
            if (exam == null)
            {
                _registryService.ExamsDetails = string.Empty;
            }
            if (exam.ToLower().Contains("int") || exam.ToLower().Contains("ext"))
            {
              _registryService.ExamsDetails = $"SSCE{_registryService.ExamType}{_registryService.ExamYear}";
            }
            else
            {
                _registryService.ExamsDetails = $"{_registryService.ExamType}{_registryService.ExamYear}";
            }

            ExamsDetails = _registryService.ExamsDetails;
        }

        private void LogOut()
        {
            if(MessageBox.Show("Do you want to log out now","Logout",MessageBoxButton.YesNo, MessageBoxImage.Question) is MessageBoxResult.No)
            {
                return;
            }

            _registryService.LogOut = true;
            _registryService.PersonnelNo = "0000";
            _registryService.FullName = "Not logged in";
            _registryService.ApiToken = "";
            Reset();
            ShowLogOut = _registryService.LogOut;
            ShowLogIn = !_registryService.LogOut;
            Picture = null;
        }

        private void Reset()
        {
            FullName = _registryService.FullName;
            PersonnelNumber = _registryService.PersonnelNo;
            Operations = "Awaitinng Input";
            IsFocused = true;
            //ShowLogIn = true;
        }

        private void Initialize()
        {
            Year = _registryService.ExamYear;
            Examination = _registryService.Examination;
            ExamsDetails = _registryService.ExamsDetails;
            ShowLogIn = _registryService.LogOut;
            if (_registryService.FirstStart)
            {
                _registryService.LogOut = true;
            }
        }
    }
}
