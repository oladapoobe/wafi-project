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

            List<TransactionInfo> transact = new List<TransactionInfo>();
            if (File.Exists("transactionData.json") == true)
            {
                using (StreamReader r = new StreamReader("transactionData.json"))
                {
                    string json = r.ReadToEnd();
                    transact = JsonConvert.DeserializeObject<List<TransactionInfo>>(json);
                }
            }


            /////////////// TEST CASE 1
            User data = new User();
            data.AccountBalance = 0;
            data.AccountNumber = 234233433;
            data.Name = "oladapo obgae";
            data.Currency = "$";

            var seerec = users.Find(x => x.AccountNumber == data.AccountNumber);
            if (seerec == null)
            {
                users.Add(data);
                CreateUser(users);
            }
            Console.WriteLine("Account already exist");
            Console.ReadLine();

            ///////////////////////////////////
            ///TEST CASE 2 
            var resAcctNo =  AccountBalance(234233433);
            Console.WriteLine("Account already exist" + resAcctNo.AccountBalance);
            Console.ReadLine();

            /// TEST CASE 3
            TransactionInfo data3 = new TransactionInfo();
            data3.AccountBalance = resAcctNo.AccountBalance;
            data3.AccountNumber = 234233433;
            data3.Currency = "$";
            data3.DateCreated = DateTime.Now;
            data3.Deposit = 400;

            transact.Add(data3);
            Deposit(transact, data3);


            // TEST CASE 4
            var resAcctNo2 = AccountBalance(234233433);
            Console.WriteLine("Account already exist" + resAcctNo2.AccountBalance);
            Console.ReadLine();
           
            TransactionInfo data2 = new TransactionInfo();
            data2.AccountBalance = resAcctNo.AccountBalance;
            data2.AccountNumber = 234233433;
            data2.Currency = "$";
            data2.DateCreated = DateTime.Now;
            data2.Withdrawal = 400;

            transact.Add(data2);
            Withdrawal(transact, data2);



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

        public static bool Deposit(List<TransactionInfo> data, TransactionInfo up)
        {
            TransactionService obj = new TransactionService();
            var res = obj.Deposit(data,up);
            if (res == true)
            {
                Console.WriteLine("Account Deposited sucessfully with " + up.Deposit);
                Console.ReadLine();
                return true;
            }
            Console.WriteLine("Account not Deposited");
            Console.ReadLine();
            return false;

        }

        public static bool Withdrawal(List<TransactionInfo> data, TransactionInfo up)
        {
            TransactionService obj = new TransactionService();
            var res = obj.Withdrawal(data, up);
            if (res == true)
            {
                Console.WriteLine("Account Withdrawal sucessfully with " + up.Withdrawal);
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
