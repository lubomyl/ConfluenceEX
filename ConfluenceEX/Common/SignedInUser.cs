using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfluenceEX.Common
{

    //TODO fill in credentials from VSCredentials service
    public static class SignedInUser
    {

        private static string _username;
        private static string _password;

        public static bool IsComplete()
        {
            bool ret = true;

            if (_username == null || _password == null)
            {
                ret = false;
            }
            else
            {
                ret = true;
            }

            return ret;
        }

        #region SignedInUser Members

        public static string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public static string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        #endregion
    }
}
