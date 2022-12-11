using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projekt1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StronaGlowna : ContentPage
    {
        public StronaGlowna()
        {
            InitializeComponent();
        }

        private void Button_Clicked_PrzegladajGry(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrzegladajGry());
        }

        private void Button_Clicked_oAutorze(object sender, EventArgs e)
        {
            Navigation.PushAsync(new oAutorze());
        }
    }
}