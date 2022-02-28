using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using OpenSilverDemo.Messages;
using OpenSilverDemo.Models;
using OpenSilverDemo.Services;

namespace OpenSilverDemo.ViewModels
{
    public class NotesViewModel : ObservableObject
    {
        private readonly INotesService _notesService;
        private readonly IDialogService _dialogService;

        public NotesViewModel(INotesService notesService, IDialogService dialogService)
        {
            _notesService = notesService;
            _dialogService = dialogService;

            AddNote = new RelayCommand(AddNoteExecute);
            EditNote = new RelayCommand<Note>(EditNoteExecute);
            DeleteNote = new RelayCommand<Note>(DeleteNoteExecute);

            Notes = new ObservableCollection<Note>(_notesService.GetNotes());

            WeakReferenceMessenger.Default.Register<NotesViewModel, RefreshNotesMessage>(this, (r, m) => RefreshNotes());
        }

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddNote { get; }
        private void AddNoteExecute()
        {
            _dialogService.OpenEditNote();
            WeakReferenceMessenger.Default.Send(new EditNoteMessage(Guid.Empty));
        }

        public RelayCommand<Note> EditNote { get; }
        private void EditNoteExecute(Note note)
        {
            _dialogService.OpenEditNote();
            WeakReferenceMessenger.Default.Send(new EditNoteMessage(note.Id));
        }

        public RelayCommand<Note> DeleteNote { get; }
        private void DeleteNoteExecute(Note note)
        {
            _notesService.DeleteNote(note.Id);
            RefreshNotes();
        }

        private void RefreshNotes()
        {
            Notes.Clear();

            foreach (var note in _notesService.GetNotes())
                Notes.Add(note);

            OnPropertyChanged(nameof(Notes));
        }
    }
}
