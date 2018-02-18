using FortisInternational_bus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_data
{
    public class LoginInformation
    {
        public static string userSignedIn = "";
        public static Customer customerSignedIn;
        public static string loginInfoPath = "LoginInformation.txt";
        public static Dictionary<string, string> loginInfoDict = new Dictionary<string, string>();

        public static void SaveLoginInformation()
        {
            using (StreamWriter streamWriter = new StreamWriter(loginInfoPath))
            {
                foreach (KeyValuePair<string, string> user in loginInfoDict)
                {
                    streamWriter.WriteLine(user.Key);
                    streamWriter.WriteLine(user.Value);
                }
            }
        }

        public static void LoadLoginInformation()
        {
            string username, password;

            loginInfoDict.Clear();

            if (!File.Exists(loginInfoPath))
            {
                using (StreamWriter streamWriter = new StreamWriter(loginInfoPath))
                {
                    streamWriter.WriteLine("admin");
                    streamWriter.WriteLine("admin");
                }
            }

            using (StreamReader streamReader = new StreamReader(loginInfoPath))
            {
                do
                {
                    username = streamReader.ReadLine();
                    password = streamReader.ReadLine();

                    loginInfoDict.Add(username, password);
                } while (!streamReader.EndOfStream);

            }

        }

    }

}
