using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KinematicTracker))]
public class SpellCaster : MonoBehaviour
{
    public Rigidbody recoilRecipient;
    public GameObject spellPreviewPrefab;
    private SpellPreview spellPreview;
    private KinematicTracker kinematicTracker;


    public class SpellSpawner
    {
        public GameObject spellTemplatePrefab;
        [HideInInspector] public GameObject spellInstance;
        [HideInInspector] public Spell spell;

    }

    [System.Serializable]
    public class BooleanActionSpellSpawner : SpellSpawner
    {
        public BooleanInputAction action;
    }

    [System.Serializable]
    public class VelocityActionSpellSpawner : SpellSpawner
    {
        public VelocityInputAction spawnAction;
        public VelocityInputAction dropAction;
        public bool dropOnSpawn = false;
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

    public enum VelocityInputAction
    {
        Punch,
        Pull,
        PunchDown,
    }

    [SerializeField] public Transform palmTransform;
    [SerializeField] public Transform indexFingerTipTransform;
    [SerializeField] public Transform middleFingerTipTransform;
    [SerializeField] public Transform ulnarBorderTransform;

    [SerializeField] public List<BooleanActionSpellSpawner> buttonSpellConfigs;
    [SerializeField] public List<VelocityActionSpellSpawner> velocitySpellConfigs;

    private void Awake()
    {
        kinematicTracker = GetComponent<KinematicTracker>();
        kinematicTracker.punchEvent.AddListener(Punch);
        kinematicTracker.pullEvent.AddListener(Pull);

        if (spellPreviewPrefab != null)
        {
            GameObject spellPreviewInstance = Instantiate(spellPreviewPrefab, transform);
            spellPreview = spellPreviewInstance.GetComponent<SpellPreview>();
        }
        foreach (BooleanActionSpellSpawner sac in buttonSpellConfigs)
        {
            if (sac.spellTemplatePrefab == null)
                throw new MissingReferenceException("Spell configuration missing reference to spell template");
        }

    }

    private Vector3 GetVelocity()
    {
        return kinematicTracker.velocity;
    }

    public void RegisterVelocityEvent(VelocityInputAction velocityInputAction)
    {
        Vector3 velocity = kinematicTracker.velocity;
        foreach (VelocityActionSpellSpawner vsac in velocitySpellConfigs)
        {
            bool justSpawned = false;
            if (vsac.spellInstance == null && vsac.spawnAction == velocityInputAction)
            {
                Transform target = transform;
                vsac.spellInstance = GameObject.Instantiate(vsac.spellTemplatePrefab, target.position, target.rotation, null);
                vsac.spell = vsac.spellInstance.GetComponent<Spell>();
                vsac.spell.Begin(this, target);
                justSpawned = true;
            }
            
            if ((!justSpawned || vsac.dropOnSpawn) && vsac.spellInstance != null && vsac.dropAction == velocityInputAction)
            {
                vsac.spell.Release(velocity);
                vsac.spellInstance = null;
                vsac.spell = null;
            }
        }

    }

    public void Punch()
    {
        Vector3 velocity = kinematicTracker.velocity;
        foreach (BooleanActionSpellSpawner sac in buttonSpellConfigs)
        {
            if (sac.spellInstance != null)
            {
                sac.spell.Punch(velocity);
            }
        }

        if (velocity.y < -kinematicTracker.punchVelocityThreshold * 0.5f)
        {
            RegisterVelocityEvent(VelocityInputAction.PunchDown);
        }
        else
        {
            RegisterVelocityEvent(VelocityInputAction.Punch);
        }


        foreach (VelocityActionSpellSpawner vsac in velocitySpellConfigs)
        {
            if (vsac.spellInstance != null)
            {
                vsac.spell.Punch(velocity);
            }
        }
    }

    public void Pull()
    {
        Vector3 velocity = kinematicTracker.velocity;
        foreach (BooleanActionSpellSpawner sac in buttonSpellConfigs)
        {
            if (sac.spellInstance != null)
            {
                sac.spell.Pull(velocity);
            }
        }

        RegisterVelocityEvent(VelocityInputAction.Pull);

        foreach (VelocityActionSpellSpawner vsac in velocitySpellConfigs)
        {
            if (vsac.spellInstance != null)
            {
                vsac.spell.Pull(velocity);
            }
        }
    }

    private void DoBooleanActionSpawns()
    {
        foreach (BooleanActionSpellSpawner sac in buttonSpellConfigs)
        {
            if (sac.action == null)
                continue;

            if (sac.spellInstance == null && sac.action.GetState())
            {
                Transform target = transform;

                if (target != null)
                {
                    sac.spellInstance = GameObject.Instantiate(sac.spellTemplatePrefab, target.position, target.rotation, null);
                    sac.spell = sac.spellInstance.GetComponent<Spell>();
                    if (sac.spell != null)
                        sac.spell.Begin(this, target);
                }

            }
            else if (sac.spellInstance != null && !sac.action.GetState())
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

    public Transform GetPreviewTarget()
    {
        return spellPreview?.GetTarget();
    }
}