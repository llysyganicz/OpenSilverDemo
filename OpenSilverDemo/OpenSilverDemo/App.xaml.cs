using System;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using OpenSilverDemo.Services;
using OpenSilverDemo.ViewModels;
using OpenSilverDemo.Views;

namespace OpenSilverDemo
{
    public sealed partial class App
    {
        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        public App()
        {
            this.InitializeComponent();

            Services = ConfigureServices();

            var mainPage = new NotesPage();
            Window.Current.Content = mainPage;
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<INotesService, NotesService>();

            services.AddTransient<NotesViewModel>();
            services.AddTransient<EditViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
