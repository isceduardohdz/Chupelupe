using System;
using System.Collections.Generic;
using Chupelupe.Helpers;
using Chupelupe.ViewModels;

using Xamarin.Forms;

namespace Chupelupe.Views
{
    public partial class PromotionList : ContentPage
    {
        PromotionListViewModel _vm;
        public PromotionList()
        {
            InitializeComponent();
            //BindingContext = new PromotionListViewModel(Navigation);
            _vm = new PromotionListViewModel(Navigation, new DependencyServiceWrapper());
            BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.GetPromotionsCommand.Execute(null);
        }
    }
}
