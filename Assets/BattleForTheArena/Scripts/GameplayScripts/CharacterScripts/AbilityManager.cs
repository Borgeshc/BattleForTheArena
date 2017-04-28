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
    public Ability rightTrigger;
    [Space, Tooltip("Circle on the Playstation Controller, B on the Xbox Controller")]
    public Ability action2;
    [Space, Tooltip("Triangle on the Playstation Controller, Y on the Xbox Controller")]
    public Ability action3;
    [Space, Tooltip("Square on the Playstation Controller, X on the Xbox Controller")]
    public Ability action4;

    InputDevice inputDevice;

	void Update ()
    {
        inputDevice = InputManager.ActiveDevice;

        if(inputDevice.RightTrigger.WasPressed)
        {
            Debug.LogError("right trigger");
            if (rightTrigger.readyToFire)
            {
                OnRightTriggerPressed();
                //rightTrigger.CmdFireAbility();
            }
        }
        else if (inputDevice.Action2.WasPressed)
        {
            Debug.LogError("action 2");
            if (action2.readyToFire)
            {
                OnActionTwoPressed();
                //action2.CmdFireAbility();
            }
        }
        else if(inputDevice.Action3.WasPressed)
        {
            Debug.LogError("action 3");
            if (action3.readyToFire)
            {
                OnActionThreePressed();
                //action3.CmdFireAbility();
            }
        }        
        else if (inputDevice.Action4.WasPressed)
        {
            Debug.LogError("action 4");
            if (action4.readyToFire)
            {
                OnActionFourPressed();
                //action4.CmdFireAbility();
            }
        }
    }
}
