﻿

namespace Batsy.ViewModels;

using Batsy.Infrastructures;
using Batsy.Resources.Interfaces;

public class HomeViewModel : ViewModel
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

    public HomeViewModel(INavigationService navServie)
    {
        _navigation = navServie;
    }
    public RelayCommand NavigateToLoginCommand
    {
        get
        {
            return new RelayCommand(execute: o => { Navigation.NavigateTo<LoginViewModel>(); }, canExecute: o => true);
        }
    }

    public RelayCommand NavigateToDashbordCommand
    {
        get
        {
            return new RelayCommand(execute: o => { Navigation.NavigateTo<DashbordViewModel>(); }, canExecute: o => true);
        }
    }

    
}
