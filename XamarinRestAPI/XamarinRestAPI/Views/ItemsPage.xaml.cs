using System;
using XamarinRestAPI.ViewModels;
using Xamarin.Forms;
using XamarinRestAPI.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace XamarinRestAPI.Views
{
    public partial class ItemsPage : ContentPage
    {       
        protected CountryService DataService { get; set; }

        public ObservableCollection<CountryViewModel> Countries { get; set; }
        public ICommand LoadCommand { get; set; }

        public NavigationService NavigationService { get; set; }

        public ItemsPage()
        {
            Countries = new ObservableCollection<CountryViewModel>();
            DataService = new CountryService();
            LoadCommand = new Command(obj => LoadCountries());
            NavigationService = new NavigationService(Navigation);

            InitializeComponent();

            List.ItemTapped += (sender, e) => {
                var viewModel = ((ListView)sender).BindingContext as CountryViewModel;

                if (viewModel != null && viewModel.BrowseCommand != null)
                {
                    viewModel.BrowseCommand.Execute(((ListView)sender).BindingContext);
                }
            };
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Countries.Any())
            {
                return;
            }
            LoadCountries();
        }
                
        private async void BrowseCountry(CountryViewModel obj)
        {
            await NavigationService.PushAsync<CountryPage>(obj);
        }

        private async void LoadCountries()
        {
            if (Countries.Any())
            {
                Indicator.IsVisible = false;
            }
            LoadButton.Text = "Atualizando";
            IsBusy = true;
            List.IsVisible = true;
            Response.Text = string.Empty;

            try
            {
                var result = await DataService.GetCountries();

                Countries.Clear();

                foreach (var item in result)
                {
                    Countries.Add(new CountryViewModel(item)
                    {
                        FlagSource = ImageSource.FromUri(DataService.GetFlagSource(item.Alpha2Code)),
                        BrowseCommand = new Command<CountryViewModel>(BrowseCountry)
                    });
                }
                Response.Text = string.Empty;
                StatusPanel.IsVisible = false;
            }
            catch (Exception ex)
            {
                Response.Text = ex.Message;
                StatusPanel.IsVisible = true;
                List.IsVisible = false;
            }
            finally
            {
                IsBusy = false;
                LoadButton.Text = "Atualizar";
            }
        }

    }
}
