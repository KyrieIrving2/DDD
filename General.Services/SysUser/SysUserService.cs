using General.Core.data;
using General.Core.Librarys;
using General.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace General.Services.SysUser
{
    public class SysUserService : ISysUserService
    {
        private IRepository<Entities.SysUser.SysUser> _sysUserRepository;
        private IRepository<Entities.SysUserToken.SysUserToken> _sysUserTokenRepository;
        private IMemoryCache _memoryCache;

        public SysUserService(IRepository<General.Entities.SysUser.SysUser> sysUserRepository, IRepository<Entities.SysUserToken.SysUserToken> sysUserTokenRepository, IMemoryCache memoryCache)
        {
            this._sysUserRepository = sysUserRepository;
            this._sysUserTokenRepository = sysUserTokenRepository;
            this._memoryCache = memoryCache;
        }

        /// <summary>
        /// 通过账户名查找用户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Entities.SysUser.SysUser GetUserByName(string account)
        {
            return _sysUserRepository.Table.Where(u => u.Account == account).FirstOrDefault();
        }

        /// <summary>
        /// 验证登录状态
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public (bool status, string messege, string token, General.Entities.SysUser.SysUser) IsValidateUser(string account, string password, string r)
        {
            var user = GetUserByName(account);
            if (user == null)
                return (false, "用户不存在", null, null);
            if (!user.Enabled)
                return (false, "该账号被冻结", null, null);
            if (user.LoginLock)
            {
                if (user.AllowLoginTime > DateTime.Now)
                {
                    return (false, "该账号被锁定" + (user.AllowLoginTime - DateTime.Now).ToString(), null, null);
                }
            }
            string md5Password = EncryptorHelper.GetMD5(password + r);
            if (md5Password.Equals(user.Password, StringComparison.InvariantCultureIgnoreCase))
            {
                //密码正确
                user.LoginLock = false;
                user.LastIpAddress = "";
                user.LastLoginTime = DateTime.Now;
                user.SysUserLoginLogs.Add(new Entities.SysUserLoginLog.SysUserLoginLog()
                {
                    Id = Guid.NewGuid(),
                    LoginTime = DateTime.Now,
                    Message = "登录成功",
                    IpAddress = "",
                    UserId = user.Id 
                });
                var userToken = new Entities.SysUserToken.SysUserToken()
                {
                    Id = Guid.NewGuid(),
                    ExpireTime = DateTime.Now.AddDays(15),
                    SysUserId = user.Id
                };
                user.SysUserTokens.Add(userToken);
                _sysUserRepository.dbContext.SaveChanges();
                return (true, "登录成功", userToken.Id.ToString(), user);
            }
            else
            {
                //密码错误
                user.LoginFailedNum++;
                if (user.LoginFailedNum > 5)
                {
                    user.LoginLock = true;
                    user.AllowLoginTime = DateTime.Now.AddHours(2);
                }
                user.SysUserLoginLogs.Add(new Entities.SysUserLoginLog.SysUserLoginLog()
                {
                    LoginTime = DateTime.Now,
                    IpAddress = "",
                    Message = "登录失败"
                });
                _sysUserRepository.dbContext.SaveChanges();

            }
            return (false, "登录失败", null, null);
        }

        /// <summary>
        /// 通过token获取用户
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Entities.SysUser.SysUser GetUserByToken(string token)
        {
            var user = _memoryCache.Get<Entities.SysUserToken.SysUserToken>(token);
            if (user != null)
                return user.SysUser;

            Guid tokenId = Guid.Empty;
            if (Guid.TryParse(token, out tokenId))
            {
                var tokenItem = _sysUserTokenRepository.Table.Include(x => x.SysUser).Where(t => t.Id == tokenId).FirstOrDefault();
                _memoryCache.Set<Entities.SysUserToken.SysUserToken>(tokenId, tokenItem);
                return tokenItem.SysUser;
            }
            return null;
        }

        /// <summary>
        /// 添加测试数据
        /// </summary>
        public void CreateUser()
        {
            _sysUserRepository.Entities.Add(new Entities.SysUser.SysUser()
            {
                Id = Guid.NewGuid(),
                Account = "admin",
                AllowLoginTime = DateTime.Now,
                CreationTime = DateTime.Now,
                DeletedTime = null,
                Enabled = true,
                Email = "11@qq.com",
                IsAdmin = false,
                IsDeleted = false,
                LastActivityTime = DateTime.Now,
                LastIpAddress = "",
                LastLoginTime = DateTime.Now,
                LoginFailedNum = 0,
                LoginLock = false,
                MobilePhone = "18665392508",
                ModifiedTime = DateTime.Now,
                Name = "xs",
                Password = "Xs@123",
                Salt = "123",
                Sex = "男"
            });
            _sysUserRepository.dbContext.SaveChanges();
        }
    }
}
