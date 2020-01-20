# FizzySteamyMirror

This is a community maintained repo forked from [FizzCube](https://github.com/FizzCube/FizzySteamyMirror). Mirror [docs](https://mirror-networking.com/docs/Transports/Fizzy.html).

FizzySteamyMirror brings together [Steam](https://store.steampowered.com/) and [Mirror](https://github.com/vis2k/Mirror) utilising Async of a Steam P2P network transport layer for **Mirror**.

## Dependencies
If you want an easy import, skip the steps bellow & download the **[unitypackage](https://github.com/Raystorms/FizzySteamyMirror/releases)**, it has Steamworks.Net already included. 

**Note: If you already have Steamworks.Net in your project, you might need to delete either your import or the one included in the release.**

Both of these projects need to be installed and working before you can use this transport.
1. [Steamworks.NET](https://github.com/rlabrecque/Steamworks.NET) FizzySteamyMirror relies on Steamworks.NET to communicate with the [Steamworks API](https://partner.steamgames.com/doc/sdk). **Requires .Net 4.x**  
2. [Mirror](https://github.com/vis2k/Mirror) FizzySteamyMirror is also obviously dependant on Mirror which is a streamline, bug fixed, maintained version of UNET for Unity. **Recommended [Stable Version](https://assetstore.unity.com/packages/tools/network/mirror-129321)**

## Setting Up

1. install the dependencies **[Download the unitypackage](https://github.com/Raystorms/FizzySteamyMirror/releases)**
2. Create a empty object in your scene and attach the **"Steam Manager"** script to it.
3. In your **"NetworkManager"** object replace **"Telepathy"** script with **"FizzySteamyMirror"** script.

## Building
1. When Building your game you have to place **"steam_appid.txt"** into the directory of the game. If you cant find it well, just make a **"steam_appid.txt"** and place **480** in side.

2. When running the game make sure you have placed it into steam as a **Non-Steam Game** **Note: This is not required, but some have reported their steam SDK not working without doing this.**

**Note: The 480(Spacewar) appid is a very grey area, technically, it's not allowed but they don't really do anything about it. When you have your own appid from steam then replace the 480 with your own game appid.
If you know a better way around this please make a [Issue ticket.](https://github.com/Raystorms/FizzySteamyMirror/issues)**

## Host
To be able to have your game working you need to make sure you have steam running in the background. 
**Note: You can also run it in Unity**

1. Host your game
2. if it says your playing **"Spacewar"** in Steam **congrats its working!**

## Client
Before sending your game to your buddy make sure you have your **steamID64** ready. To find your **steamID64** goto **[Steamid.io](https://steamid.io/lookup)** and enter your steam profile URL.

1. Send the game to your buddy.
2. Your buddy needs your **steamID64** to be able to connect.
3. Place the **steamID64** into **"localhost"** then click **"Lan Client"**
5. Then they will be connected to you.

## Testing your game locally

You cant connect to yourself locally while using **FizzySteamyMirror** since it's using steams P2P. If you want to test your game locally you'll have to use **"Telepathy Transport"** instead of **"Fizzy Steamy Mirror"**.
