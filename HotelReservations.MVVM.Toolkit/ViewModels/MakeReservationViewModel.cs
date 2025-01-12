using HotelReservations.MVVM.Commands;
using HotelReservations.MVVM.Services;
using HotelReservations.MVVM.Stores;
using MVVM.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.ViewModels
{
    public class MakeReservationViewModel : ViewModelBase, INotifyDataErrorInfo
    {
		private string _username;
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
				OnPropertyChanged(nameof(Username));
			}
		}

        private int _floorNumber;
        public int FloorNumber
        {
            get
            {
                return _floorNumber;
            }
            set
            {
                _floorNumber = value;
                OnPropertyChanged(nameof(FloorNumber));
            }
        }

        private int _roomNumber;
        public int RoomNumber
        {
            get
            {
                return _roomNumber;
            }
            set
            {
                _roomNumber = value;
                OnPropertyChanged(nameof(RoomNumber));
            }
        }

        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError(nameof(StartDate), "Start date cannot be after end date.");
                }
            }
        }

        private DateTime _endDate = DateTime.Today.AddDays(1);

        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));

                ClearErrors(nameof(StartDate));
                ClearErrors(nameof(EndDate));

                if (EndDate < StartDate)
                {
                    AddError(nameof(EndDate), "End date must be after start date.");
                }
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(HotelStore hotelStore, NavigationService<ReservationsListingViewModel> reservationListNavigationService)
        {
            SubmitCommand = new MakeReservationCommand(this, hotelStore, reservationListNavigationService);
            CancelCommand = new NavigateCommand<ReservationsListingViewModel>(reservationListNavigationService);
        }

        #region Errors

        private Dictionary<string, List<string>> _propertyNameToErrorsDictionary = [];

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public bool HasErrors => _propertyNameToErrorsDictionary.Any();

        public IEnumerable GetErrors(string? propertyName)
        {
            return _propertyNameToErrorsDictionary.GetValueOrDefault(propertyName ?? "", new List<string>());
        }

        private void ClearErrors(string propertyName)
        {
            _propertyNameToErrorsDictionary.Remove(propertyName);
            OnErrorsChanged(propertyName);
        }

        private void AddError(string propertyName, string error)
        {
            if(!_propertyNameToErrorsDictionary.ContainsKey(propertyName))
            {
                _propertyNameToErrorsDictionary.Add(propertyName, []);
            }

            _propertyNameToErrorsDictionary[propertyName].Add(error);

            OnErrorsChanged(propertyName);
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}
