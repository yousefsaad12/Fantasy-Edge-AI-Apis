using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    public class UserResponse
    {
         public string userName { get; set; }
         public string email { get; set; }
         public string token { get; set; }
    }
}