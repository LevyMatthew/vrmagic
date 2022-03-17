using System;
using UnityEngine;

public abstract class BooleanInputAction : MonoBehaviour
{
    public abstract bool GetState();
    public abstract bool RisingEdge();
    public abstract bool FallingEdge();
}

