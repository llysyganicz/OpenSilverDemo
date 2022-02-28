using System;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace OpenSilverDemo.Messages
{
    public class EditNoteMessage : ValueChangedMessage<Guid>
    {
        public EditNoteMessage(Guid value) : base(value)
        {
        }
    }
}
