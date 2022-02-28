using System;
using System.Collections.Generic;
using LiteDB;
using OpenSilverDemo.Models;

namespace OpenSilverDemo.Services
{
    public class NotesService : INotesService
    {
        private const string dbFile = @"C:\NotesDb";
        public List<Note> GetNotes()
        {
            using (var db = new LiteDatabase(dbFile))
            {
                var notes = db.GetCollection<Note>("notes");

                return notes.Query().OrderBy(note => note.Title).ToList();
            }
        }

        public Note GetNote(Guid id)
        {
            using (var db = new LiteDatabase(dbFile))
            {
                var notes = db.GetCollection<Note>("notes");

                return notes.FindById(id);
            }
        }

        public void AddNote(string title, string content)
        {
            using (var db = new LiteDatabase(dbFile))
            {
                var notes = db.GetCollection<Note>("notes");

                notes.Insert(new Note { Id = Guid.NewGuid(), Title = title, Content = content });
            }
        }

        public void UpdateNote(Note note)
        {
            using (var db = new LiteDatabase(dbFile))
            {
                var notes = db.GetCollection<Note>("notes");

                var noteToEdit = notes.FindById(note.Id);
                if (noteToEdit != null)
                {
                    notes.Update(note);
                }
            }
        }

        public void DeleteNote(Guid id)
        {
            using (var db = new LiteDatabase(dbFile))
            {
                var notes = db.GetCollection<Note>("notes");

                notes.Delete(id);
            }
        }
    }
}
