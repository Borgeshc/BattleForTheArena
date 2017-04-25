using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class AbilityManager : MonoBehaviour
{
    [Tooltip("RightTrigger, Short Cooldown, ResourceGainer")]
    public Ability basicAbility;
    [Space, Tooltip("X on the Playstation Controller, A on the Xbox Controller")]
    public Ability ability1;
    [Space, Tooltip("X on the Playstation Controller, A on the Xbox Controller")]
    public Ability ability2;
    [Space, Tooltip("X on the Playstation Controller, A on the Xbox Controller")]
    public Ability ability3;
    [Space, Tooltip("X on the Playstation Controller, A on the Xbox Controller")]
    public Ability ability4;

    InputDevice inputDevice;

	void Update ()
    {
        inputDevice = InputManager.ActiveDevice;

        if(inputDevice.RightTrigger.WasPressed)
        {
            if(basicAbility.readyToFire)
            basicAbility.FireAbility();
        }
        else if(inputDevice.Action1.WasPressed)
        {
            if (ability1.readyToFire)
                ability1.FireAbility();
        }
        else if (inputDevice.Action2.WasPressed)
        {
            if (ability2.readyToFire)
                ability2.FireAbility();
        }
        else if (inputDevice.Action4.WasPressed)
        {
            if (ability3.readyToFire)
                ability3.FireAbility();
        }
        else if (inputDevice.RightBumper.WasPressed)
        {
            if (ability4.readyToFire)
                ability4.FireAbility();
        }

    }
}
