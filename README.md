# MULTIPLATFORM GAME DEVELOPMENT PROJECT - GROUP W :desktop_computer:

## Assets used :art: :

The following assets are used in the prototype submission:

- video tutorial for health bar implementation: https://www.youtube.com/watch?v=BLfNP4Sc_iA(published 2/2020, brackeys on YouTube.)
- video tutorial explaining how to change between scenes for the game over screen: https://www.youtube.com/watch?v=K4uOjb5p3Io (published 12/2020, CocoCode on YouTube.)
- lecture videos for scripting player movement
- video tutorial for hotbar implementation: https://www.youtube.com/watch?v=aUc1Qu9_pBs(published 2/2016,MrBuFF on YouTube.)

The following assets can be found in the repository, but are NOT a part of the prototype submission:
- player models to be used in the final version of the game: https://www.mixamo.com
- animations to be used along with player models: https://www.mixamo.com

- video tutorial for scene transistions: https://www.youtube.com/watch?v=CE9VOZivb3I&list=WL&index=2&t=614s (Accessed 12/2022, published 1/2020, brackeys on YouTube.)
- video tutorial for menus: https://www.youtube.com/watch?v=pcyiub1hz20&list=WL&index=14 (Accessed 11/2022, published 6/2022, DB Dev on YouTube)
- video tutorial for Pause menu:https://www.youtube.com/watch?v=bxKEftSIGiQ&list=WL&index=10&t=604s(Accessed 11/2022, published 10/2022, DB Dev on YouTube)
- video tutorial for volume slider and volume controller:https://www.youtube.com/watch?v=k2vOeTK0z2g&list=WL&index=2(Accessed 11/2022, published 6/2021, SpeedTutor on YouTube)
-  video tutorial for rebinding controls:https://www.youtube.com/watch?v=csqVa2Vimao&list=WL&index=10&t=745s (accessed 11/2022-1/2023 , published 4/2021, used for rebinding controls ,samyam on Youtube)(this code from this was used to modify the scripts from the rebinding UI extention for the input system package that was also used for rebinding)

unity documentaion and manual pages used:
-  Input system and input binding:https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/api/UnityEngine.InputSystem.InputBinding.html
-  Mouse events:https://docs.unity3d.com/Manual/UIE-Mouse-Events.html
-  IEnumerator and startcoroutine:https://docs.unity3d.com/ScriptReference/MonoBehaviour.StartCoroutine.html
-  SerializeField:https://docs.unity3d.com/ScriptReference/SerializeField.html


## Gameplay Tutorial :video_game: :
### Brief Game Introduction:
The player is spawned on a sky island filled with enemies. The goal of the game is to collect three key objects
(yellow cubes spread across the map) in order to unlock the bridge to the final island, as well as defeat the final
enemies located on said island.

### Controls: 
- W/A/S/D for player movement
- Space for jumping
- Mouse 1 (left mouse button) for attacking; the player rotates to face the cursor
- Mouse Scroll to change between weapons
- E to pick up objects
- Esc to Pause game (while in main scene)

### Additional information:
Upon death, the game can be restarted by pressing the 'RESTART' button. After winning, the game can be 
replayed by pressing the 'REPLAY' button.
