//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;

namespace Valve.VR.Extras
{
    public class FireballThrower : MonoBehaviour
    {
        [SerializeField] public GameObject fireballPrefab;
        [SerializeField] public Rigidbody attachmentPoint;

        public SteamVR_Action_Boolean spawn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

        private GameObject fireballInstance;

        SteamVR_Behaviour_Pose behaviourPose;
        
        private void Awake()
        {
            behaviourPose = GetComponent<SteamVR_Behaviour_Pose>();
        }

        private Vector3 GetVelocity()
        {
            return behaviourPose.GetVelocity();
        }     

        private void FixedUpdate()
        {
            if (fireballInstance == null && spawn.GetStateDown(behaviourPose.inputSource))
            {
                fireballInstance = GameObject.Instantiate(fireballPrefab, transform.position, Quaternion.identity, null);
                Fireball fireball = fireballInstance.GetComponent<Fireball>();
                fireball.Grab(attachmentPoint);
            }
            else if (fireballInstance != null && spawn.GetStateUp(behaviourPose.inputSource))
            {
                Fireball fireball = fireballInstance.GetComponent<Fireball>();
                fireball.Throw(GetVelocity() * 10f);                                
                fireballInstance = null;
            }
        }
    }
}