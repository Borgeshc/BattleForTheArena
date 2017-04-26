using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class AbilityManager : MonoBehaviour
{
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
            if(rightTrigger.readyToFire)
                rightTrigger.FireAbility();
        }
        else if(inputDevice.Action3.WasPressed)
        {
            if (action3.readyToFire)
                action3.FireAbility();
        }
        else if (inputDevice.Action2.WasPressed)
        {
            if (action2.readyToFire)
                action2.FireAbility();
        }
        else if (inputDevice.Action4.WasPressed)
        {
            if (action4.readyToFire)
                action4.FireAbility();
        }
    }
}
