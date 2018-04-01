using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

using ArkNet.Controller;
using System.Collections;
using System.Text;
using ArkNet.Model;
using ArkNet.Service;
using ArkNet.Utils.Enum;
using Newtonsoft.Json;
using ArkNet.Model.Transactions;
using ArkNet.Core;
using ArkNet;
using ArkNet.Messages.Transaction;

namespace Avalanche.Services
{
    public class APIcaller_Service
    {
        // TODO will have to be coming from the database eventually
        private string _passPhrase = "ability cloud wisdom tortoise use rocket draw napkin cute split keep strike";
        private string _address = "D8wohrKtnziDS6AGNKD7fWnx4kxF4e6bGx";

        public void GetRequestPhoton(string endnode)
        {
            var client = new RestClient(endnode);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "71ac3e8e-7c4b-4731-a203-47fd29b4bd31");
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);
        }


        public void PostRequestPhoton(string endnode, string post)
        {
            var client = new RestClient(endnode);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "cf2a0a17-df25-4585-9a57-cc6ec09eca52");
            request.AddHeader("Cache-Control", "no-cache");
            request.AddParameter("undefined", post, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
        }


        public ArkTransactionList GetRequestArk()
        {
            var account = AccountService.GetByAddress(Crypto.GetAddress(Crypto.GetKeys(_passPhrase), 
                ArkNetApi.Instance.NetworkSettings.BytePrefix)).Account;
            var transaction = new ArkTransactionRequest();
            var result = TransactionService.GetTransactions(transaction);
            //var accCtnrl = new AccountController(_passPhrase);
            //var result = accCtnrl.GetTransactions();

            return result;
        }

        // TODO the satoshi amount, recepient address and passphrase are currently only used for the test account, we will
        // need to have a unique one per user, coming from the database
        public ArkTransactionPostResponse PostRequestArk(string address, string message)
        {
            var accCtnrl = new AccountController(_passPhrase);
            var result = accCtnrl.SendArk(1, _address, message);

            return result;
        }
    }
}
