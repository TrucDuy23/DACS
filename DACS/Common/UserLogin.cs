using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DACS.Common
{
    [Serializable]
    public class UserLogin
    {

        public long UserID { set; get; }
        public string UserName { set; get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string GroupID { set; get; }
    }
}