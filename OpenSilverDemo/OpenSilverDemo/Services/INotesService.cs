using System;
using System.Collections.Generic;
using OpenSilverDemo.Models;

namespace OpenSilverDemo.Services
{
    public interface INotesService
    {
        List<Note> GetNotes();
        Note GetNote(Guid id);
        void AddNote(string title, string content);
        void UpdateNote(Note note);
        void DeleteNote(Guid id);
    }
}
