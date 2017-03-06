using XamarinRestAPI.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinRestAPI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                //inclusão de itens de navegação
                Children =
                {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Países",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    }
                }
            };
        }
    }
}
