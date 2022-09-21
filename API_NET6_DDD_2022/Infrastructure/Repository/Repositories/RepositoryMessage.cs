﻿using Domain.Interfaces;
using Entities.Entities;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Repositories
{
    public class RepositoryMessage : RepositoryGenerics<Message>, IMessage
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        public RepositoryMessage()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<Message>> ListMessage(Expression<Func<Message, bool>> exMessage)
        {
            using(var context = new ContextBase(_OptionsBuilder))
            {
                return await context.Message.Where(exMessage).AsNoTracking().ToListAsync();
            }
        }
    }
}
