using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace articleapp.Models
{
    public class TokenModel
    {
   
        public  string token { get; set; }
      
        public  string refreshToken { get; set; }
    }
}
