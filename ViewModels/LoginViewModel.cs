

using Batsy.Infrastructures;
using Batsy.Resources.Interfaces;

namespace Batsy.ViewModels;

public class LoginViewModel : ViewModel
{
    public LoginViewModel(INavigationService navService)
    {
        Navigation = navService;
    }

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
}
