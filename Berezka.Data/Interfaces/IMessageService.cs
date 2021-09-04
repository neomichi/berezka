using System;
using Berezka.Data.Model;
using Berezka.Data.ViewModel;

namespace Berezka.Data.Service
{
    public interface IMessageService
    {
        int AddMessage(MessageView message);
        int GetCountMessages(Guid AccountId);
        Message[] GetFromGuid(Guid AccountId);
        
        int DeleteMessage(MessageView message);
    }
}