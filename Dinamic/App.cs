using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dinamic
{
    public class App : Application
    {
        public App()
        {
            //MainPage = new NavigationPage(new DinamicPage1());
            MainPage = new NavigationPage(new DinamicPage("FORM TITLE",null));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
