using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class AbilityManager : MonoBehaviour
{
    [Tooltip("RightTrigger, No Cooldown, ResourceGainer")]
    public Ability basicAbility;
    [Space, Tooltip("X on the Playstation Controller, A on the Xbox Controller")]
    public Ability ability1;

    InputDevice inputDevice;

	void Update ()
    {
        inputDevice = InputManager.ActiveDevice;

        if(inputDevice.RightTrigger.WasPressed && Aim.isAiming)
        {
            if(basicAbility.readyToFire)
            basicAbility.FireAbility();
        }
        else if(inputDevice.Action1.WasPressed && Aim.isAiming && !Movement.moving)
        {
            if (ability1.readyToFire)
                ability1.FireAbility();
        }
	}
}
