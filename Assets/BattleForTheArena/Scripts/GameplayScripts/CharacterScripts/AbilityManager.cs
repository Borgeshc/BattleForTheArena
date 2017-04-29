using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class AbilityManager : MonoBehaviour
{
    public delegate void OnAbilityUsed();
    public static event OnAbilityUsed OnRightTriggerPressed;
    public static event OnAbilityUsed OnActionTwoPressed;
    public static event OnAbilityUsed OnActionThreePressed;
    public static event OnAbilityUsed OnActionFourPressed;

    InputDevice inputDevice;

	void Update ()
    {
        inputDevice = InputManager.ActiveDevice;

        if(inputDevice.RightTrigger.WasPressed)
        {
            OnRightTriggerPressed();
        }
        else if (inputDevice.Action2.WasPressed)
        {
            OnActionTwoPressed();
        }
        else if(inputDevice.Action3.WasPressed)
        {
            OnActionThreePressed();
        }        
        else if (inputDevice.Action4.WasPressed)
        {
            OnActionFourPressed();
        }
    }
}
