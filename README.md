# OpenLong
OpenLog is a source fork based on "Long (龙)"

This project is a fork from Comet and all rights over Comet are reserved by Gareth Jensen "Spirited".
It is also a reimagination of Canyon which may be a good thing (or not).
The project is split between three servers: an account server, game server and ai server. The account server authenticates players, while the game server services players in the game world and the ai server handles ai over the NPCs in the world. This simple three-server architecture acts as a good introduction into server programming and concurrency patterns. The server is interoperable with the Conquer Online game client, but a modified client will not be provided.
This still a work in progress and is not recommended to starters. No support will be given to creating events, NPCs or anything like that. But if you want to work with Canyon you may report bugs and we will keep the main repository updated with bug fixes to whoever wants to try it.
When the live server leaves the Beta Stage, we will start keeping stable versions of Canyon in the main main repository, if you download from development make sure you know what you are doing and that you are ready to face bugs.

## Details about source
``Working with version 6609``

When creating a new character in the conquer_account table, create the id for example 1 and change it to 1000001

## Guide for setup
1- Copy folder map/map and map/Scenes from client to Bin folder of Solution

2- Copy lua folder from solution root

3- Build all solution (not only gameserver and accountserver)

4- In table account_cq.realm of the database mysql change the ip and user, password etc

## TODO
- PK Explorer System
- Prestige System
- Hero System
- Daily Quests (Incomplete)
- Daily Signin (Tab in Reward) (Incomplete)
- Activeness (Tab in Reward)
- Events (Tab in Reward) (Incomplete)
- Poker
- Bulletin (I think are not implemented but need see if have some parts coded)
- Perfection
- Chat (Ghost channel for example not implemented)
- Some npcs missing (like some npcs for quest of tower and fan)
- Some npcs are showing errors of lua (Example: PirateLord in TwinCity on the Armorer NPC)
- Arenas (I think only work the Qualifier Standard)
- House Furnitures (Incomplete)
- Titles (Not from Wardrobe, that are fine)
- Lottery (Not working 100% fine)
- Mentor (Not 100% implemented)
- Some portals are wrong (Like portal of phoenix city to twin city)


Official source: https://gitlab.com/world-conquer-online/canyon/long/-/tree/develop?ref_type=heads

Author: Felipe Vieira Vendramini

Updates Author & Fix: Javier Vargas Ruiz & Team
