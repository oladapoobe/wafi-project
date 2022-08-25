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
                    if (json != "")
                    {
                        users = JsonConvert.DeserializeObject<List<User>>(json);
                    }
                }
            }

            List<TransactionInfo> transact = new List<TransactionInfo>();
            if (File.Exists("transactionData.json") == true)
            {
                using (StreamReader r = new StreamReader("transactionData.json"))
                {
                    string json = r.ReadToEnd();
                    if (json != "")
                    {
                        transact = JsonConvert.DeserializeObject<List<TransactionInfo>>(json);
                    }
                }
            }


            /////////////// TEST CASE 1
            User data = new User();
            data.AccountBalance = 0;
            data.AccountNumber = 23423343003;
            data.Name = "oladapo obe";
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
            var resAcctNo = AccountBalance(234233433);
            Console.WriteLine("Account already exist" + resAcctNo.AccountBalance);
            Console.ReadLine();

            ///// TEST CASE 3
            TransactionInfo data3 = new TransactionInfo();
            data3.AccountBalance = resAcctNo.AccountBalance;
            data3.AccountNumber = 234233433;
            data3.Currency = "$";
            data3.DateCreated = DateTime.Now;
            data3.Deposit = 400;
            data3.Tranfer = false;

            transact.Add(data3);
            Deposit(transact, data3);


            // TEST CASE 4
            var resAcctNo2 = AccountBalance(234233433);
            Console.WriteLine("Account already exist" + resAcctNo2.AccountBalance);
            Console.ReadLine();

            TransactionInfo data2 = new TransactionInfo();
            data2.AccountBalance = resAcctNo2.AccountBalance;
            data2.AccountNumber = 234233433;
            data2.Currency = "$";
            data2.DateCreated = DateTime.Now;
            data2.Withdrawal = 200;
            data2.Tranfer = false;

            transact.Add(data2);
            Withdrawal(transact, data2);


            // TEST CASE 5
            var resAcctNo4 = AccountBalance(234233433);
            Console.WriteLine("Account already exist" + resAcctNo4.AccountBalance);
            Console.ReadLine();

            TransactionInfo data4 = new TransactionInfo();
            data4.AccountBalance = resAcctNo4.AccountBalance;
            data4.AccountNumber = 234233433;
            data4.Currency = "$";
            data4.DateCreated = DateTime.Now;
            data4.Withdrawal = 100;
            data4.Tranfer = true;
            data4.TranferTo = 23423343003;

            transact.Add(data4);
            Transfer(transact, data4);



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
            var res = obj.Deposit(data, up);
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

        public static bool Transfer(List<TransactionInfo> data, TransactionInfo up)
        {
           


            TransactionService obj = new TransactionService();
            var res = obj.Withdrawal(data, up);
            if (res == true)
            {
                Console.WriteLine("Account Withdrawal sucessfully with " + up.Withdrawal);
                up.AccountNumber = up.TranferTo;
                up.Deposit = up.Withdrawal;
                var res2 = obj.Deposit(data, up);
                Console.WriteLine("Account Creditted sucessfully with " + up.Withdrawal + "Transferred to " + up.TranferTo);

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
