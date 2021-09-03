using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Berezka.Data.Model;

namespace Berezka.Data.Service
{
    public class MessageService : IMessageService
    {
        private MyDbContext _db;
        public MessageService(MyDbContext db)
        {
            _db = db;
        }
        private IQueryable<Message> Get()
        {
            return _db.Messages.Where(x => x.Visible);

        }

        public Message[] GetFromGuid(Guid AccountId)
        {
            var response = new Message[] { };
            response = Get().Where(x => x.To == AccountId).AsNoTracking().ToArray();
            if (response.Any())
            {
                response.OrderByDescending(x => x.CreateAt)
                    .ThenByDescending(x => x.messageStatus == EnumType.MessageStatus.Unread);
            }
            return response;
        }

        public int GetCountMessages(Guid AccountId)
        {
            return _db.Messages.AsNoTracking().Count(x => x.To == AccountId);
        }

        public void AddMessage(Message message)
        {
            _db.Messages.Add(message);
            _db.SaveChanges();
        }


    }
}
