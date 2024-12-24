# Xenomech
Server-side C# code used in the Neverwinter Nights Xenomech module.

Game: Neverwinter Nights: Enhanced Edition

Website: TBA

Discord: TBA

Forums: TBA

# Project Description

This project contains the C# source code used on the Xenomech server. 

It serves as a replacement for NWScript and handles most server features and functions. This is possible by using the NWNX_DotNet plugin for NWNX.

Refer to the quick start guide below and be sure to post any issues on our forums. The link to the forums is above.

# Prerequisites: 

Git: https://git-scm.com/downloads
  
Docker: https://www.docker.com/products/docker-desktop/
  
Visual Studio 2022: https://www.visualstudio.com/downloads/

Neverwinter Nights: https://store.steampowered.com/app/704450/Neverwinter_Nights_Enhanced_Edition/

# Installation:

1. Fork this repo.

2. At command line: ``git clone --recursive https://github.com/<your_username>/NWN.Xenomech.git``

3. Run NWN.Xenomech/Module/PackModule.cmd to generate the Xenomech.mod file.
  
4. Open NWN.Xenomech.sln with Visual Studio
  
5. Right click on NWN.Xenomech.Runner within Visual Studio and select "Set as Startup Project"
  
6. Click the green run button at the top of Visual Studio. Note that this will take a while the first time you do it.

# Troubleshooting

**'TLK does not exist' error:**

This happens because you didn't clone with the --recursive command. Redo step 2.

**'Type initializer for Ductus.FluentDocker.Extensions.CommandExtensions threw an exception' error:**

You need to install docker.

**'Missing required HAK file' error on login**

Copy the files in the hak directory to your NWN play directory. Alternatively, edit nwn.ini to point to the debugserver/hak folder.

**'Missing required TLK file' error on login**

Copy the xenomech.tlk file in the tlk directory to your NWN play directory. Alternatively, edit nwn.ini to point to the debugserver/tlk folder.

**'Unable to load module' error**

Delete all docker containers and images, then delete all files in the debugserver/modules, debugserver/tlk, and debugserver/hak folders. Then rerun the application.

# Getting Help

If you need help with anything related to Star Wars: Legends of the Old Republic please feel free to contact us on our Discord here: TBA

For NWNX and Docker related issues please look for help in the NWNX Discord channel here: https://discord.gg/m2hJPDE
