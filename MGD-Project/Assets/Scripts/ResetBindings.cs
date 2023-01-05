using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetBindings : MonoBehaviour//from:https://www.youtube.com/watch?v=csqVa2Vimao&list=WL&index=12
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
