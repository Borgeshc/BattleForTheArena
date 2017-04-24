using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Movement : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float rotationSpeed;

    public static bool moving;

    CharacterController cc;
    float horizontal;
    float vertical;
    Vector3 direction;
    Animator anim;
    InputDevice inputDevice;

	void Start ()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
	}

    void Update()
    {
        inputDevice = InputManager.ActiveDevice;

        horizontal = inputDevice.LeftStick.X;
        vertical = inputDevice.LeftStick.Y;

        direction = new Vector3(horizontal, 0, vertical);

        if (direction != Vector3.zero)
            moving = true;
        else
            moving = false;


        if (!inputDevice.LeftStickButton)
            Move();
        else
            Sprint();
	}

    void Move()
    {
        cc.SimpleMove(transform.forward * vertical * speed * Time.deltaTime);
        cc.SimpleMove(transform.right * horizontal * speed * Time.deltaTime);

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);
    }

    void Sprint()
    {
        cc.SimpleMove(transform.forward * vertical * sprintSpeed * Time.deltaTime);
        cc.SimpleMove(transform.right * horizontal * sprintSpeed * Time.deltaTime);

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);
    }
}
