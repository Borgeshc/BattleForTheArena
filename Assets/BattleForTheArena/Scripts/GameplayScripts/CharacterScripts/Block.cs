using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Block : MonoBehaviour
{
    public static bool isBlocking;

    Animator anim;
    InputDevice inputDevice;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        inputDevice = InputManager.ActiveDevice;

        if (inputDevice.LeftBumper && !Movement.moving && !Aim.isAiming)
        {
            isBlocking = true;
            anim.SetBool("Block", true);
        }
        else
        {
            isBlocking = false;
            anim.SetBool("Block", false);
        }
    }
}