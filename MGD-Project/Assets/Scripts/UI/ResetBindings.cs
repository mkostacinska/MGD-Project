using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetBindings : MonoBehaviour // assets used: https://www.youtube.com/watch?v=csqVa2Vimao&list=WL&index=10&t=745s
{
    [SerializeField] private InputActionAsset inputActions;

    /// <summary>
    /// Restore all bingings to their defaults across all input maps.
    /// </summary>
    public void RemoveBindings(){
        foreach (InputActionMap map in inputActions.actionMaps){
            map.RemoveAllBindingOverrides();
        }
    }    
}
