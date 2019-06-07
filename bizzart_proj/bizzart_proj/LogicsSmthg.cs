using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bizzart_proj
{
    class LogicsSmthg
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
            var desiredId = LogicsSmthg.LoginAuth(login, password).RoleId;
            foreach (int id in Ids)
            {
                if (desiredId == id)
                {
                    return true;
                }
            }
            return false;
        }

        public class ImageRepository
        {
            ArtGalletyContext db = new ArtGalletyContext();
        }
    }
}
