using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using waficash.Models;
using waficash.Services;

namespace waficash
{
    public class Program
    {

        static void Main(string[] args)
        {
            List<User> users = new List<User>();
            if (File.Exists("userData.json") == true)
            {
                using (StreamReader r = new StreamReader("userData.json"))
                {
                    string json = r.ReadToEnd();
                    users = JsonConvert.DeserializeObject<List<User>>(json);
                }
            }


            /////////////// TEST CASE
            User data = new User();
            data.AccountBalance = 0;
            data.AccountNumber = 234233433;
            data.Name = "oladapo obgae";

            var seerec = users.Find(x => x.AccountNumber == data.AccountNumber);
            if (seerec == null)
            {
                users.Add(data);
                CreateUser(users);
            }
            Console.WriteLine("Account already exist");
            Console.ReadLine();
            ///////////////////////////////////
            /// TEST CASE




        }
        public static bool CreateUser(List<User> data)
        {
            TransactionService obj = new TransactionService();
            var res = obj.CreateUser(data);
            if (res == true)
            {
                Console.WriteLine("Account Opened sucessfully");
                Console.ReadLine();
                return true;
            }
            Console.WriteLine("Account not Opened");
            Console.ReadLine();
            return false;

        }

        public static bool Deposit(TransactionInfo data)
        {
            TransactionService obj = new TransactionService();
            var res = obj.Deposit(data);
            if (res == true)
            {
                Console.WriteLine("Account Deposited sucessfully with " + data.Deposit);
                Console.ReadLine();
                return true;
            }
            Console.WriteLine("Account not Deposited");
            Console.ReadLine();
            return false;

        }

        public static bool Withdrawal(TransactionInfo data)
        {
            TransactionService obj = new TransactionService();
            var res = obj.Withdrawal(data);
            if (res == true)
            {
                Console.WriteLine("Account Withdrawal sucessfully with " + data.Withdrawal);
                Console.ReadLine();
                return true;
            }
            Console.WriteLine("Account not debitted");
            Console.ReadLine();
            return false;

        }

        public static User AccountBalance(long AccountBalance)
        {
            TransactionService obj = new TransactionService();
            var res = obj.GetAccountBalance(AccountBalance);

            Console.WriteLine("Account balance of : " + res.AccountBalance);
            Console.ReadLine();
            return res;



        }
    }
}
