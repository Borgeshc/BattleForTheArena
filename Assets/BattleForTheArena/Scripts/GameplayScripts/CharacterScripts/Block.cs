using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Block : MonoBehaviour
{
    public static bool isBlocking;
    public static bool canBlock;

    Animator anim;
    InputDevice inputDevice;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        canBlock = true;
    }

    void Update()
    {
        inputDevice = InputManager.ActiveDevice;

        if(canBlock)
        {
            if (inputDevice.LeftBumper && !Aim.isAiming)
            {
                Movement.canMove = false;
                if (!isBlocking)
                    isBlocking = true;

                if (!anim.GetBool("Block"))
                    anim.SetBool("Block", true);
            }
            else
            {
                Movement.canMove = true;
                if (isBlocking)
                    isBlocking = false;

                if (anim.GetBool("Block"))
                    anim.SetBool("Block", false);
            }
        }
        else
        {
            Movement.canMove = true;
            if (isBlocking)
            isBlocking = false;

            if(anim.GetBool("Block"))
            anim.SetBool("Block", false);
        }
    }
}