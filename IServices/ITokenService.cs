using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IServices
{
    public interface ITokenService
    {
        string CreateToken(Customer customer);
    }
}
