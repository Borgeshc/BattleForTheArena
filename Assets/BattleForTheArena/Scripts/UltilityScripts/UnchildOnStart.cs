using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnchildOnStart : MonoBehaviour
{
    
	void Start ()
    {
        transform.parent = null;
	}
}
