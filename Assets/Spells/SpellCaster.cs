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
            public AttachmentMode attachMode;
            public AttachmentPoint attachmentPoint;
            [HideInInspector] public GameObject spellInstance;
            [HideInInspector] public Spell spell;
        }

        public enum AttachmentPoint // your custom enumeration
        {
            ObjectHold,
            IndexFingerTip,
        };

        public enum AttachmentMode
        {
            HandAttach,
            TargetTransform
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

                    if (sac.attachMode == AttachmentMode.HandAttach)
                    {
                        Hand hand = GetComponent<Hand>();
                        sac.spellInstance.SetActive(true);
                        hand.AttachObject(sac.spellInstance, GrabTypes.Scripted);
                    }
                    else if (sac.attachMode == AttachmentMode.TargetTransform)
                    {
                        sac.spell.Begin(target);
                    }

                }
                else if (sac.spellInstance != null && sac.action.GetStateUp(behaviourPose.inputSource))
                {
                    if (sac.attachMode == AttachmentMode.HandAttach)
                    {
                        Hand hand = GetComponent<Hand>();
                        hand.DetachObject(sac.spellInstance, false);
                    }
                    else
                    {
                        sac.spell.Release(GetVelocity());
                    }

                    if (sac.spell.destroyTime >= 0f)
                    {
                        Destroy(sac.spellInstance, sac.spell.destroyTime);
                    }
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