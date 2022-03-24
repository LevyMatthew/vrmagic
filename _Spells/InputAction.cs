using UnityEngine;

[CreateAssetMenu]
public abstract class BooleanInputAction : ScriptableObject
{
    public abstract bool GetState();
    public abstract bool RisingEdge();
    public abstract bool FallingEdge();
}

