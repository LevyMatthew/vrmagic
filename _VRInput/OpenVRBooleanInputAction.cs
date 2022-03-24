using UnityEngine;

[CreateAssetMenu(fileName = "Key Code Input Action")]
public class LegacyInputBooleanInputAction : BooleanInputAction
{
    public KeyCode keyCode;

    public override bool FallingEdge()
    {
        return Input.GetKey(keyCode);
    }

    public override bool RisingEdge()
    {
        return Input.GetKeyDown(keyCode);
    }

    public override bool GetState()
    {
        return Input.GetKeyUp(keyCode);
    }
}