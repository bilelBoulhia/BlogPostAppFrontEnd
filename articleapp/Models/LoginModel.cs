using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace articleapp.Models
{
   public class LoginModel
    {
        public string UserEmail { get; set; } = null!;
        public string UserHash { get; set; } = null!;
    }
}
