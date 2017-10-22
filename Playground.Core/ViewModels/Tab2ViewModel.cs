using System.Windows.Input;
using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;

namespace Playground.Core.ViewModels
{
    public class Tab2ViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public Tab2ViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            CloseViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Close(this));
        }

        public IMvxAsyncCommand CloseViewModelCommand { get; private set; }
    }
}
