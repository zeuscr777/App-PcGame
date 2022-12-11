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
    public partial class DataPremiery : ContentPage
    {
        public DataPremiery(string nazwaGry, string dzien, string miesiac, string rok)
        {
            InitializeComponent();

            myLabelDataPremiery.Text = $"DATA PREMIERY GRY: {nazwaGry}";
            myLabelDzien.Text = $"DZIEŃ: {dzien}";
            myLabelMiesiac.Text = $"MIESIĄC: {miesiac}";
            myLabelRok.Text = $"ROK: {rok}";
        }
    }
}