Building The Tools
============================
Note: This has only been tested on Windows, and it is very unlikely the project will work on other platforms due to use of FNA instead of XNA.

1. Clone this repo
2. Create a folder named 'Vanilla' and copy the files salt.exe and Steamworks.NET.dll into it from your game folder.
3. Create a folder named 'MonoMod' and copy all of the package 'net40' versions into it.
4. Additionally, copy all MonoMod files into your game directory.
5. Build the project and the built project will be placed into a folder named Output.
6. Copy salt.exe from this folder to your game folder.

Using The Tools
============================

As of right now, functionality is extremely limited.

If there doesn't exist a file 'input.tas' in the game folder, the game will launch in input recording mode. In this mode, it will create the file 'input.tas' and record all inputs to this file until the game is closed.
If the file 'input.tas' already exists, the game will launch in playback mode. In this mode, user inputs will be ignored and it will instead play back the previously recorded inputs. On finishing replay, it will begin accepting user input again.

Additionally, there are several hotkeys that can be used to control the game speed:
Numpad 0 - Toggle fast forward
Numpad 1 - Toggle pause
Numpad 2 - Advance by one frame (Automatically pauses the game as well)

When the game launches, it defaults to a paused state. You will have to use one of these hotkeys to make it advance.