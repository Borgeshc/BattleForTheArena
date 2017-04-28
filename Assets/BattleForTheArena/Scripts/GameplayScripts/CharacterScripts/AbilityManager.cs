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

    [Tooltip("RightTrigger, Short Cooldown, ResourceGainer")]
    public UseAbility rightTrigger;
    [Space, Tooltip("Circle on the Playstation Controller, B on the Xbox Controller")]
    public UseAbility action2;
    [Space, Tooltip("Triangle on the Playstation Controller, Y on the Xbox Controller")]
    public UseAbility action3;
    [Space, Tooltip("Square on the Playstation Controller, X on the Xbox Controller")]
    public UseAbilityToo action4;

    InputDevice inputDevice;

	void Update ()
    {
        inputDevice = InputManager.ActiveDevice;

        if(inputDevice.RightTrigger.WasPressed)
        {
            if (rightTrigger.readyToFire)
            {
                OnRightTriggerPressed();
                //rightTrigger.CmdFireAbility();
            }
        }
        else if (inputDevice.Action2.WasPressed)
        {
            if (action2.readyToFire)
            {
                OnActionTwoPressed();
                //action2.CmdFireAbility();
            }
        }
        else if(inputDevice.Action3.WasPressed)
        {
            if (action3.readyToFire)
            {
                OnActionThreePressed();
                //action3.CmdFireAbility();
            }
        }        
        else if (inputDevice.Action4.WasPressed)
        {
            if (action4.readyToFire)
            {
                OnActionFourPressed();
                //action4.CmdFireAbility();
            }
        }
    }
}
