using System;
using Chupelupe.ViewModels.Helpers;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Chupelupe.Models;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Chupelupe.Helpers;

namespace Chupelupe.ViewModels
{
    public class PromotionListViewModel : BaseViewModel
    {
        public Command GetPromotionsCommand { get; set; }

        public ObservableCollection<Promotion> ObjectList { get; set; }

        bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                SetValue(ref _isBusy, value);
                Device.BeginInvokeOnMainThread(GetPromotionsCommand.ChangeCanExecute);
            }
        }

        public PromotionListViewModel(INavigation navigation, IDependencyService dependencyService) : base(navigation, dependencyService)
        {
            GetPromotionsCommand = new Command(async (obj) =>
            await ExecuteGetPromotionsCommand(obj), (obj) => !IsBusy);
            //ObjectList = new ObservableCollection<Promotion>
            //{
              //  new Promotion(){Id = 1,Title = "Cerveza 12 Pack XXX",Detail = "$40"},
                //new Promotion(){Id = 2,Title = "Cerveza 12 Pack 3XX",Detail = "$50"},
                //new Promotion(){Id = 3,Title = "Cerveza 12 Pack 5XX",Detail = "$60"},
            //};
        }

        async Task ExecuteGetPromotionsCommand(object obj)
        {
            try
            {
                if (IsBusy)
                {
                    return;
                }
                IsBusy = true;

                //Firebase
                var localPromotionList = new List<Promotion>();

                localPromotionList = await WebApi.GetPromotions().ConfigureAwait(false);



                if (!localPromotionList.Any())
                {
                    IsBusy = true;
                    return;
                }



                /*
                //Todo : Web Services

                await Task.Delay(3000);
                var LocalPromotionList = new System.Collections.Generic.List<Promotion>
                    {
                        new Promotion(){Title = "Cerveza",Detail = "1223"},
                        new Promotion(){Title = "Refresco",Detail = "5555"}

                    };*/

                ObjectList = new ObservableCollection<Promotion>(localPromotionList);
                OnPropertyChanged(nameof(ObjectList));
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                Console.WriteLine(ex.Message);
            }

        }
    }
}
