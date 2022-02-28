using CommunityToolkit.Mvvm.Messaging.Messages;

namespace OpenSilverDemo.Messages
{
    public class RefreshNotesMessage : ValueChangedMessage<object>
    {
        public RefreshNotesMessage(object value) : base(value)
        {
        }
    }
}
