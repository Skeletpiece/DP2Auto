﻿using DP2_Auto_App.Models.RestServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DP2_Auto_App.Contents
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GoalsAchieved : ContentPage
	{
        private List<Objective> objectives { get; set; }
        public static bool AchievedLoop;
        public GoalsAchieved ()
		{
			InitializeComponent ();
            AchievedLoop = true;
            initializeValues();
        }
        
        private async void initializeValues()
        {
            while (RestService.client != null && AchievedLoop)
            {
                objectives = await webService.rest.listAchievedGoals();
                MyListView.ItemsSource = objectives;
                //MyListView.IsPullToRefreshEnabled = true;

                await Task.Delay(1000);
            }
        }
        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Click", "Objetivo seleccionado.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        /*private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }*/
    }
}