﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Chupelupe.Helpers;
using Chupelupe.Services;
using Xamarin.Forms;

namespace Chupelupe.ViewModels.Helpers
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDependencyService DependencyServiceWrapper { get; set; }
        public INavigation Navigation { get; set; }
        public IWebServiceApi WebApi { get; set; }

        public BaseViewModel(INavigation navigation, IDependencyService dependencyService)
        {
            DependencyServiceWrapper = dependencyService;
            Navigation = navigation;
            //WebApi = new WebServicesApi();

            WebApi = DependencyServiceWrapper.Get<IWebServiceApi>();
        }

        protected bool SetValue<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
