using UnityEngine;
using System;
using Steamworks;
using System.Threading.Tasks;

namespace FizzySteam
{
    public class Client : Common
    {
        enum ConnectionState : byte {
            DISCONNECTED,
            CONNECTING,
            CONNECTED
        }

        public event Action<Exception> OnReceivedError;
        public event Action<byte[]> OnReceivedData;
        public event Action OnConnected;
        public event Action OnDisconnected;

        //how long to wait before connect timeout
        public static int clientConnectTimeoutMS = 25000;

        private ConnectionState state = ConnectionState.DISCONNECTED;
        private CSteamID hostSteamID = CSteamID.Nil;

        public bool Connecting { get { return state == ConnectionState.CONNECTING; } private set { if( value ) state = ConnectionState.CONNECTING; } }
        public bool Connected {
            get { return state == ConnectionState.CONNECTED; }
            private set {
                if (value)
                {
                    bool wasConnecting = Connecting;
                    state = ConnectionState.CONNECTED;
                    if (wasConnecting)
                    {
                        OnConnected?.Invoke();
                    }

                }
            }
        }
        public bool Disconnected {
            get { return state == ConnectionState.DISCONNECTED; }
            private set {
                if (value)
                {
                    bool wasntDisconnected = !Disconnected;
                    state = ConnectionState.DISCONNECTED;
                    if (wasntDisconnected)
                    {
                        OnDisconnected?.Invoke();
                    }

                    deinitialise();
                }
            }
        }

        //internally used while connecting. Subscribe to onconnect and signal this task
        TaskCompletionSource<Task> connectedComplete;
        private void setConnectedComplete()
        {
            connectedComplete.SetResult(connectedComplete.Task);
        }

        System.Threading.CancellationTokenSource cancelToken;
        public async void Connect(string host)
        {
            cancelToken = new System.Threading.CancellationTokenSource();
            // not if already started
            if (!Disconnected)
            {
                // exceptions are better than silence
                Debug.LogError("Client already connected or connecting");
                OnReceivedError?.Invoke(new Exception("Client already connected"));
                return;
            }

            // We are connecting from now until Connect succeeds or fails
            Connecting = true;

            initialise();

            try
            {
                hostSteamID = new CSteamID(Convert.ToUInt64(host));

                InternalReceiveLoop();

                connectedComplete = new TaskCompletionSource<Task>();
                
                OnConnected += setConnectedComplete;
                CloseP2PSessionWithUser(hostSteamID);

                //Send a connect message to the steam client - this requests a connection with them
                SendInternal(hostSteamID, connectMsgBuffer);

                Task connectedCompleteTask = connectedComplete.Task;

                if (await Task.WhenAny(connectedCompleteTask, Task.Delay(clientConnectTimeoutMS, cancelToken.Token)) != connectedCompleteTask)
                {
                    //Timed out waiting for connection to complete
                    OnConnected -= setConnectedComplete;

                    Exception e = new Exception("Timed out while connecting");
                    OnReceivedError?.Invoke(e);
                    throw e;
                }

                OnConnected -= setConnectedComplete;

                await ReceiveLoop();
            }
            catch (FormatException)
            {
                Debug.LogError("Failed to connect ERROR passing steam ID address");
                OnReceivedError?.Invoke(new Exception("ERROR passing steam ID address"));
                return;
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to connect " + ex);
                OnReceivedError?.Invoke(ex);
            }
            finally
            {
                Disconnect();
            }

        }

        public async void Disconnect()
        {
            if (!Disconnected)
            {
                SendInternal(hostSteamID, disconnectMsgBuffer);
                Disconnected = true;
                cancelToken.Cancel();

                //Wait a short time before calling steams disconnect function so the message has time to go out
                await Task.Delay(100);
                CloseP2PSessionWithUser(hostSteamID);
            }
        }

        private async Task ReceiveLoop()
        {
            Debug.Log("ReceiveLoop Start");

            uint readPacketSize;
            CSteamID clientSteamID;

            try
            {
                byte[] receiveBuffer;

                while (Connected)
                {
                    for (int i = 0; i < channels.Length; i++) {
                        while (Receive(out readPacketSize, out clientSteamID, out receiveBuffer, i)) {
                            if (readPacketSize == 0) {
                                continue;
                            }
                            if (clientSteamID != hostSteamID) {
                                Debug.LogError("Received a message from an unknown");
                                continue;
                            }
                            // we received some data,  raise event
                            OnReceivedData?.Invoke(receiveBuffer);
                        }
                    }
                    //not got a message - wait a bit more
                    await Task.Delay(TimeSpan.FromSeconds(secondsBetweenPolls));
                }
            }
            catch (ObjectDisposedException) { }

            Debug.Log("ReceiveLoop Stop");
        }

        protected override void OnNewConnectionInternal(P2PSessionRequest_t result)
        {
            Debug.Log("OnNewConnectionInternal in client");

            if (hostSteamID == result.m_steamIDRemote)
            {
                SteamNetworking.AcceptP2PSessionWithUser(result.m_steamIDRemote);
            } else
            {
                Debug.LogError("");
            }
        }

        //start a async loop checking for internal messages and processing them. This includes internal connect negotiation and disconnect requests so runs outside "connected"
        private async void InternalReceiveLoop()
        {
            Debug.Log("InternalReceiveLoop Start");

            uint readPacketSize;
            CSteamID clientSteamID;

            try
            {
                while (!Disconnected)
                {
                    while (ReceiveInternal(out readPacketSize, out clientSteamID))
                    {
                        if (readPacketSize != 1)
                        {
                            continue;
                        }
                        if (clientSteamID != hostSteamID)
                        {
                            Debug.LogError("Received an internal message from an unknown");
                            continue;
                        }
                        switch (receiveBufferInternal[0])
                        {
                            case (byte)InternalMessages.ACCEPT_CONNECT:
                                Connected = true;
                                break;
                            case (byte)InternalMessages.DISCONNECT:
                                Disconnected = true;
                                break;
                        }
                    }
                    //not got a message - wait a bit more
                    await Task.Delay(TimeSpan.FromSeconds(secondsBetweenPolls));
                }
            }
            catch (ObjectDisposedException) { }

            Debug.Log("InternalReceiveLoop Stop");
        }

        // send the data or throw exception
        public bool Send(byte[] data, int channelId)
        {
            if (Connected)
            {
                Send(hostSteamID, data, channelToSendType(channelId), channelId);
                return true;
            }
            else
            {
                throw new Exception("Not Connected");
                return false;
            }
        }

    }
}