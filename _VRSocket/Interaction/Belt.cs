using UnityEngine;

public class Belt : Connector
{
    [Range(0.5f, 0.75f)]
    public float height = 0.5f;
    private Transform head = null;

    private void Start()
    {        
    }

    private void Update()
    {
        //Belt-specific positioning
        //PositionUnderHead();
        //RotateWithHead();
    }
}
