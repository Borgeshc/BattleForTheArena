using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    RaycastHit hit;
    Animator anim;
    Vector3 hitPoint;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
	void Update ()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width /2, Screen.height / 2, 0));
        Debug.DrawLine(ray.origin, Camera.main.transform.forward * 50000000, Color.red);
		if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500))
        {
            if (hit.point != null)
            {
                hitPoint = hit.point;
                print(hitPoint);
            }
        }
	}

    void OnAnimatorIK()
    {
        anim.SetLookAtWeight(1);
        anim.SetLookAtPosition(hitPoint);
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKPosition(AvatarIKGoal.RightHand, hitPoint);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        anim.SetIKRotation(AvatarIKGoal.RightHand, new Quaternion(hitPoint.x, hitPoint.y, hitPoint.z, 0));
    }
}
