# FizzySteamyMirror

Fizzcube bringing together [Steam](https://store.steampowered.com/) and [Mirror](https://github.com/vis2k/Mirror)

This project was previously called **"SteamNetNetworkTransport"**, this is Version 2 it's a complete rebuild utilising Async of a Steam P2P network transport layer for [Mirror](https://github.com/vis2k/Mirror)

## Dependencies
Both of these projects need to be installed and working before you can use this transport.
1. [Steamworks.NET](https://github.com/rlabrecque/Steamworks.NET) FizzySteamyMirror relies on Steamworks.NET to communicate with the [Steamworks API](https://partner.steamgames.com/doc/sdk). **Requires .Net 4.x**  
**Note : If you get the package from Release, Steamworks.NET is already included, you don't have to download it separately**
2. [Mirror](https://github.com/vis2k/Mirror) FizzySteamyMirror is also obviously dependant on Mirror which is a streamline, bug fixed, maintained version of UNET for Unity. **Recommended [Stable Version](https://assetstore.unity.com/packages/tools/network/mirror-129321)**

## Setting Up
* Note: if you want an easy import, skip the steps bellow & take the [release](https://github.com/Raystorms/FizzySteamyMirror/releases), it has Steamworks.Net already included. (if you already have Steamworks.Net in your project, you might need to delete either your import or the one included in the release).

1. Download and install the dependencies **Download the unitypackage from release for easy all in one**
2. Download **"FizzySteamyMirror"** and place in your Assets folder somewhere. **If errors occur, open a [Issue ticket.](https://github.com/FizzCube/FizzySteamyMirror/issues)**
3. In your NetworkManager object replace Telepathy (or any other active transport) with FizzySteamyMirror.

## Building
1. When Building your game you have to place **"steam_appid.txt"** into the directory of the game. If you cant find it well, just make a **"steam_appid.txt"** and place **480** in side.

2. When running the game make sure you have placed it into steam as a **Non-Steam Game**
**Note: This is not reuired, but some have reported their steam SDK not working without doing this**

**Note: The 480(Spacewar) appid is a very grey area, technically, it's not allowed but they don't really do anything about it.
If you know a better way around this please make a [Issue ticket.](https://github.com/FizzCube/FizzySteamyMirror/issues)**

**Note: When you have your own appid from steam then replace the 480 with your own game appid.**

## Host
1. Open your game through Steam
2. Host your game through the NetworkManagerHUD
3. if it says your playing **"Spacewar"** in Steam **"congrats its working!"**

**Note: You can run it in Unity aswell**

## Client
1. Send the game to your buddy.
2. The client needs the steam64id of the host to be able to connect.
3. Place the steam64id into the address of NetworkManagerHUD then click "Lan Client"
4. **Bing bash bong DONE!**

**Joining through code is the same like any other transport in mirror, just pass the steam64id as the address**

## Play Testing your game locally

1.You need to have both scripts **"Fizzy Steamy Mirror"** and **"Telepathy Transport"**

2.To test it locally disable **"Fizzy Steamy Mirror"** and enable **"Telepathy Transport"**

3.To test it on Steam P2P again enable **"Fizzy Steamy Mirror"** and disable **"Telepathy Transport"**

