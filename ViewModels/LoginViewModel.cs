

using Batsy.Infrastructures;
using Batsy.Models;
using Batsy.Resources.Interfaces;
using Batsy.Resources.Services;
using System.Windows;

namespace Batsy.ViewModels;

public class LoginViewModel : ViewModel
{
    private readonly IResourceService _resourceService;
    private INavigationService _navigation;
    private readonly ITokenContainer _tokenContainer;



    public INavigationService Navigation
    {
        get => _navigation;
        set
        {
            _navigation = value;
            OnPropertyChanged(nameof(Navigation));
        }
    }

    private string _password;
    private string _userName;
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged(nameof(UserName));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    public RelayCommand CloseWindowCommand
    {
        get
        {
            return new RelayCommand(o=> { Navigation.NavigateTo<DashbordViewModel>(); }, o=> true);
        }

    }

    public RelayCommand LoginCommand
    {
        get
        {
            return new RelayCommand(async (o) => await Login());
        }

    }
    private readonly MainViewModel _mainViewModel;
    public LoginViewModel(INavigationService navService, 
                         IResourceService resourceService,
                         MainViewModel mainViewModel,
                         ITokenContainer tokenContainer)
    {
        Navigation = navService;
        _resourceService = resourceService;
        _mainViewModel = mainViewModel;
        _tokenContainer = tokenContainer;
    }

    private async Task Login()
    {
       
        if(string.IsNullOrWhiteSpace(UserName))
        {
            MessageBox.Show("Invalid user name", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            MessageBox.Show("Invalid password", "Login", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        var _request = new LoginRequest
        {
            PersonnelNo = UserName,
            Password = Password,
        };
        var (_success, _message, _result) = await _resourceService.Login(_request);
        if(!_success)
        {
            MessageBox.Show(_message,"Login", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        _tokenContainer.SetToken(_result.Token);
        var (_status, _msg, _staff) = await _resourceService.GetStaffDetails(UserName);
        if(!_status)
        {
            MessageBox.Show(_msg,"Get Staff details", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        _mainViewModel.FullName = _staff.ToString();
        _mainViewModel.PersonnelNumber = _staff.PersonnelNo;
        _mainViewModel.Picture = _staff.ToBitmap();
    }
}
