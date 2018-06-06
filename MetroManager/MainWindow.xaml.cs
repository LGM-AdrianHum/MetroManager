// File: MetroManager/MetroManager/MainWindow.xaml.cs
// User: Adrian Hum/
//
// Created:  2018-06-06 8:34 PM
// Modified: 2018-06-06 10:00 PM

using MetroManager.ViewModels;

namespace MetroManager
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}