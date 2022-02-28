using Microsoft.Extensions.DependencyInjection;

namespace OpenSilverDemo.ViewModels
{
    public class ViewModelLocator
    {
        public NotesViewModel NotesViewModel => App.Current.Services.GetService<NotesViewModel>();
        public EditViewModel EditViewModel => App.Current.Services.GetService<EditViewModel>();
    }
}
