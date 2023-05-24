using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Miras
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            CollectionView.ItemsSource = await App.Database.GetPersonAsync();
        }

        private async void AddPerson(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(SurName.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhiteSpace values is invalid", "Ok");
                return;
            }
            else if(!int.TryParse(Age.Text,out int age))
            {
                await DisplayAlert("Invalid", "Age can be only numbers", "Ok");
                return;
            }
            Person p = new Person(Name.Text,SurName.Text,Int32.Parse(Age.Text));
            await App.Database.SavePeopleAsync(p);
            Name.Text = string.Empty;
            SurName.Text = string.Empty;
            Age.Text = string.Empty;
            CollectionView.ItemsSource = await App.Database.GetPersonAsync();

        }

        private async void DropTable(object sender, EventArgs e)
        {
            
            var selected=CollectionView.SelectedItem;

            if(selected==null)
            {
                await DisplayAlert("Invalid", "Select Person!", "Ok");
            }
            else
            {
                await App.Database.DropDbAsync((Person)selected);
                CollectionView.ItemsSource = await App.Database.GetPersonAsync();
            }
            
        }

        private void EditPerson(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var Person = menuItem.CommandParameter as Person;
            Navigation.PushAsync(new Edit(Person));
            
            
        }
    }
}
