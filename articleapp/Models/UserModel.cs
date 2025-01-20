using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace articleapp.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFamilyName { get; set; }
        public long UserPhoneNumber { get; set; }
        public DateTime UserBirthDay { get; set; }
        public string UserHash { get; set; }
        public string UserSalt { get; set; }
        public string UserImage { get; set; }
        public string UserEmail { get; set; }
        public List<object> Articles { get; set; }
        public List<object> Comments { get; set; }
        public List<object> Reports { get; set; }
        public List<object> Articles1 { get; set; }
        public List<object> ArticlesNavigation { get; set; }
        public List<object> CommentsNavigation { get; set; }
        public List<object> Followers { get; set; }
        public List<object> Hobbies { get; set; }
        public List<object> Users { get; set; }

    }

}