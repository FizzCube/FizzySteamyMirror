using UnityEngine;
using System;
using Steamworks;

namespace FizzySteam
{
    public class Common
    {

        protected enum SteamChannels : int
        {
            SEND_DATA,
            SEND_INTERNAL = 100
        }

        protected enum InternalMessages : byte
        {
            CONNECT,
            ACCEPT_CONNECT,
            DISCONNECT
        }

        public static float secondsBetweenPolls = 0.03333f;

        //this is a callback from steam that gets registered and called when the server receives new connections
        private Callback<P2PSessionRequest_t> callback_OnNewConnection = null;
        //this is a callback from steam that gets registered and called when the ClientConnect fails
        private Callback<P2PSessionConnectFail_t> callback_OnConnectFail = null;

        readonly static protected byte[] connectMsgBuffer = new byte[] { (byte)InternalMessages.CONNECT };
        readonly static protected byte[] acceptConnectMsgBuffer = new byte[] { (byte)InternalMessages.ACCEPT_CONNECT };
        readonly static protected byte[] disconnectMsgBuffer = new byte[] { (byte)InternalMessages.DISCONNECT };
        public static EP2PSend[] channels;

        readonly static protected uint maxPacketSize = 1048576;
        readonly protected byte[] receiveBufferInternal = new byte[1];

        protected void deinitialise()
        {
            if (callback_OnNewConnection == null)
            {
                callback_OnNewConnection.Dispose();
                callback_OnNewConnection = null;
            }

            if (callback_OnConnectFail == null)
            {
                callback_OnConnectFail.Dispose();
                callback_OnConnectFail = null;
            }

        }

        protected virtual void initialise()
        {
            Debug.Log("initialise");
            /*
            nextConnectionID = 1;

            steamConnectionMap = new SteamConnectionMap();

            steamNewConnections = new Queue<int>();

            serverReceiveBufferPendingConnectionID = -1;
            serverReceiveBufferPending = null;
            */

            if (SteamManager.Initialized)
            {
                if (callback_OnNewConnection == null)
                {
                    Debug.Log("initialise callback 1");
                    callback_OnNewConnection = Callback<P2PSessionRequest_t>.Create(OnNewConnection);
                }
                if (callback_OnConnectFail == null)
                {
                    Debug.Log("initialise callback 2");

                    callback_OnConnectFail = Callback<P2PSessionConnectFail_t>.Create(OnConnectFail);
                }
            }
            else
            {
                Debug.LogError("STEAM NOT Initialized so couldnt integrate with P2P");
                return;
            }
        }

        protected void OnNewConnection(P2PSessionRequest_t result)
        {
            Debug.Log("OnNewConnection");
            OnNewConnectionInternal(result);
        }

        protected virtual void OnNewConnectionInternal(P2PSessionRequest_t result) { Debug.Log("OnNewConnectionInternal"); }

        protected virtual void OnConnectFail(P2PSessionConnectFail_t result)
        {
            Debug.Log("OnConnectFail " + result);
            throw new Exception("Failed to connect");
        }

        protected void SendInternal(CSteamID host, byte[] msgBuffer)
        {
            if (!SteamManager.Initialized)
            {
                throw new ObjectDisposedException("Steamworks");
            }
            SteamNetworking.SendP2PPacket(host, msgBuffer, (uint)msgBuffer.Length, EP2PSend.k_EP2PSendReliable, (int)SteamChannels.SEND_INTERNAL);
        }

        protected bool ReceiveInternal(out uint readPacketSize, out CSteamID clientSteamID)
        {
            if (!SteamManager.Initialized)
            {
                throw new ObjectDisposedException("Steamworks");
            }
            return SteamNetworking.ReadP2PPacket(receiveBufferInternal, 1, out readPacketSize, out clientSteamID, (int)SteamChannels.SEND_INTERNAL);
        }

        protected void Send(CSteamID host, byte[] msgBuffer, EP2PSend sendType, int channel)
        {
            if (!SteamManager.Initialized)
            {
                throw new ObjectDisposedException("Steamworks");
            }
            if (channel >= channels.Length) {
                channel = 0;
            }
            SteamNetworking.SendP2PPacket(host, msgBuffer, (uint)msgBuffer.Length, sendType, channel);
        }

        protected bool Receive(out uint readPacketSize, out CSteamID clientSteamID, out byte[] receiveBuffer, int channel)
        {
            if (!SteamManager.Initialized)
            {
                throw new ObjectDisposedException("Steamworks");
            }

            uint packetSize;
            if (SteamNetworking.IsP2PPacketAvailable(out packetSize, channel) && packetSize > 0)
            {
                receiveBuffer = new byte[packetSize];
                return SteamNetworking.ReadP2PPacket(receiveBuffer, packetSize, out readPacketSize, out clientSteamID, channel);
            }

            receiveBuffer = null;
            readPacketSize = 0;
            clientSteamID = CSteamID.Nil;
            return false;
        }

        protected void CloseP2PSessionWithUser(CSteamID clientSteamID)
        {
            if (!SteamManager.Initialized)
            {
                throw new ObjectDisposedException("Steamworks");
            }
            SteamNetworking.CloseP2PSessionWithUser(clientSteamID);
        }

        public uint GetMaxPacketSize(EP2PSend sendType)
        {
            switch (sendType)
            {
                case EP2PSend.k_EP2PSendUnreliable:
                case EP2PSend.k_EP2PSendUnreliableNoDelay:
                    return 1200; //UDP like - MTU size.

                case EP2PSend.k_EP2PSendReliable:
                case EP2PSend.k_EP2PSendReliableWithBuffering:
                    return maxPacketSize; //Reliable message send. Can send up to 1MB of data in a single message.

                default:
                    Debug.LogError("Unknown type so uknown max size");
                    return 0;
            }

        }

        protected EP2PSend channelToSendType(int channelId)
        {
            if (channelId >= channels.Length) {
                Debug.LogError("Unknown channel id, please set it up in the component, will now send reliably");
                return EP2PSend.k_EP2PSendReliable;
            }
            return channels[channelId];
        }

    }
}