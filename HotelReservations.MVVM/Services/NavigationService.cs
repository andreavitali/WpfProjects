using HotelReservations.MVVM.Stores;
using MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.MVVM.Services
{
    public class NavigationService
    {
        private readonly NavigationStore _navigationStore;
        private readonly Func<ViewModelBase> _navigationFunc;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> navigationFunc)
        {
            _navigationStore = navigationStore;
            _navigationFunc = navigationFunc;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _navigationFunc();
        }
    }
}
