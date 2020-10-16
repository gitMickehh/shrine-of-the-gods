using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Light2DController : MonoBehaviour
{
    public Light2D myLight;

    public void ChangeIntensity(float intensity)
    {
        myLight.intensity = intensity;
    }
}
