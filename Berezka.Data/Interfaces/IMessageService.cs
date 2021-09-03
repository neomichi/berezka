using System;
using Berezka.Data.Model;

namespace Berezka.Data.Service
{
    public interface IMessageService
    {
        void AddMessage(Message message);
        int GetCountMessages(Guid AccountId);
        Message[] GetFromGuid(Guid AccountId);
    }
}