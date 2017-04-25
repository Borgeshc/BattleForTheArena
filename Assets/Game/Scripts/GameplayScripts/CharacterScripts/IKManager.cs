using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    RaycastHit hit;
    Animator anim;
    Transform hitPoint;

    public RootMotion.FinalIK.BipedIK ik;


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
                hitPoint = hit.transform;
            }
        }
	}

    private void LateUpdate()
    {
        ik.solvers.aim.target = hitPoint;
        ik.solvers.rightHand.target = hitPoint;
        ik.solvers.lookAt.target = hitPoint;
    }

    void OnAnimatorIK()
    {

    }
}
