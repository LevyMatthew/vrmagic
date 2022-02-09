//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;

namespace Valve.VR.Extras
{
    public class SpellCaster : MonoBehaviour
    {

        [System.Serializable]
        public class SpellActionConfig
        {
            public SteamVR_Action_Boolean action;
            public GameObject spellTemplatePrefab;
            public AttachmentPoint attachmentPoint;
            [HideInInspector] public GameObject spellInstance;
            [HideInInspector] public Spell spell;
        }

        public enum AttachmentPoint // your custom enumeration
        {
            ObjectHold,
            IndexFingerTip,
        };

        [SerializeField] public Transform objectHoldPoint;
        [SerializeField] public Transform indexFingerTipPoint;

        [SerializeField] public List<SpellActionConfig> actionConfigs;
        
        SteamVR_Behaviour_Pose behaviourPose;

        private void Awake()
        {
            //behaviourPose = GetComponent<SteamVR_Behaviour_Pose>();
            behaviourPose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        }

        private Vector3 GetVelocity()
        {
            if (behaviourPose)
                return behaviourPose.GetVelocity();
            return Vector3.zero;
        }

        private void DoBooleanActionSpawns()
        {
            foreach (SpellActionConfig sac in actionConfigs)
            {
                if (sac.spellInstance == null && sac.action.GetStateDown(behaviourPose.inputSource))
                {
                    Transform target = transform;
                    if (sac.attachmentPoint == AttachmentPoint.ObjectHold)
                        target = objectHoldPoint;
                    else if (sac.attachmentPoint == AttachmentPoint.IndexFingerTip)
                        target = indexFingerTipPoint;

                    sac.spellInstance = GameObject.Instantiate(sac.spellTemplatePrefab, target.position, target.rotation, null);
                    sac.spell = sac.spellInstance.GetComponent<Spell>();
                    sac.spell.Begin(target);

                }
                else if (sac.spellInstance != null && sac.action.GetStateUp(behaviourPose.inputSource))
                {
                    sac.spell.Release(GetVelocity());
                    sac.spellInstance = null;
                }
            }
        }



        private void FixedUpdate()
        {
            DoBooleanActionSpawns();
        }
    }
}