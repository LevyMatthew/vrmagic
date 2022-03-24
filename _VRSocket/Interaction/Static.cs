using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static : Interactable
{
    public GameObject spawnPrefab = null;

    public override void StartInteraction(Hand hand)
    {
        Socket handSocket = hand.GetSocket();
        CreateObject(handSocket);

    }

    private void CreateObject(Socket socket)
    {
        if(socket.GetStoredObject())
            return;
        
        GameObject newObject = Instantiate(spawnPrefab, socket.transform.position, Quaternion.identity);
        Moveable moveable = newObject.GetComponent<Moveable>();

        moveable.AttachNewSocket(socket);
    }
}
