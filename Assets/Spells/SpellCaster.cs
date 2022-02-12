//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using Valve.VR.Extras;
using Valve.VR;

public class SpellCaster : MonoBehaviour
    {

        public GameObject spellPreviewPrefab;
        private SpellPreview spellPreview;


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
            Hand,
            Palm,
            IndexFingerTip,
            MiddleFingerTip,
            UlnarBorder,
            PreviewSelect
    };

        [SerializeField] public Transform palmTransform;
        [SerializeField] public Transform indexFingerTipTransform;
        [SerializeField] public Transform middleFingerTipTransform;
        [SerializeField] public Transform ulnarBorderTransform;

        [SerializeField] public List<SpellActionConfig> actionConfigs;
        
        SteamVR_Behaviour_Pose behaviourPose;

    public void Punch()
    {
        Vector3 velocity = GetComponent<KinematicTracker>().velocity;
        foreach (SpellActionConfig sac in actionConfigs)
        {
            if (sac.spellInstance != null)
            {
                sac.spell.Punch(velocity);
            }
        }
    }

    private void Awake()
        {
            //behaviourPose = GetComponent<SteamVR_Behaviour_Pose>();
            behaviourPose = GetComponentInParent<SteamVR_Behaviour_Pose>();
            if (spellPreviewPrefab != null)
            {
                GameObject spellPreviewInstance = Instantiate(spellPreviewPrefab, transform);
                spellPreview = spellPreviewInstance.GetComponent<SpellPreview>();
            }
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
                    Transform target = null;

                    switch (sac.attachmentPoint)
                    {
                        case AttachmentPoint.Hand:
                            target = transform;
                            break;
                        case AttachmentPoint.Palm:
                            target = palmTransform;
                            break;
                        case AttachmentPoint.IndexFingerTip:
                            target = indexFingerTipTransform;
                            break;
                        case AttachmentPoint.MiddleFingerTip:
                            target = middleFingerTipTransform;
                            break;
                        case AttachmentPoint.UlnarBorder:
                            target = ulnarBorderTransform;
                            break;
                        case AttachmentPoint.PreviewSelect:
                            target = GetPreviewTarget();
                            break;
                    }

                    if (target != null)
                    {
                        sac.spellInstance = GameObject.Instantiate(sac.spellTemplatePrefab, target.position, target.rotation, null);
                        sac.spell = sac.spellInstance.GetComponent<Spell>();
                        if (sac.spell != null)
                            sac.spell.Begin(this, target);
                    }

            }
            else if (sac.spellInstance != null && sac.action.GetStateUp(behaviourPose.inputSource))
                {
                    sac.spell.Release(GetVelocity());

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

    public Transform GetPreviewTarget()
    {
        return spellPreview?.GetTarget();
    }

}