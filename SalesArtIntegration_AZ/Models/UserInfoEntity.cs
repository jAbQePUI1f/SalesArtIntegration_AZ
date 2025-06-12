using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesArtIntegration_AZ.Models
{
    public class UserInfoEntity
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public static class UserSharedInfo
    {
        private static UserInfoEntity _userInfo;

        public static UserInfoEntity UserInfo
        {

            get
            {
                if (_userInfo == null)
                    _userInfo = new UserInfoEntity();
                return _userInfo;
            }
            set
            {

                _userInfo = value;
            }
        }
        public static string GetToken()
        {
            return _userInfo.Token;
        }
        public static string GetUserName()
        {
            return _userInfo.UserName;

        }
        public static string GetUserPass()
        {
            return _userInfo.Password;

        }
      
    }
}
