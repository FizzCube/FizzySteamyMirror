# FizzySteamyMirror

Fizzcube bringing together [Steam](https://store.steampowered.com/) and [Mirror](https://github.com/rlabrecque/Steamworks.NET)

This project was previously called **"SteamNetNetworkTransport"**, this is Version 2 it's a complete rebuild utilising Async of a Steam P2P network transport layer for [Mirror](https://github.com/rlabrecque/Steamworks.NET)

## Quick start - Demo Project 

(coming soon)

## Dependencies
Both of these projects need to be installed and working before you can use this transport.
1. [Steamworks.NET](https://github.com/rlabrecque/Steamworks.NET) FizzySteamyMirror relies on Steamworks.NET to communicate with the [Steamworks API](https://partner.steamgames.com/doc/sdk). **Requires .Net 4.x**
2. [Mirror](https://github.com/vis2k/Mirror) FizzySteamyMirror is also obviously dependant on Mirror which is a streamline, bug fixed, maintained version of UNET for Unity. **Recommended [Stable Version](https://assetstore.unity.com/packages/tools/network/mirror-129321)**

## Setting Up
1. Download and install the dependencies 
2. Download **"FizzySteamyMirror"** and place in your Assets folder somewhere. **If errors occur, open a [Issue ticket.](https://github.com/vis2k/Mirror)**
3. In your ![Image](http://i.galtrox.com/index.php/s/LX2KPkezLwazrTS/preview) object replace ![Image](http://i.galtrox.com/index.php/s/LTwTTyZLtbmGHY6/preview) with ![Image](http://i.galtrox.com/index.php/s/5PJBqPjJiFdqxG9/preview) 

## Building
1. When Building your game you have to place **"steam_appid.txt"** into the dir of the game. If you cant find it well, just make a **"steam_appid.txt"** and place **480** in side.

![Image](http://i.galtrox.com/index.php/s/KLB8W6kFtnjwQPJ/preview)

2. When running the game make sure you have placed it into steam as a **Non-Steam Game**

**Note: The 480(Spacewar) appid is a very grey area, technically, it's not allowed but they don't really do anything about it.
If you know a better way around this please make a [Issue ticket.](https://github.com/vis2k/Mirror)**

**Note: When you have a appid from steam then replace the 480 with your own game appid.**

## Host
1. Open your game through Steam
2. Host your game ![Image](http://i.galtrox.com/index.php/s/ycNEwXKf5jdYD8T/preview)
3. if it says your playing **"Spacewar"** in Steam **"congrats its working!"**

**Note: You can run it in Unity aswell**

## Client
1. Send the game to your buddy.
2. The client needs the steam64id of the host to be able to connect.
3. Place the steam64id into ![Image](http://i.galtrox.com/index.php/s/py8ZgqtkbrzyC3B/preview) then click "Lan Client"
4. **Bing bash bong DONE!**

## Play Testing your game localy

1.You need to have both scripts **"Fizzy Steamy Mirror"** and **"Telepathy Transport"**

![Image](http://i.galtrox.com/index.php/s/LPqwLpqXecSiG7z/preview)

2.To test it localy disable **"Fizzy Steamy Mirror"** and enable **"Telepathy Transport"**

![Image](http://i.galtrox.com/index.php/s/TdBsCSETdXCp5rr/preview)

3.To test it on Steam P2P again enable **"Fizzy Steamy Mirror"** and disable **"Telepathy Transport"**

![Image](http://i.galtrox.com/index.php/s/kb63dSQiQ3KdgAa/preview)
