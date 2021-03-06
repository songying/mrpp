﻿using Mirror;
using Mirror.Discovery;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Network {

   


    public class NetworkSystem : GDGeek.Singleton<NetworkSystem>
    {

      

        readonly Dictionary<long, ServerResponse> discoveredServers = new Dictionary<long, ServerResponse>();
        public Dictionary<long, ServerResponse> servers {
            get {
                return discoveredServers;
            }
        
        }
        public void onDiscoveredServer(ServerResponse info)
        {
            discoveredServers[info.serverId] = info;
        }

        [SerializeField]
        NetworkManager _manager;
        [SerializeField]
        NetworkDiscovery _discovery;

        public NetworkDiscovery discovery {

            get {
                return _discovery;
            }
        }
        void Start() {
            _discovery.OnServerFound.AddListener(onDiscoveredServer);
        }
       
        public void startHost() {
            discoveredServers.Clear();
            _manager.StartHost();
            _discovery.AdvertiseServer();

        }
        public void listening()
        {
            discoveredServers.Clear();
            _discovery.StartDiscovery();
        }
        private ServerResponse serverResponse_;
        public ServerResponse serverResponse{
            get{
                return serverResponse_; 
            }
            set {
                serverResponse_ = value;
            }
        }
        public void client()
        {
            _manager.StartClient(serverResponse_.uri);
        }
    }
}