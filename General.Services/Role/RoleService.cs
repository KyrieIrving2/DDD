using General.Core.data;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Services.Role
{
    public class RoleService: IRoleService
    {
        private readonly IRepository<Entities.User.User> _repository;
        public RoleService(IRepository<Entities.User.User> repository)
        {
            this._repository = repository;
        }
    }
}
