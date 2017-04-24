using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Aim : MonoBehaviour
{
    public static bool isAiming;
    public SpriteRenderer reticle;
    InputDevice inputDevice;

    private void LateUpdate()
    {
        inputDevice = InputManager.ActiveDevice;

        if(inputDevice.LeftTrigger)
        {
            if (!isAiming)
            {
                isAiming = true;
                reticle.enabled = true;
            }
        }
        else
        {
            if (isAiming)
            {
                isAiming = false;
                reticle.enabled = false;
            }
        }
    }
}
