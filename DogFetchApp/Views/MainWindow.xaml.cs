using DogFetchApp.ViewModels;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace DogFetchApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel currentViewmodel;
        
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.ApiHelper.InitializeClient();

            currentViewmodel = new MainViewModel();

            DataContext = currentViewmodel;
        }
        
        private  void LoadCommand(object sender, RoutedEventArgs e)
        {
             currentViewmodel.RaceList.Execute(0);
           
        }

       
    }
}
