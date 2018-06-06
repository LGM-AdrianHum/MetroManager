// File: MetroManager/MetroManager/MainViewModel.cs
// User: Adrian Hum/
//
// Created:  2018-06-06 8:34 PM
// Modified: 2018-06-06 10:00 PM

using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.Management.Deployment;

namespace MetroManager.ViewModels
{
    internal class MainViewModel : BindableBase
    {
        private IEnumerable<Package> _packages;

        private string _searchText;

        private Package _selectedPackage;

        public MainViewModel()
        {
            LaunchCommand = new DelegateCommand(
                    () =>
                    {
                        try
                        {
                            MetroLauncher.LaunchApp(SelectedPackage.Id.FullName);
                        }
                        catch
                        {
                            MessageBox.Show("Error launching app", "Metro Launcher");
                        }
                    },
                    () => SelectedPackage != null && !SelectedPackage.IsFramework)
                .ObservesProperty(() => SelectedPackage);
        }

        public IEnumerable<Package> Packages =>
            _packages ?? (_packages = new PackageManager().FindPackagesForUser(string.Empty).ToArray());

        public DelegateCommand LaunchCommand { get; }

        public Package SelectedPackage
        {
            get => _selectedPackage;
            set => SetProperty(ref _selectedPackage, value);
        }

        public ICommand RefreshCommand => new DelegateCommand(() => OnPropertyChanged(nameof(Packages)));

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (!SetProperty(ref _searchText, value)) return;
                var view = CollectionViewSource.GetDefaultView(Packages);
                if (string.IsNullOrEmpty(value))
                {
                    view.Filter = null;
                }
                else
                {
                    var text = value.ToLower();
                    view.Filter = obj =>
                    {
                        var package = (Package)obj;
                        return package.Id.Name.ToLower().Contains(text);
                    };
                }
            }
        }
    }
}