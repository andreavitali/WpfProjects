using HotelReservations.MVVM.Stores;
using MVVM.Models;
using MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelReservations.MVVM.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly ReservationsListingViewModel _viewModel;
        private readonly HotelStore _hotelStore;

        public LoadReservationsCommand(ReservationsListingViewModel viewModel, HotelStore hotelStore)
        {
            _viewModel = viewModel;
            _hotelStore = hotelStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.IsLoading = true;

            try
            {
                await _hotelStore.LoadAsync();                
                _viewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Failed to load reservations.";
                //MessageBox.Show("Failed to load reservations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _viewModel.IsLoading = false;
            }
        }
    }
}
