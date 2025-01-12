using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Controls;

namespace HotelReservations.MVVM.Toolkit.Views
{
    public abstract class BaseContentView<TViewModel> : UserControl where TViewModel : ObservableObject
    {
        protected BaseContentView(TViewModel viewModel)
        {
            base.DataContext = viewModel;
        }

        //protected new TViewModel BindingContext => (TViewModel)base.BindingContext;
    }
}
