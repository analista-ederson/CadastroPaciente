using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheOne.Clinica.API.ViewModels
{
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimViewModel> Claims { get; set; }
    }
}
