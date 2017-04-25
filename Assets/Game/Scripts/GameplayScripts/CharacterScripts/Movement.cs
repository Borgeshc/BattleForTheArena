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
                anim.SetTrigger("Dodge");
                anim.SetFloat("DodgeDirectionX", horizontal);
                anim.SetFloat("DodgeDirectionY", vertical);
                StartCoroutine(Dodging());
            }
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

    IEnumerator Dodging()
    {
        yield return new WaitForSeconds(.2f);
        direction = transform.TransformDirection(direction);
        transform.position = Vector3.Lerp(transform.position, transform.position + direction * dodgeDistance , dodgeSpeed * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        dodging = false;
    }
}
