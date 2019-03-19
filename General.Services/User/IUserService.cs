using System;
using System.Collections.Generic;
using System.Text;

namespace General.Services.User
{
    public interface IUserService
    {
        List<General.Entities.User.User> GetAllUser();
    }
}
