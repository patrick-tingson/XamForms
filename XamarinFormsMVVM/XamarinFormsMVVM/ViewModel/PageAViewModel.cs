using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsMVVM.Model; 

namespace XamarinFormsMVVM.ViewModel
{
    public class PageAViewModel : INotifyPropertyChanged
    {   
        public PageAViewModel()
        {
            CheckProfileCommand = new Command(async ()=> await CheckProfile(), () => !IsBusy);
            //ImageCommand = new Command(async () => await CheckProfile(), () => !IsBusy);
            AddProfileCommand = new Command(AddProfile, () => !IsBusy);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            } 
        }

        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
                CheckProfileCommand.ChangeCanExecute();
                //ImageCommand.ChangeCanExecute();
                AddProfileCommand.ChangeCanExecute();
            }
        }

        bool isHuman;
        public bool IsHuman
        {
            get { return isHuman; }
            set { 
                isHuman = value;
                OnPropertyChanged();
                OnPropertyChanged("IsHumanLabel");
            }
        }

        string id = "";
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
                OnPropertyChanged("IsHumanLabel");
            }
        }

        string name = "";
        public string Name
        {
            get { return name; }
            set { 
                name = value;
                OnPropertyChanged();
                OnPropertyChanged("IsHumanLabel");
            }
        }

        public string IsHumanLabel
        {
            get { return string.Format("{0}-{1} is human? {2}", Id, Name, (IsHuman ? "Yes" : "No")); } 
        }

        public Command CheckProfileCommand { get; set; }
        public Command ImageCommand { get; set; }
        public Command AddProfileCommand { get; set; } 

        async Task CheckProfile()
        {
            IsBusy = true;

            try
            {
                if (await Application.Current.MainPage.DisplayAlert("MVVM", "Are you sure?", "Ok", "Cancel"))
                {
                    var pageA = new PageAModel();

                    var result = await pageA.GetDataAsync(Id);

                    await Application.Current.MainPage.DisplayAlert("MVVM",
                            string.Format("Profile checked! {0}", result),
                            "Ok");
                }
            }
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("MVVM", ex.Message, "Ok");
            }

            IsBusy = false; 
        } 

        async void AddProfile()
        {
            IsBusy = true; 

            try
            {
                if (await Application.Current.MainPage.DisplayAlert("MVVM", "Are you sure?", "Ok", "Cancel"))
                {

                    var requestProfile = new RequestProfile();
                    requestProfile.Id = Id;
                    requestProfile.Name = Name;

                    var pageA = new PageAModel();

                    var result = await pageA.PostDataAsync(requestProfile);

                    await Application.Current.MainPage.DisplayAlert("MVVM",
                            string.Format("Profile checked! {0}", result),
                            "Ok");
                }
            }
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("MVVM", ex.Message, "Ok");
            }

            IsBusy = false;
        }
    }
}
