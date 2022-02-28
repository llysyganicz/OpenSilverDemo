using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OpenSilverDemo.Messages;
using OpenSilverDemo.Models;
using OpenSilverDemo.Services;

namespace OpenSilverDemo.ViewModels
{
    public class EditViewModel : ObservableObject
    {
        private readonly INotesService _notesService;
        private readonly IDialogService _dialogService;
        private Guid _id;

        public EditViewModel(INotesService notesService, IDialogService dialogService)
        {
            _notesService = notesService;
            _dialogService = dialogService;

            Cancel = new RelayCommand(CancelExecute);
            Save = new RelayCommand(SaveExecute, SaveCanExecute);

            WeakReferenceMessenger.Default.Register<EditViewModel, EditNoteMessage>(this, (r, m) =>
            {
                _id = m.Value;
                if (_id != Guid.Empty)
                {
                    var note = _notesService.GetNote(_id);
                    Title = note.Title;
                    Content = note.Content;
                }
                else
                {
                    Title = string.Empty;
                    Content = string.Empty;
                }
            });
        }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
                Save.NotifyCanExecuteChanged();
            }
        }

        private string _content;
        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
                Save.NotifyCanExecuteChanged();
            }
        }

        public RelayCommand Save { get; }
        private void SaveExecute()
        {
            if (_id == Guid.Empty)
            {
                _notesService.AddNote(Title, Content);
            }
            else
            {
                _notesService.UpdateNote(new Note { Id = _id, Title = Title, Content = Content });
            }

            WeakReferenceMessenger.Default.Send(new RefreshNotesMessage(null));
            _dialogService.CloseEditNote();
        }

        private bool SaveCanExecute() => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Content);

        public RelayCommand Cancel { get; }

        private void CancelExecute()
        {
            _dialogService.CloseEditNote();
        }
    }
}
