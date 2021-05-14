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

            // call first or the icons won't show up
            vm.NoteIcon = "note.png"; 
            vm.CheckboxIcon = "check.png";
        }

        /* high contrast theme */
        void OnHighContrast(object sender, EventArgs e)
        {
            Resources["textCol"] = Color.FromHex("#ffff00");
            Resources["saveCol"] = Color.FromHex("#00ff00");
            Resources["saveTextCol"] = Color.Black;
            Resources["pageCol"] = Color.Black;

            vm.NoteIcon = "hc_note.png";
            vm.CheckboxIcon = "hc_check.png";
        }

        /* light theme */
        void OnLight(object sender, EventArgs e)
        {
            Resources["textCol"] = Color.SlateGray;
            Resources["saveCol"] = Color.PaleGreen;
            Resources["saveTextCol"] = Color.DarkGreen;
            Resources["pageCol"] = Color.White;

            vm.NoteIcon = "note.png";
            vm.CheckboxIcon = "check.png";
        }
    }
}
