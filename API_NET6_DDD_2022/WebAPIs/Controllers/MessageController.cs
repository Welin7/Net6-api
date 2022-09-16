using AutoMapper;
using Domain.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIs.Models;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IMessage _IMessage;
  
        public MessageController(IMapper IMapper, IMessage IMessage)
        {
            this._IMapper = IMapper;
            this._IMessage = IMessage;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Add")]
        public async Task<List<Notifications>> Add(MessageViewModel message)
        {
            message.UserId = await ReturnLoggedInUser();
            var messageMap = _IMapper.Map<Message>(message);
            await _IMessage.Add(messageMap);
            return messageMap.NotificationsList;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Update")]
        public async Task<List<Notifications>> Update(MessageViewModel message)
        {
            var messageMap = _IMapper.Map<Message>(message);
            await _IMessage.Update(messageMap);
            return messageMap.NotificationsList;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/Delete")]
        public async Task<List<Notifications>> Delete(MessageViewModel message)
        {
            var messageMap = _IMapper.Map<Message>(message);
            await _IMessage.Delete(messageMap);
            return messageMap.NotificationsList;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/GetById")]
        public async Task<MessageViewModel> GetById(Message message)
        {
            message = await _IMessage.GetEntityById(message.Id);
            var messageMap = _IMapper.Map<MessageViewModel>(message);
            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpPost("/api/List")]
        public async Task<List<MessageViewModel>> List()
        {
            var messages = await _IMessage.GetAll();
            var messageMap = _IMapper.Map<List<MessageViewModel>>(messages);
            return messageMap;
        }

        private async Task<string> ReturnLoggedInUser()
        {
            if(User != null)
            {
                var userId = User.FindFirst("userId");
                return userId.Value;
            }

            return string.Empty;
        }
    }
}
