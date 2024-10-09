using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.UserModel;

namespace Api.Interfaces
{
    public interface ITokenServices
    {
        public string CreateToken(User user);
    }
}