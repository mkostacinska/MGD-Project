using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetBindings : MonoBehaviour//https://www.youtube.com/watch?v=csqVa2Vimao&list=WL&index=10&t=745s -accessed 11/2022-1/2023 , published 4/2021, used for rebinding controls ,samyam on Youtube.
{
    [SerializeField]
    private InputActionAsset inputActions;

    public void RemoveBindings(){
        foreach (InputActionMap map in inputActions.actionMaps){//for every action
            map.RemoveAllBindingOverrides();//resetAllbindings
        }
    }

    //PlayerPrefs.DeleteKey("rebinds");//delete persistance
    
}
