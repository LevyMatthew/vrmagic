// Copyright (c) Valve Corporation, All rights reserved. ======================================================================================================



using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    //-----------------------------------------------------------------------------
    public class PlantFeet : MonoBehaviour
    {
        public bool showPlantAnimation = true;

        public SteamVR_Action_Boolean plantFeetAction = SteamVR_Input.GetBooleanAction("/actions/motion/in/PlantFeet");

        private bool canPlant = true;

        public float canToggleEverySeconds = 0.4f;


        private void Start()
        {
        }


        private void Update()
        {
            Player player = Player.instance;

            bool leftHandPlant = plantFeetAction.GetStateDown(SteamVR_Input_Sources.LeftHand); //&& leftHandValid;
            bool rightHandPlant = plantFeetAction.GetStateDown(SteamVR_Input_Sources.RightHand); // && rightHandValid;
            bool anyHandPlant = plantFeetAction.GetStateDown(SteamVR_Input_Sources.Any);

            if (canPlant && anyHandPlant)
            {
                CharacterBody playerBody = player.GetComponent<CharacterBody>();
                playerBody.stickyFeet = !playerBody.stickyFeet;
                Debug.Log(playerBody.stickyFeet);
            }


            /*
            if (canPlant && plantFeetAction != null)
            {
                if (plantFeetAction.activeBinding)
                {
                    //only allow snap turning after a quarter second after the last teleport
                    if (Time.time < (lastActiveTime + canToggleEverySeconds))
                        return;

                    // only allow snap turning when not holding something

                    bool rightHandValid = player.rightHand.currentAttachedObject == null ||
                        (player.rightHand.currentAttachedObject != null
                        && player.rightHand.currentAttachedTeleportManager != null
                        && player.rightHand.currentAttachedTeleportManager.teleportAllowed);

                    bool leftHandValid = player.leftHand.currentAttachedObject == null ||
                        (player.leftHand.currentAttachedObject != null
                        && player.leftHand.currentAttachedTeleportManager != null
                        && player.leftHand.currentAttachedTeleportManager.teleportAllowed);


                    bool lefHandPlant = plantFeetAction.GetStateDown(SteamVR_Input_Sources.LeftHand); //&& leftHandValid;

                    bool rightHandPlant = plantFeetAction.GetStateDown(SteamVR_Input_Sources.RightHand); // && rightHandValid;

                    if (lefHandPlant || rightHandPlant)
                    {
                        PlantFeetToggle();
                    }
                }
                else
                {
                    Debug.Log("Action not Bound");
                }
            }            */
        }
    }
}