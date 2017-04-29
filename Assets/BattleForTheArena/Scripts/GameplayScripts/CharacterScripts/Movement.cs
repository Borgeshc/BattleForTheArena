using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;
    public float sprintSpeed;
    public float rotationSpeed;
    public float dodgeSpeed;
    public float dodgeDistance;
    public float dodgeStamConsumption;

    public static bool moving;
    public static bool canMove;

    Image staminaBar;

    bool sprinting;

    CharacterController cc;
    float horizontal;
    float vertical;
    Vector3 direction;
    Animator anim;
    InputDevice inputDevice;
    bool dodging;
    Vector3 destination;
    bool exhausted;

    void Start ()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        canMove = true;
        staminaBar = GameObject.Find("PlayerStamina").GetComponent<Image>();
	}

    void Update()
    {
        inputDevice = InputManager.ActiveDevice;

        horizontal = inputDevice.LeftStick.X;
        vertical = inputDevice.LeftStick.Y;

        direction = new Vector3(horizontal, 0, vertical);

        if (canMove)
        {
            if (direction != Vector3.zero)
                moving = true;
            else
                moving = false;


            if (!inputDevice.LeftStickButton)
            {
                Move();

                if (exhausted)
                    {
                        if (staminaBar.fillAmount >= 1)
                            exhausted = false;
                    }

                    sprinting = false;
                    staminaBar.fillAmount += Time.deltaTime * .25f;
            }
            else if(inputDevice.LeftStickButton)
            {
                if (!exhausted)
                {
                    Sprint();
                    staminaBar.fillAmount -= (Time.deltaTime * .5f);
                }
                else
                {
                    Move();
                }

                if (staminaBar.fillAmount <= 0)
                {
                    exhausted = true;
                }
            }

            if (Block.isBlocking)
            {
                if(!exhausted)
                {
                    Block.canBlock = true;
                    staminaBar.fillAmount -= (Time.deltaTime * .75f);
                    if (staminaBar.fillAmount <= 0)
                    {
                        exhausted = true;
                        Block.canBlock = false;
                    }
                  
                }
                else
                {
                    if (staminaBar.fillAmount >= 1)
                    {
                        exhausted = false;
                        Block.canBlock = true; ;
                    }
                }
            }
            else
            {

                if (exhausted)
                {
                    if (staminaBar.fillAmount >= 1)
                    {
                        exhausted = false;
                    }
                }

                sprinting = false;
                staminaBar.fillAmount += Time.deltaTime * .25f;
            }
            

            if (direction != Vector3.zero && inputDevice.Action1 && !dodging && staminaBar.fillAmount >= .5f)
            {
                dodging = true;
                staminaBar.fillAmount -= dodgeStamConsumption;
                direction = transform.TransformDirection(direction);
                destination = transform.position + direction * dodgeDistance;
                anim.SetBool("Dodge", true);
                anim.SetFloat("DodgeDirectionX", horizontal);
                anim.SetFloat("DodgeDirectionY", vertical);
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

        sprinting = true;

        cc.SimpleMove(transform.forward * vertical * sprintSpeed * Time.deltaTime);
        cc.SimpleMove(transform.right * horizontal * sprintSpeed * Time.deltaTime);

        anim.SetFloat("Vertical", vertical);
        anim.SetFloat("Horizontal", horizontal);
    }
}
