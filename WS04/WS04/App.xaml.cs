using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using WS04.Views;

namespace WS04
{
    public partial class App : Application
    {
        public static String DbName;        //Adicionado
        public static String DbPath;        //Adicionado

        //Esse aqui é pra quando abrir a primeira vez
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PageHome()); // mudar aqui e adicionar
        }
        //Esse aqui é pra quando abrir outras vezes, sem ser a primeira
        public App(string dbPath, string dbName)
        {
            InitializeComponent();
            App.DbName = dbName;
            App.DbPath = dbPath;
            MainPage = new NavigationPage(new PageHome());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
