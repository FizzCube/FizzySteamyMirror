# FizzySteamyMirror

Fizzcube bringing together Steam and Mirror.

Version 2, complete rebuild utilising Async (Previously SteamNetNetworkTransport) of a Steam P2P network transport layer for Mirror - 2018 branch ([vis2k's UNET replacement for Unity](https://github.com/vis2k/Mirror/tree/2018))

FizzySteamyMirror uses [Steamworks.NET](https://github.com/rlabrecque/Steamworks.NET) wrapper for the steamworks API.

## Quick start - Demo Project 

(coming soon)

## Dependencies
FizzySteamyMirror relies on [Steamworks.NET](https://github.com/rlabrecque/Steamworks.NET) to communicate with the [Steamworks API](https://partner.steamgames.com/doc/sdk).

FizzySteamyMirror is also obviously dependant on [Mirror](https://github.com/vis2k/Mirror) which is a streamline, bug fixed, maintained version of UNET for Unity.

Both of these projects need to be installed and working before you can use this transport

Requires .Net 4.x

## How to use
1. Download and install the dependencies 
2. Download all the .cs files and place in your Assets folder somewhere. If errors occur, open a Issue ticket.
3. In your class extending from Mirror's NetworkManager class, you would do:
```csharp
public override void InitializeTransport() {
  NetworkManager.transport = new FizzySteamyMirror();
}
```
Then, to start as a host:

```csharp
NetworkManager.singleton.StartHost();
```

Or, to connect to a host as a client:

```csharp
NetworkManager.singleton.networkAddress = friendSteamIDs;
NetworkManager.singleton.StartClient();
```
Where friendSteamIDs is a `string` containing the hosts Steam account numeric ID.
