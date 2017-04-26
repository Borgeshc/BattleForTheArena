using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class AbilityManager : MonoBehaviour
{
    [Tooltip("RightTrigger, Short Cooldown, ResourceGainer")]
    public Ability rightTrigger;
    [Space, Tooltip("X on the Playstation Controller, A on the Xbox Controller")]
    public Ability rightBumper;
    [Space, Tooltip("X on the Playstation Controller, A on the Xbox Controller")]
    public Ability action1;
    [Space, Tooltip("X on the Playstation Controller, A on the Xbox Controller")]
    public Ability action2;
    [Space, Tooltip("X on the Playstation Controller, A on the Xbox Controller")]
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
        else if(inputDevice.Action1.WasPressed)
        {
            if (action1.readyToFire)
                action1.FireAbility();
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
        else if (inputDevice.RightBumper.WasPressed)
        {
            if (rightBumper.readyToFire)
                rightBumper.FireAbility();
        }

    }
}
