using Batsy.Infrastructures;
using Batsy.Resources.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batsy.ViewModels
{
    public class CandidateListViewModel:ViewModel
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

        public RelayCommand NavigateToDashbordCommand
        {
            get
            {
                return new RelayCommand(execute: o => { Navigation.NavigateTo<DashbordViewModel>(); }, canExecute: o => true);
            }
        }
        public CandidateListViewModel(INavigationService navServie)
        {
            _navigation = navServie;
        }
    }
}
