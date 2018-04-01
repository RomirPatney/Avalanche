using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

using System.Collections;
using System.Text;
using Newtonsoft.Json;
//using ArkNet.Model.Transactions;
//using ArkNet.Controller;

namespace Avalanche.Services
{
    /*public sealed class ArkNetApi
    {
        private List<Tuple<string, int>> _peerSeedListMainNet =
            new List<Tuple<string, int>> {
            Tuple.Create("5.39.9.240", 4001),
            Tuple.Create("5.39.9.241", 4001),
            Tuple.Create("5.39.9.242", 4001),
            Tuple.Create("5.39.9.243", 4001),
            Tuple.Create("5.39.9.244", 4001),
            Tuple.Create("5.39.9.250", 4001),
            Tuple.Create("5.39.9.251", 4001),
            Tuple.Create("5.39.9.252", 4001),
            Tuple.Create("5.39.9.253", 4001),
            Tuple.Create("5.39.9.254", 4001),
            Tuple.Create("5.39.9.255", 4001)
            };

        private List<Tuple<string, int>> _peerSeedListDevNet =
            new List<Tuple<string, int>> {
            Tuple.Create("167.114.43.48", 4002),
            Tuple.Create("167.114.29.49", 4002),
            Tuple.Create("167.114.43.43", 4002),
            Tuple.Create("167.114.29.54", 4002),
            Tuple.Create("167.114.29.45", 4002),
            Tuple.Create("167.114.29.40", 4002),
            Tuple.Create("167.114.29.56", 4002),
            Tuple.Create("167.114.43.35", 4002),
            Tuple.Create("167.114.29.51", 4002),
            Tuple.Create("167.114.29.59", 4002),
            Tuple.Create("167.114.43.42", 4002),
            Tuple.Create("167.114.29.34", 4002),
            Tuple.Create("167.114.29.62", 4002),
            Tuple.Create("167.114.43.49", 4002),
            Tuple.Create("167.114.29.44", 4002)
            };

        private static readonly Lazy<ArkNetApi> _lazy =
            new Lazy<ArkNetApi>(() => new ArkNetApi());

        public static ArkNetApi Instance => _lazy.Value;

        public ArkNetworkSettings NetworkSettings;

        private ArkNetApi()
        {

        }

        public async Task Start(NetworkType type)
        {
            await SetNetworkSettings(await GetInitialPeer(type));
        }

        public async Task Start(string initialPeerIp, int initialPeerPort)
        {
            await SetNetworkSettings(GetInitialPeer(initialPeerIp, initialPeerPort));
        }

        private async Task SetNetworkSettings(PeerApi initialPeer)
        {
            var responseAutoConfigure = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Loader.GET_AUTO_CONFIGURE);
            var responseFees = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, ArkStaticStrings.ArkApiPaths.Block.GET_FEES);
            var responsePeer = await initialPeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Peer.GET, initialPeer.Ip, initialPeer.Port));

            var autoConfig = JsonConvert.DeserializeObject<ArkLoaderNetworkResponse>(responseAutoConfigure);
            var fees = JsonConvert.DeserializeObject<Fees>(JObject.Parse(responseFees)["fees"].ToString());
            var peer = JsonConvert.DeserializeObject<ArkPeerResponse>(responsePeer);

            NetworkSettings = new ArkNetworkSettings()
            {
                Port = initialPeer.Port,
                BytePrefix = (byte)autoConfig.Network.Version,
                Version = peer.Peer.Version,
                NetHash = autoConfig.Network.NetHash,
                Fee = fees
            };

            await NetworkApi.Instance.WarmUp(new PeerApi(initialPeer.Ip, initialPeer.Port));
        }

        private PeerApi GetInitialPeer(string initialPeerIp, int initialPeerPort)
        {
            return new PeerApi(initialPeerIp, initialPeerPort);
        }

        private async Task<PeerApi> GetInitialPeer(NetworkType type, int retryCount = 0)
        {
            var peerUrl = _peerSeedListMainNet[new Random().Next(_peerSeedListMainNet.Count)];
            if (type == NetworkType.DevNet)
                peerUrl = _peerSeedListDevNet[new Random().Next(_peerSeedListDevNet.Count)];

            var peer = new PeerApi(peerUrl.Item1, peerUrl.Item2);
            if (await peer.IsOnline())
            {
                return peer;
            }

            if ((type == NetworkType.DevNet && retryCount == _peerSeedListDevNet.Count)
             || (type == NetworkType.MainNet && retryCount == _peerSeedListMainNet.Count))
                throw new Exception("Unable to connect to a seed peer");

            return await GetInitialPeer(type, retryCount + 1);
        }
    }

    public class ArkAccount
    {
        public string Address { get; set; }
        public long UnconfirmedBalance { get; set; }
        public long Balance { get; set; }
        public string PublicKey { get; set; }
        public int UnconfirmedSignature { get; set; }
        public int SecondSignature { get; set; }
        public object SecondPublicKey { get; set; }
        public object[] Multisignatures { get; set; }
        public object[] U_Multisignatures { get; set; }
    }

    public sealed class NetworkApi
    {
        private static readonly Lazy<NetworkApi> lazy =
            new Lazy<NetworkApi>(() => new NetworkApi());

        private readonly Random _random = new Random();
        private List<PeerApi> _peers = new List<PeerApi>();

        private NetworkApi()
        {
            _peers = new List<PeerApi>();
        }

        public static NetworkApi Instance => lazy.Value;

        public string Nethash { get; set; } = ArkNetApi.Instance.NetworkSettings.NetHash;
        public int Port { get; set; } = ArkNetApi.Instance.NetworkSettings.Port;
        public byte Prefix { get; set; } = ArkNetApi.Instance.NetworkSettings.BytePrefix;
        public string Version { get; set; } = ArkNetApi.Instance.NetworkSettings.Version;
        public int BroadcastMax { get; set; } = ArkNetApi.Instance.NetworkSettings.MaxNumOfBroadcasts;
        public PeerApi ActivePeer { get; set; }

        public async Task WarmUp(PeerApi initialPeer)
        {
            ActivePeer = initialPeer;
            await SetPeerList();
            ActivePeer = GetRandomPeer();
            StartPeerCleaningTask();
        }

        public PeerApi GetRandomPeer()
        {
            return _peers[_random.Next(_peers.Count())];
        }

        public void SwitchPeer()
        {
            ActivePeer = GetRandomPeer();
        }

        private async Task SetPeerList()
        {
            var peers = await PeerService.GetAllAsync();
            var peersOrderByHeight = peers.Peers
                .Where(x => x.Status.Equals("OK") && x.Version == ArkNetApi.Instance.NetworkSettings.Version)
                .OrderByDescending(x => x.Height)
                .ToList();

            var heightToCompare = peersOrderByHeight.FirstOrDefault().Height - ArkNetApi.Instance.NetworkSettings.PeerCleaningHeightThreshold;

            var peerURLs = peersOrderByHeight.Where(x => x.Height >= heightToCompare)
                .Select(x => new { Ip = x.Ip, Port = x.Port })
                .ToList();

            var tmpPeerList = new List<PeerApi>();
            foreach (var peerURL in peerURLs)
            {
                tmpPeerList.Add(new PeerApi(peerURL.Ip, peerURL.Port));
            }

            if (!tmpPeerList.Any(x => x.Ip == NetworkApi.Instance.ActivePeer.Ip))
                tmpPeerList.Add(NetworkApi.Instance.ActivePeer);

            _peers = tmpPeerList;
        }

        private void StartPeerCleaningTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(ArkNetApi.Instance.NetworkSettings.PeerCleaningIntervalInMinutes));
                    try
                    {
                        await SetPeerList();
                    }
                    catch { }
                }
            });
        }
    }

        public class AccountService
    {
        public static ArkAccountResponse GetByAddress(string address)
        {
            return GetByAddressAsync(address).Result;
        }

        public async static Task<ArkAccountResponse> GetByAddressAsync(string address)
        {
            var response = await NetworkApi.Instance.ActivePeer.MakeRequest(ArkStaticStrings.ArkHttpMethods.GET, string.Format(ArkStaticStrings.ArkApiPaths.Account.GET_ACCOUNT, address));

            return JsonConvert.DeserializeObject<ArkAccountResponse>(response);
        }
    }

    public class ArkAccountResponse : ArkResponseBase
    {
        public ArkAccount Account { get; set; }
    }
    public class ArkResponseBase
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string Error { get; set; }
    }


    public class ArkController
    {
        private ArkAccount _account;
        private string _passPhrase;
        private string _secondPassPhrase;

        public ArkController(string passphrase, string secondPassPhrase = null)
        {
            _passPhrase = passphrase;
            _secondPassPhrase = secondPassPhrase;
        }

        public ArkAccount GetArkAccount()
        {
            if (_account == null)
                _account = AccountService.GetByAddress(Crypto.GetAddress(Crypto.GetKeys(_passPhrase), ArkNetApi.Instance.NetworkSettings.BytePrefix)).Account;
            return _account;
        }
    }*/


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


    





