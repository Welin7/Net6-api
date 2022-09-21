using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.InterfaceServices
{
    public interface IServiceMessage
    {
        Task Add(Message message);

        Task Update(Message message);

        Task<List<Message>> ListMessageActive();
    }
}
