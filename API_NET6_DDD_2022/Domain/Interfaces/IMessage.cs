using Domain.Interfaces.Generics;
using Entities.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IMessage : IGeneric<Message>
    {
        Task<List<Message>> ListMessage(Expression<Func<Message, bool>> exMessage);
    }
}
