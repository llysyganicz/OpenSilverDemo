using OpenSilverDemo.Views;

namespace OpenSilverDemo.Services
{
    public class DialogService : IDialogService
    {
        private EditWindow _window;

        public void OpenEditNote()
        {
            if (_window == null)
                _window = new EditWindow();

            _window.Show();
        }

        public void CloseEditNote()
        {
            _window.DialogResult = false;
        }
    }
}
