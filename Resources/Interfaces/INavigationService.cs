using Batsy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Batsy.Resources.Interfaces
{
    public interface INavigationService
    {
        ViewModel CurrentView { get; }
        void NavigateTo<T>() where T : ViewModel;
    }
}
