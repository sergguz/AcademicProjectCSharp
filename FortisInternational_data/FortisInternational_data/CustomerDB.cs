using FortisInternational_bus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FortisInternational_data
{
    public class CustomerDB
    {
        public static string customerDBPath = "CustomerDatabase.bin";
        public static Dictionary<string, Customer> customerDatabase = new Dictionary<string, Customer>();

        public static void SaveDatabase()
        {
            if (File.Exists(customerDBPath))
            {
                File.Delete(customerDBPath);
            }

            FileStream fileStream = new FileStream(customerDBPath, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            foreach (Customer customer in customerDatabase.Values)
            {
                binaryFormatter.Serialize(fileStream, customer);
            }
            fileStream.Close();
        }

        public static void LoadDatabase()
        {
            if (File.Exists(customerDBPath))
            {
                FileStream fileStream = new FileStream(customerDBPath, FileMode.Open, FileAccess.Read);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                customerDatabase.Clear();

                while (fileStream.Position < fileStream.Length)
                {
                    Customer customer = (Customer)binaryFormatter.Deserialize(fileStream);
                    customerDatabase.Add(customer.CustomerID.ToString(), customer);
                }
                fileStream.Close();
            }

        }

    }
}
