using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace To_Do
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel vm;
        public MainPage()
        {
            InitializeComponent();

            vm = new MainPageViewModel();
            BindingContext = vm;
        }
        void OnHighContrast(object sender, EventArgs e)
        {
            Resources["textCol"] = Color.FromHex("#ffff00");
            Resources["saveCol"] = Color.FromHex("#00ff00");
            Resources["saveTextCol"] = Color.Black;
            Resources["pageCol"] = Color.Black;
            Resources["buttonImage"] = "hc_check.png";
            Resources["noteImage"] = "hc_note.png";
        }

        void OnLight(object sender, EventArgs e)
        {
            Resources["textCol"] = Color.SlateGray;
            Resources["saveCol"] = Color.PaleGreen;
            Resources["saveTextCol"] = Color.DarkGreen;
            Resources["pageCol"] = Color.White;
            Resources["buttonImage"] = "check.png";
            Resources["noteImage"] = "note.png";
        }
    }
}
