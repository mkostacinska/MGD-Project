# MULTIPLATFORM GAME DEVELOPMENT PROJECT - GROUP W :desktop_computer:
## Assets used :art: :

The following assets are used in the prototype submission:

- video tutorial for health bar implementation: 
<br>https://www.youtube.com/watch?v=BLfNP4Sc_iA (published 2/2020, brackeys on YouTube.)
- video tutorial explaining how to change between scenes for the game over screen: 
<br>https://www.youtube.com/watch?v=K4uOjb5p3Io (published 12/2020, CocoCode on YouTube.)
- lecture videos for scripting player movement
- video tutorial for hotbar implementation: 
<br>https://www.youtube.com/watch?v=aUc1Qu9_pBs (published 2/2016, MrBuFF on YouTube.)

The following assets have been used in the final game submission:

- video tutorial for scene transistions: 
<br>https://www.youtube.com/watch?v=CE9VOZivb3I&list=WL&index=2&t=614s (published 1/2020, brackeys on YouTube.)
- video tutorial for menus: 
<br>https://www.youtube.com/watch?v=pcyiub1hz20&list=WL&index=14 (published 6/2022, DB Dev on YouTube)
- video tutorial for pause menu: 
<br>https://www.youtube.com/watch?v=bxKEftSIGiQ&list=WL&index=10&t=604s (published 10/2022, DB Dev on YouTube)
- video tutorial for volume slider and volume controller: 
<br>https://www.youtube.com/watch?v=k2vOeTK0z2g&list=WL&index=2 (published 6/2021, SpeedTutor on YouTube)
-  video tutorial for rebinding controls: 
<br>https://www.youtube.com/watch?v=csqVa2Vimao&list=WL&index=10&t=745s (published 4/2021, used for rebinding controls, samyam on Youtube) (this code from this was used to modify the scripts from the rebinding UI extention for the input system package that was also used for rebinding)
- video tutorial for a gamepad cursor: 
<br>https://www.youtube.com/watch?v=Y3WNwl1ObC8 (published 10/2021, samyam on YouTube)
- unity forum post for persistent music: 
<br>http://answers.unity.com/answers/1260412/view.html
- unity documentation and manual pages: 
<br>https://docs.unity3d.com

## Gameplay Tutorial :video_game: :
### Brief Game Introduction:
The player is spawned on a sky island filled with enemies. The goal of the game is to collect three keys
(spread across the map) in order to unlock the bridge to the final island, as well as defeat all the enemies
across all of the existing islands. 

### Controls (Keyboard and Mouse / Gamepad): 
Walk: WASD / Left Joystick

Attack: Left Click / Right Trigger

Pick Up Items: E / PS: Triangle / XBOX:Y

Jump: Space / PS: X / XBOX: A

Change Controls: Tab / Select

Gamepad Cursor Mode: Right Stick Press

Change Weapon: Scroll Wheel / Number Buttons / Left Shoulder & Trigger / D-Pad

Pause: Esc / Select

Character Direction: Cursor Position / Right Stick Direction

The controls can be rebinded in the game settings, to reflect the user preferences.

## Folder Structure 📁:
* [Animations](/MGD-Project/Assets/Animations)
* [Audio](/MGD-Project/Assets/Audio)
* [Final Game](/MGD-Project/Assets/Final%20Game)
	* [Prefabs](/MGD-Project/Assets/Final%20Game/Prefabs)
		* [Systems](/MGD-Project/Assets/Final%20Game/Prefabs/Systems)
		* [UI](/MGD-Project/Assets/Final%20Game/Prefabs/UI)
		* [Objects](/MGD-Project/Assets/Final%20Game/Prefabs/Objects)
	* [Scenes](/MGD-Project/Assets/Final%20Game/Scenes)
* [Graphics](/MGD-Project/Assets/Graphics)
* [Input System](/MGD-Project/Assets/Input%20System)
* [Models](/MGD-Project/Assets/Models)
	* [Enemies](/MGD-Project/Assets/Models/Enemies)
		* [Plant](/MGD-Project/Assets/Models/Enemies/Plant)
		* [Slime](/MGD-Project/Assets/Models/Enemies/Slime)
		* [Snail](/MGD-Project/Assets/Models/Enemies/Snail)
	* [Environment](/MGD-Project/Assets/Models/Environment)
	* [Player](/MGD-Project/Assets/Models/Player)
	* [Weapons](/MGD-Project/Assets/Models/Player)
* [Scripts](/MGD-Project/Assets/Scripts)
	* [Combat System](/MGD-Project/Assets/Scripts/Combat%20System)
	* [Game System](/MGD-Project/Assets/Scripts/Game%20System)
	* [Helper Classes](/MGD-Project/Assets/Scripts/Helper%20Classes)
	* [Inventory System](/MGD-Project/Assets/Scripts/Inventory%20System)
	* [Player Input](/MGD-Project/Assets/Scripts/Player%20Input)
	* [UI](/MGD-Project/Assets/Scripts/UI)

WARNING: There are errors in the editor if you start on a scene other than Main Menu. If you want to run a specific scene, add the [InputManager](/MGD-Project/Assets/Final%20Game/Prefabs/Systems/InputManager.prefab) and [EventSystem](/MGD-Project/Assets/Final%20Game/Prefabs/Systems/EventSystem.prefab) prefabs into the scene. Remove these from every scene other than Main Menu before building.
