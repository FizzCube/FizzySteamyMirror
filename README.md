# FizzySteamyMirror

Fizzcube bringing together Steam and Mirror.

This project was Previously called SteamNetNetworkTransport, this is Version 2 it's a complete rebuild utilising Async of a Steam P2P network transport layer for [Mirror](https://github.com/rlabrecque/Steamworks.NET)

## Quick start - Demo Project 

(coming soon)

## Dependencies
Both of these projects need to be installed and working before you can use this transport.
1. [Steamworks.NET](https://github.com/rlabrecque/Steamworks.NET) FizzySteamyMirror relies on Steamworks.NET to communicate with the [Steamworks API](https://partner.steamgames.com/doc/sdk). **Requires .Net 4.x**
2. [Mirror](https://github.com/vis2k/Mirror) FizzySteamyMirror is also obviously dependant on Mirror which is a streamline, bug fixed, maintained version of UNET for Unity. **Recommended [Stable Version](https://github.com/vis2k/Mirror)**

## Setting Up
1. Download and install the dependencies 
2. Download FizzySteamyMirror and place in your Assets folder somewhere. **If errors occur, open a [Issue ticket.](https://github.com/vis2k/Mirror)**
3. In your ![Mirror](http://i.galtrox.com/index.php/s/LX2KPkezLwazrTS/preview) object replace ![Mirror](http://i.galtrox.com/index.php/s/LTwTTyZLtbmGHY6/preview) with ![Mirror](http://i.galtrox.com/index.php/s/5PJBqPjJiFdqxG9/preview) 

## Building
1. When Building your game you have to place steam_appid.txt into the dir of the game. If you cant find it well, just make a steam_appid.txt and place 480 in side.

![Mirror](http://i.galtrox.com/index.php/s/GiGEJHXXHr4y7gs/preview)

2. When running the game make sure you have placed it into steam as a **Non-Steam Game**

## Host
1. Open your game though steam
2. Host your game ![Mirror](http://i.galtrox.com/index.php/s/ycNEwXKf5jdYD8T/preview)
3. if it says your playing "Spacewar" congrats its working!

**Note: You can run it in Unity aswell**

## Client
1. Send the game to your buddy.
2. The client needs the steam64id of the host to be able to connect.
3. Place the steam64id into ![Mirror](http://i.galtrox.com/index.php/s/py8ZgqtkbrzyC3B/preview) then click "lan Client"
4. Bing bash bong DONE!
