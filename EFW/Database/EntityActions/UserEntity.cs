﻿using Final.EFW.Database;
using System.Xml.Linq;
using Final;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Identity;
using Final.EFW.Entities;
using static Final.EFW.Database.Core;
namespace Final.EFW.Database.EntityActions
{
    internal class UserEntity
    {      
        protected internal static bool Check(string _login, DB _db)
        {
            User? _user = _db.context.Users.FirstOrDefault(x => x.Login == _login);
            if (_user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected internal static void Register(string _login, DB _db, string _password, string _firstName, string _lastName, string _email, Role? _role = null)
        {
            if (_role == null)
            {
                _role = _db.context.Roles.FirstOrDefault(x => x.Name == "Пользователи");
            }
            User _user = new User();
            _user.Var(_login, _email, _password, _firstName, _lastName);
            _db.context.Users.Add(_user);
            _db.context.SaveChanges();
            if (_role != null)
            {
                UserRoleEntity.AddRoleToUser(_db, _user, _role);
            }
        }
        protected internal static User? GetByLogin(string _login, DB _db)
        {
            User? _user = _db.context.Users.FirstOrDefault(x => x.Login == _login);
            return _user;
        }
        protected internal static User? Authorization(string _login, string _password, DB _db)
        {
            User? _user = _db.context.Users.FirstOrDefault(x => x.Login == _login && x.PasswordHash == _password) ?? null;
            return _user;
        }
    }
}
