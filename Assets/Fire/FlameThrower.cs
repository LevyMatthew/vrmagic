//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;

namespace Valve.VR.Extras
{
    public class FlameThrower : MonoBehaviour
    {
        [SerializeField] public GameObject firePrefab;
        [SerializeField] public Rigidbody attachmentPoint;

        public SteamVR_Action_Boolean spawn = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

        private GameObject fireInstance;

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
            if (fireInstance == null && spawn.GetStateDown(behaviourPose.inputSource))
            {
                fireInstance = GameObject.Instantiate(firePrefab, transform.position, Quaternion.identity, null);
                Fire fire = fireInstance.GetComponent<Fire>();
                if (fire != null)
                    fire.Spawn(attachmentPoint);
            }
            else if (fireInstance != null && spawn.GetStateUp(behaviourPose.inputSource))
            {
                Fire fire = fireInstance.GetComponent<Fire>();
                fire.Release(GetVelocity());
                fireInstance = null;
            }
        }
    }
}