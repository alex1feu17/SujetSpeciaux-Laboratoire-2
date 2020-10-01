using ApiHelper;
using ApiHelper.Models;
using DogFetchApp.Commands;
using DogFetchApp.Models;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DogFetchApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        private ObservableCollection<BitmapImage> dogimage;

        public ObservableCollection<BitmapImage> DogImage
        {
            get => dogimage;
            set
            {
                dogimage = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DogModel> races;
        public ObservableCollection<DogModel> Races
        {
            get => races;
            set
            {
                races = value;
                OnPropertyChanged();
            }
        }
        
        private DogModel selectedDog;

        public DogModel SelectedDog
        {
            get => selectedDog;
            set
            {
                selectedDog = value;
                OnPropertyChanged();
            }
        }
        private DogModel currentDog;

        public DogModel CurrentDog
        {
            get => currentDog;
            set
            {
                currentDog = value;
                OnPropertyChanged();
            }
        }
        private NumberModel selectedNumber;

        public NumberModel SelectedNumber
        {
            get => selectedNumber;
            set
            {
                selectedNumber = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<NumberModel> numberList1; 

        public ObservableCollection<NumberModel> NumberList1
        {
            get => numberList1;
            set
            {
                numberList1 = value;
                OnPropertyChanged();
            }
        }


        
        public RelayCommand LanguageFr { get; private set; }
        public RelayCommand LanguageEn { get; private set; }
        public RelayCommand Restart { get; private set; }
       
        
        public AsyncCommand<object> Suivant { get; private set; }

        public RelayCommand<object> RaceList { get; private set; }
        
       

        public AsyncCommand<string> Loadimage { get; private set; }
        public MainViewModel()
        {

            initValues();
           
        }

        private void initValues()
        {

            LanguageFr = new RelayCommand(Language_fr);
            LanguageEn = new RelayCommand(Language_En);
            Restart = new RelayCommand(Restart_apli);
            RaceList = new RelayCommand<object>(LoadRaceList);
            Loadimage = new AsyncCommand<string>(LoadImageDog);
            Suivant = new AsyncCommand<object>(SuivantDog);
            SelectedDog = new DogModel();
            SelectedNumber = new NumberModel();

            CurrentDog = new DogModel();
            DogImage = new ObservableCollection<BitmapImage>();



            NumberList1 = new ObservableCollection<NumberModel>()
            {
                new NumberModel() { Number = 1},
                new NumberModel() { Number = 3 },
                new NumberModel() { Number = 5 },
                new NumberModel() { Number = 10 },

            };


        }

        private async Task SuivantDog(object arg)
        {
            await LoadImageDog(CurrentDog.Name);
        }

        public async void LoadRaceList(object sender)
        {
            var res = await DogApiProcessor.LoadBreedList();
            Races = new ObservableCollection<DogModel>(res);    
           
        }

        private void Change_Language(string param)
        {
            Properties.Settings.Default.Language = param;
            Properties.Settings.Default.Save();
        }
        private void Restart_apli(object obj)
        {
            var filename = Application.ResourceAssembly.Location;
            var newFile = Path.ChangeExtension(filename, ".exe");
            Process.Start(newFile);
            Application.Current.Shutdown();
        }
        private void Language_En(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Est ce que tu veux redemarer l'application", "My App", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Change_Language("en");
                    Restart.Execute(0);
                    break;
                case MessageBoxResult.No:
                    Change_Language("en");
                    break;

            }
        }
        private void Language_fr(object obj)
        {
            MessageBoxResult result = MessageBox.Show("Est ce que tu veux redemarer l'application", "My App", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Change_Language("fr");
                    Restart.Execute(0);
                    break;
                case MessageBoxResult.No:
                    Change_Language("fr");
                    break;

            }
        }
        public async Task LoadImageDog(string nom)
        {

            DogImage = new ObservableCollection<BitmapImage>();
            for (int i = 0; i < SelectedNumber.Number; i++)
            {
                var Dog1 = await DogApiProcessor.GetImageUrl(SelectedDog.Name);
                var uriSource1 = new Uri(Dog1.img, UriKind.Absolute);

                DogImage.Add(new BitmapImage(uriSource1,
                new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable)));
            }
           

        }
    }
}
