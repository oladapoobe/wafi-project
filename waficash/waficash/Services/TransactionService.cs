using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Transactions;
using waficash.Models;

namespace waficash.Services
{
    public class TransactionService 
    {
        public  TransactionService()
        {

        }

 

        public bool CreateUser(List<User> data)
        {
            using (StreamWriter sw = File.CreateText("userData.json"))
            {
                sw.Write(JsonConvert.SerializeObject(data));

                return true;
            }
        }

        public bool Withdrawal(TransactionInfo data)
        {
            using (StreamWriter sw = File.CreateText("transactionData.json"))
            {
                sw.Write(JsonConvert.SerializeObject(data));
                var Updaterecord = GetAccountBalance(data.AccountBalance);
                var AccountBalance = Updaterecord.AccountBalance - data.Withdrawal;
                var response = UpdateAccountSheet(AccountBalance, Updaterecord.Id);

                return response;
            }
        }
        public bool Deposit(TransactionInfo data)
        {
            using (StreamWriter sw = File.CreateText("transactionData.json"))
            {
                sw.Write(JsonConvert.SerializeObject(data));
                var Updaterecord = GetAccountBalance(data.AccountNumber);
                var AccountBalance = Updaterecord.AccountBalance + data.Deposit;
                var response = UpdateAccountSheet(AccountBalance, Updaterecord.Id);

                return response;

            }
        }
        public User GetAccountBalance(long AccountNumber)
        {
            using (StreamReader r = new StreamReader("userData.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<User>>(json);
                var userRecord = items.Find(x => x.AccountNumber == AccountNumber);
                return userRecord;
            }

        }
        public bool UpdateAccountSheet(decimal AccountBalance, long Id)
        { 
            string json = File.ReadAllText("userData.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonObj["User"][Id]["AccountBalance"] = AccountBalance;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("userData.json", output);
            return true;
        }
    }
}
