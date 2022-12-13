using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MonkeyCache.FileStore;
using Xamarin.Essentials;
using System.Diagnostics;
using MonkeyCache;
using System.Net;
using System.Net.NetworkInformation;

using Plugin.Connectivity;
using Plugin.DownloadManager;

namespace Projekt1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrzegladajGry : ContentPage
    {
        static HttpClient client = new HttpClient();
        List<string> myList = new List<string> { };
        static String url = "https://raw.githubusercontent.com/zeuscr777/App-PcGame/main/games.json";
        Root DoPrzekazania;
        static Root aaa;
        static Root bbb;
        static string sciezka = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);


        IDictionary<int, string> slownikGier = new Dictionary<int, string>();

        public PrzegladajGry()
        {
            InitializeComponent();
            Barrel.ApplicationId = "zeuscr";

            //Jeżeli jest połączenie z Internetem
            if(CzyJestInternet())
            {
                DisplayAlert("Uwaga!", $"Masz połączenie z Internetem: {CzyJestInternet()}", "OK");
                DoCache();
                czaryJson();
            }
            else
            {
                DisplayAlert("Uwaga!", $"Nie masz połączenia z Internetem: {CzyJestInternet()}", "OK");
                DoCache();
                czaryJsonInOffline();
            }       
        }


        
        public bool CzyJestInternet()
        {
            return CrossConnectivity.Current.IsConnected;
        }
        
        public async Task DoCache()
        {
            bbb = await GetCache();
        }

        public static Task<Root> GetCache() =>
            GetAsync<Root>(url, "get-cache");

        static async Task<T> GetAsync<T>(string url, string key, int mins = 30, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                json = Barrel.Current.Get<string>(key);
            }
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
            {
                json = Barrel.Current.Get<string>(key);
            }         

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await client.GetStringAsync(url);

                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }

                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

        public async Task czaryJsonInOffline()
        {        
            DoPrzekazania = bbb;

            for (int i = 0; i < 10; i++)
            {
                myList.Add(bbb.gry[i].nazwaGry);
                slownikGier.Add(i, bbb.gry[i].nazwaGry);
            }
            myListView.ItemsSource = myList;
        }


            public async Task czaryJson()
        {
            string response = await client.GetStringAsync(url);

            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(response);

            DoPrzekazania = myDeserializedClass;
            
            for (int i = 0; i< 10; i++)
            {
                myList.Add(myDeserializedClass.gry[i].nazwaGry);
                slownikGier.Add(i, myDeserializedClass.gry[i].nazwaGry);
            }
            myListView.ItemsSource = myList;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ListView lv = (ListView)sender;

            var selected = lv.SelectedItem; //Zwróciło nazwę gry klikniętej

            var id = slownikGier.FirstOrDefault(x => x.Value == selected.ToString()).Key;

            Navigation.PushAsync(new SzczegolyGry(DoPrzekazania, id));
        }
    }



    public class Gry
    {
        public string id { get; set; }
        public string nazwaGry { get; set; }
        public string okladka { get; set; }
        public string gatunek { get; set; }
        public string cena { get; set; }
        public DataPremiery2 dataPremiery { get; set; }
        public string opisGry { get; set; }
        public InneDane2 inneDane { get; set; }
    }

    public class DataPremiery2
    {
        public string dzien { get; set; }
        public string miesiac { get; set; }
        public string rok { get; set; }
    }

    public class InneDane2
    {
        public string platforma { get; set; }
        public string trybGry { get; set; }
    }

    public class Root
    {
        public List<Gry> gry { get; set; }
    }

}