using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using General.Core.data;
using General.Entities;
using General.Entities.User;

namespace General.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<Entities.User.User> _repository;
        public UserService(IRepository<Entities.User.User> repository)
        {
            this._repository = repository;
        }

        public List<Entities.User.User> GetAllUser()
        {
            return _repository.GetAll();
        }
    }
}
