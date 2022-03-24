using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : Connector
{
    private Moveable storedObject;
    private Joint joint;

    private void Awake()
    {
        joint = GetComponent<Joint>();      
        Rigidbody connectedBody = joint.connectedBody;
        if (connectedBody)
        {
            Attach(connectedBody.GetComponent<Moveable>());
        }
    }

    public void Attach(Moveable newObject)
    {
        if (storedObject)
            return;

        storedObject = newObject;
        storedObject.transform.position = transform.position;
        storedObject.transform.rotation = Quaternion.identity;

        Rigidbody targetBody = storedObject.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;
    }

    public void Detach()
    {
        if (!storedObject)
            return;

        joint.connectedBody = null;
        storedObject = null;
    }

    public Moveable GetStoredObject()
    {
        return storedObject;
    }

}