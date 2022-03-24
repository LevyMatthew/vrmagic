using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorToggle : MonoBehaviour
{
    public Material materialA = null;
    public Material materialB = null;

    private bool isA = true;

    public void ToggleColor()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        isA = !isA;
        if (isA)
        {
            renderer.material = materialA;
        }
        else
        {
            renderer.material = materialB;
        }
    }
}
