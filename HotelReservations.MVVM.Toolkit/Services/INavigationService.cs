using MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.MVVM.Services
{
    public interface INavigationService
    {
        IPageViewModel CurrentViewModel { get; }
        void NavigateTo<TViewModel>() where TViewModel : IPageViewModel;
    }
}
