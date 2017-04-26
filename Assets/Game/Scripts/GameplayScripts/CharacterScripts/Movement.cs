using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Movement : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float rotationSpeed;
    public float dodgeSpeed;
    public float dodgeDistance;

    public static bool moving;
    public static bool canMove;

    CharacterController cc;
    float horizontal;
    float vertical;
    Vector3 direction;
    Animator anim;
    InputDevice inputDevice;
    bool dodging;
    Vector3 destination;

    void Start ()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        canMove = true;
	}

    void Update()
    {
        inputDevice = InputManager.ActiveDevice;

        horizontal = inputDevice.LeftStick.X;
        vertical = inputDevice.LeftStick.Y;

        direction = new Vector3(horizontal, 0, vertical);

        if(canMove)
        {
            if (direction != Vector3.zero)
                moving = true;
            else
                moving = false;

            if (!inputDevice.LeftStickButton)
                Move();
            else
                Sprint();

            if (direction != Vector3.zero && inputDevice.Action3 && !dodging)
            {
                dodging = true;
                direction = transform.TransformDirection(direction);
                destination = transform.position + direction * dodgeDistance;
                anim.SetBool("Dodge", true);
                anim.SetFloat("DodgeDirectionX", horizontal);
                anim.SetFloat("DodgeDirectionY", vertical);
               // StartCoroutine(Dodging());
            }
        }

        if (dodging && Vector3.Distance(transform.position, destination) > 1)
        {
            Time.timeScale = .5f;   //This makes the animation look really good and its so fast its barely noticable that it happened. I would like to test how this affects the game when networking is a thing. It sounds scary but it is needed!!
            //http://answers.unity3d.com/questions/625945/timescale-slowmo-and-multiplayer.html
            //Someone asked about this in that link

            canMove = false;
            transform.position = Vector3.Lerp(transform.position, destination, dodgeSpeed * Time.deltaTime);
        }
        else
        {
            Time.timeScale = 1;
            canMove = true;
            dodging = false;
            anim.SetBool("Dodge", false);
        }
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
