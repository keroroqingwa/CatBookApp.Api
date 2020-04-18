using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backstage.Api.Models.Dto
{
    public class AccountLoginInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
    }
}
