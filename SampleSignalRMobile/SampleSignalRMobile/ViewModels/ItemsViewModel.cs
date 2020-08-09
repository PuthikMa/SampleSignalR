using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using SampleSignalRMobile.Models;
using SampleSignalRMobile.Views;
using Microsoft.AspNetCore.SignalR.Client;
using Xamarin.Essentials;

namespace SampleSignalRMobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        HubConnection hubConnection;
        bool connected;
        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });

            hubConnection = new HubConnectionBuilder()
                .WithUrl($"{App.AzureBackendUrl}/hubs/messages").Build();
            hubConnection.On<string>("NewItem", (item) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        var newItem = new Item();
                        newItem.Description = item;
                        Items.Insert(0, newItem);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                });
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
       

            IsBusy = true;

            try
            {
                if (!connected)
                    await hubConnection.StartAsync();

                connected = true;
                await App.Current.MainPage.DisplayAlert("", "Connected", "OK");

            }
            catch (Exception ex)
            {

                throw;
            }

            try
            {
               
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}