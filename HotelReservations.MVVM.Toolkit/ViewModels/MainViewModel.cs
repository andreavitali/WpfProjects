using HotelReservations.MVVM.Stores;
using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentViewModelChanged;
        }

        private void _navigationStore_CurrentViewModelChanged()
        {
            this.OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
