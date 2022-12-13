using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projekt1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SzczegolyGry : ContentPage
    {
        string nazwaGry, dzien, miesiac, rok, platforma, trybGry;

        public SzczegolyGry(Root Informacje, int id)
        {
            InitializeComponent();



            //Jeżeli jest połączenie z Internetem
            if (CzyJestInternet())
            {
                for (int i = 0; i < 10; i++)
                {
                    myImage.Source = new UriImageSource
                    {
                        Uri = new Uri(Informacje.gry[i].okladka),
                        CachingEnabled = true,
                        CacheValidity = new TimeSpan(30, 0, 0, 0)
                    };
                }

                //myImage.Source = Informacje.gry[id].okladka;
                myImage.Source = new UriImageSource
                {
                    Uri = new Uri(Informacje.gry[id].okladka),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(30, 0, 0, 0)
                };
            }
            else
            {

                myImage.Source = new UriImageSource
                {
                    Uri = new Uri(Informacje.gry[id].okladka),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(30, 0, 0, 0)
                };
                //myImage.Source = ImageSource.FromResource("Projekt1.Images.cover.png");

            }

            myLabelOpisGry.Text = Informacje.gry[id].opisGry;
            myLabelGatunek.Text = $"Gatunek: {Informacje.gry[id].gatunek}";
            myLabelCena.Text = $"Cena: {Informacje.gry[id].cena}";

            nazwaGry = Informacje.gry[id].nazwaGry;
            dzien = Informacje.gry[id].dataPremiery.dzien;
            miesiac = Informacje.gry[id].dataPremiery.miesiac;
            rok = Informacje.gry[id].dataPremiery.rok;
            platforma = Informacje.gry[id].inneDane.platforma;
            trybGry = Informacje.gry[id].inneDane.trybGry;
        }

        public bool CzyJestInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }

        private void Button_Clicked_DataPremiery(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DataPremiery(nazwaGry, dzien, miesiac, rok));
        }

        private void Button_Clicked_InneDane(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InneDane(nazwaGry, platforma, trybGry));
        }
    }
}