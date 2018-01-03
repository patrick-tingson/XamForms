using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFormsMVVM.Page
{ 
    public partial class PageA : ContentPage
    {
        public PageA()
        {
            InitializeComponent();
            BindingContext = new ViewModel.PageAViewModel();
        }
    }
}