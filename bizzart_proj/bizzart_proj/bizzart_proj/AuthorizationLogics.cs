using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bizzart_proj
{
    class AuthorizationLogics
    {
        public static User LoginAuth(string Login, string Password)
        {
            ArtGalletyContext mc = new ArtGalletyContext();
            try
            {
                User u1 = mc.Users.Where(c => c.Login == Login).FirstOrDefault();
                if (u1.Password.Equals(Password))
                {
                    return u1;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static bool CheckForRole(string login, string password, List<int> Ids)
        {
            return Ids.Contains(AuthorizationLogics.LoginAuth(login, password).RoleId);
        }
    }
}
