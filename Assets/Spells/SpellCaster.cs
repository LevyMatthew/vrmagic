//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Valve.VR.Extras
{
    public class SpellCaster : MonoBehaviour
    {
        [System.Serializable]
        public class SpellActionConfig
        {
            public SteamVR_Action_Boolean action;
            public GameObject spellTemplatePrefab;
            [HideInInspector] public GameObject spellInstance;
            [HideInInspector] public Spell spell;
        }

        [SerializeField] public List<SpellActionConfig> actionConfigs;

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
            foreach (SpellActionConfig sac in actionConfigs)
            {
                if (sac.spellInstance == null && sac.action.GetStateDown(behaviourPose.inputSource))
                {
                    sac.spellInstance = GameObject.Instantiate(sac.spellTemplatePrefab, transform.position, transform.rotation, null);
                    sac.spell = sac.spellInstance.GetComponent<Spell>();
                    sac.spell.Grab(transform);
                }
                else if (sac.spellInstance != null && sac.action.GetStateUp(behaviourPose.inputSource))
                {
                    sac.spell.Release(GetVelocity());
                    sac.spellInstance = null;
                }
            }
        }
    }
}