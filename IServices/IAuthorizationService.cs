using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IServices
{
    public interface IAuthorizationService
    {
        bool TryValidate(string login, string password, out Customer customer);
    }
}
