using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 distance;
    public Vector3 zoomedDistance;
    public float zoomSpd = 2.0f;
    public float speed;
    public float rotationSpeed;

    public float xSpeed = 240.0f;
    public float ySpeed = 123.0f;

    public int yMinLimit = -723;
    public int yMaxLimit = 877;

    private float x = 0.0f;
    private float y = 0.0f;

    InputDevice inputDevice;
    float horizontal;
    float vertical;
    Vector3 position;
    Quaternion rotation;
    GameObject player;
    Animator anim;

    public void Start()
    {
        player = GameObject.Find("Player");
        anim = player.GetComponent<Animator>();
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    public void LateUpdate()
    {
        inputDevice = InputManager.ActiveDevice;

        horizontal = inputDevice.RightStick.X;
        vertical = inputDevice.RightStick.Y;
        
        if(horizontal != 0 && !Movement.moving)
        {
            anim.SetLayerWeight(3, 1);
            anim.SetFloat("Horizontal2", horizontal);
        }
        else
        {
            anim.SetLayerWeight(3, 0);
        }

        if (target)
        {
            x -= -horizontal * xSpeed * 0.02f;
            y += -vertical * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            rotation = Quaternion.Euler(y, x, 0.0f);

            if(Aim.isAiming)
                position = rotation * zoomedDistance + target.position;
            else
                position = rotation * distance + target.position;

            if (horizontal != 0)
            {
                player.transform.Rotate(new Vector3(0, horizontal * (rotationSpeed * 2.75f) * Time.deltaTime, 0));
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime); ;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;
        return Mathf.Clamp(angle, min, max);
    }
}