        //    public ArkTransactionList GetRequestArk()
        //{
        //    //var account = AccountService.GetByAddress(Crypto.GetAddress(Crypto.GetKeys(_passPhrase), 
        //    //    ArkNetApi.Instance.NetworkSettings.BytePrefix)).Account;

        //    //var transaction = new ArkTransactionRequest();
        //    //transaction.OrderBy = "timestamp:desc";
        //    //transaction.RecipientId = "D8wohrKtnziDS6AGNKD7fWnx4kxF4e6bGx";
        //    //transaction.SenderId = "D8wohrKtnziDS6AGNKD7fWnx4kxF4e6bGx";
        //    //transaction.Offset = 0;
        //    //transaction.Limit = 100000;
        //    //if (TransactionService == null)
        //      //  return null;

        //    //var result = TransactionService.GetTransactions(transaction);
        //    var accCtnrl = new AccountController(_passPhrase);
        //    var result = accCtnrl.GetTransactions();


        //    return result;
        //}

        // TODO the satoshi amount, recepient address and passphrase are currently only used for the test account, we will
        // need to have a unique one per user, coming from the database
        //public ArkTransactionPostResponse PostRequestArk(string message)
        //{
        //    var accCtnrl = new AccountController("ability cloud wisdom tortoise use rocket draw napkin cute split keep strike");
        //    var result = accCtnrl.SendArk(1, _address, message);


        //    return result;


        //}
}
}
