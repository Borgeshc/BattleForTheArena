using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLightingSettings : MonoBehaviour
{
    public Material skyboxMaterial;
    public bool fog;
    
	void Start ()
    {
        RenderSettings.skybox = skyboxMaterial;
        RenderSettings.fog = fog;
	}
}
