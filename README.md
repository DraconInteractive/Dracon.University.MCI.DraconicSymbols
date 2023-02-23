# Dracon.University.MCI.DraconicSymbols
This repository contains the accompanying creative work to my exegesis paper titled "The Impact of Draconic Signs upon My Game Design Process".

This creative work takes the form of a VR stealth game, made in the Unity 3D game engine. Within the repository are the core project files, and all of the assets I have used. 

In order to play this project, you must install one of the precompiled .apk files onto a Meta Quest 2 device. This will require one of two tools: ADB or SideQuest. 

**ADB**
If you are comfortable with using a CLI, then ADB, the Android Debug Bridge is the simplest and fastest tool available for installing .apk files on mobile devices (including the Meta Quest). If you have Android Studio installed, then you already will have access to ADB, a quick google will show you how. If you do not, you may download the standalone platform-tools file that contains ADB here: https://developer.android.com/studio/releases/platform-tools

Once you have your platform-tools file (I will assume you have to extract it), navigate into it and you will see adb.exe. Run a terminal from this location, or add that location to your system PATH. 

Ensure your headset is plugged in, and you have accepted whatever permissions popups have appeared. The command is: 
```
adb install -g {path to my file}
```
Your headset should be automatically detected, and the apk will be installed. Hooray! You can now launch the game from the "Unknown Sources" section of your Meta library. 

**SideQuest**
SideQuest is an amazing piece of software that allows many indie developers a method to allowing the wider public access to their games. This is via installing an apk, so it is very suitable for our purpose. 

The process is very simple, so much so that I will not write every instruction here. Simply download and install SideQuest from this location: https://sidequestvr.com/setup-howto. Note that you will want the Advanced Installation to be able to install .apk files. 

SideQuest should have all the instructions you need, it is a very user friendly program! 

