using Microsoft.Extensions.DependencyInjection;

namespace MauiSqliteDemoEstela
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();
            MainPage = mainPage;
        }
    }
}
