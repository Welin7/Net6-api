using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _IMessage;

        public ServiceMessage(IMessage iMessage)
        {
            _IMessage = iMessage;
        }

        public async Task Add(Message message)
        {
            var tituleValidate = message.ValidateStringProperty(message.Title, "Title");
            if(tituleValidate)
            {
                message.RegistrationDate = DateTime.Now;
                message.ChangeDate = DateTime.Now;
                message.Active = true;
                await _IMessage.Add(message);
            }
        }

        public async Task<List<Message>> ListMessageActive()
        {
            return await _IMessage.ListMessage(l => l.Active);
        }

        public async Task Update(Message message)
        {
            var tituleValidate = message.ValidateStringProperty(message.Title, "Title");
            if (tituleValidate)
            {
                message.ChangeDate = DateTime.Now;
                await _IMessage.Update(message);
            }
        }
    }
}
