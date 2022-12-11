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
    public partial class InneDane : ContentPage
    {
        public InneDane(string nazwaGry, string platforma, string trybGry)
        {
            InitializeComponent();

            myLabelInneDane.Text = $"INNE DANE GRY: {nazwaGry}";
            myLabelPlatforma.Text = $"PLATFORMA: {platforma}";
            myLabelTrybGry.Text = $"TRYB GRY: {trybGry}";
        }
    }
}