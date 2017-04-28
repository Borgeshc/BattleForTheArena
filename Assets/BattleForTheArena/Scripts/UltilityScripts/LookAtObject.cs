using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public GameObject targetObject;
    
	void Update ()
    {
        transform.LookAt(targetObject.transform);
	}
}
