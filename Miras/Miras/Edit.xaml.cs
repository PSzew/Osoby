using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Miras
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Edit : ContentPage
    {
        Person p2;
        public Edit(Person p)
        {
            InitializeComponent();
            Name.Text = p.Imie;
            Surname.Text = p.Nazwisko;
            Age.Text = p.Wiek.ToString();
            p2 = p;
        }

        private async void edit(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Name.Text) || string.IsNullOrWhiteSpace(Surname.Text))
            {
                await DisplayAlert("Invalid", "Blank or WhiteSpace values is invalid", "Ok");
                return;
            }
            else if (!int.TryParse(Age.Text, out int age))
            {
                await DisplayAlert("Invalid", "Age can be only numbers", "Ok");
                return;
            }
            p2.Imie = Name.Text;
            p2.Nazwisko = Surname.Text;
            p2.Wiek = Int32.Parse(Age.Text);
            await App.Database.EditPeopleAsync(p2);
            await Navigation.PushAsync(new MainPage());
        }
    }
}