using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_AnimationController : MonoBehaviour {

    public float pWalkSpeed { get { return WalkSpeed; } set { WalkSpeed = Mathf.Clamp(value, 0f, 4f); UpdateTargetsProperties(); } }
    public float pStride { get { return Stride; } set { Stride = Mathf.Clamp(value, 0.25f, 1f); UpdateTargetsProperties(); } }
    public float pStepHeight { get { return StepHeight; } set { StepHeight = Mathf.Clamp(value, 0f, 1f); UpdateTargetsProperties(); } }
    public float pBounceHeight { get { return BounceHeight; } set { BounceHeight = Mathf.Clamp(value, 0f, 1f); UpdateTargetsProperties(); } }
    public float pArmSwing { get { return ArmSwing; } set { ArmSwing = Mathf.Clamp(value, 0f, 1f); UpdateTargetsProperties(); } }
    public float pArmBend { get { return ArmBend; } set { ArmBend = Mathf.Clamp(value, 0f, 1f); UpdateTargetsProperties(); } }
    public float pLean { get { return Lean; } set { Lean = Mathf.Clamp(value, 0f, 0.99f); UpdateTargetsProperties(); } }
    public float pHunch { get { return Hunch; } set { Hunch = Mathf.Clamp(value, 0f, 1f); UpdateTargetsProperties(); } }
    public float pHeadTilt { get { return HeadTilt; } set { HeadTilt = Mathf.Clamp(value, 0f, 0.99f); UpdateTargetsProperties(); } }

    [Header("Character Settings")]
    [SerializeField]
    [Range(0f, 4f)]
    private float WalkSpeed = 0.75f;
    [SerializeField]
    [Range(0.25f, 1f)]
    private float Stride = 0.5f;
    [SerializeField]
    [Range(0f, 1f)]
    private float StepHeight = 0.1f;
    [SerializeField]
    [Range(0f, 1f)]
    private float BounceHeight = 0.1f;
    [SerializeField]
    [Range(0f, 1f)]
    private float ArmSwing = 0.5f;
    [SerializeField]
    [Range(0f, 1f)]
    private float ArmBend = 0.5f;
    [SerializeField]
    [Range(0f, 0.99f)]
    private float Lean = 0f;
    [SerializeField]
    [Range(0f, 1f)]
    private float Hunch = 0f;
    [SerializeField]
    [Range(0f, 0.99f)]
    private float HeadTilt = 0f;
    [SerializeField]
    [Range(0f, 1f)]
    private float StartPhase = 0f;

    [Header("Animation Targets")]
    [SerializeField]
    private Anim_IKTarget Body;
    [SerializeField]
    private Anim_IKTarget LegLeft;
    [SerializeField]
    private Anim_IKTarget LegRight;
    [SerializeField]
    private Anim_IKTarget ArmLeft;
    [SerializeField]
    private Anim_IKTarget ArmRight;
    [SerializeField]
    private Anim_IKTarget Spine;
    [SerializeField]
    private Anim_IKTarget Head;
    [SerializeField]
    private bool DrawAllPaths = false;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        UpdateStartPhases();
        UpdateTargetsProperties();   
    }


    //-----------------------------------Public Functions----------------------------------

    public void UpdateTargetsProperties()
    {
        // Setting global offsets.
        if (!Application.isPlaying)
            UpdateStartPhases();       

        // Updating walk speed.
        Body.GlobalSpeed = WalkSpeed * 2f;
        LegLeft.GlobalSpeed = WalkSpeed;
        LegRight.GlobalSpeed = WalkSpeed;
        ArmLeft.GlobalSpeed = WalkSpeed;
        ArmRight.GlobalSpeed = WalkSpeed;

        // Updating stride.
        LegLeft.XAmplitude = Stride;
        LegRight.XAmplitude = Stride;

        // Updating step height.
        LegLeft.YAmplitude = StepHeight;
        LegRight.YAmplitude = StepHeight;

        // Updating the body bounce height.
        Body.YAmplitude = BounceHeight;

        // Updating arm swing.
        ArmLeft.XAmplitude = ArmSwing;
        ArmRight.XAmplitude = ArmSwing;

        // Updating arm bend.
        ArmLeft.YAmplitude = ArmBend;
        ArmRight.YAmplitude = ArmBend;

        // Updating the spine.
        Spine.pPhase = Lean;
        Spine.XAmplitude = 0.85f * (0.6f + (1f - Hunch) * 0.4f);
        Spine.YAmplitude = 0.85f * (0.8f + (1f - Hunch) * 0.2f);

        // Updating the head.
        Head.pPhase = HeadTilt;
    }

    public void UpdateDrawAllPaths()
    {
        LegLeft.DrawPath = DrawAllPaths;
        LegRight.DrawPath = DrawAllPaths;
        ArmLeft.DrawPath = DrawAllPaths;
        ArmRight.DrawPath = DrawAllPaths;
        Body.DrawPath = DrawAllPaths;
        Spine.DrawPath = DrawAllPaths;
        Head.DrawPath = DrawAllPaths;
    }

    public void UpdateIKRoots()
    {
        foreach (var root in GetComponentsInChildren<Anim_IKRoot>())
            root.UpdateRoot(10);
    }


    //----------------------------------Private Functions----------------------------------

    private void UpdateStartPhases()
    {
        Body.pPhase = StartPhase;
        LegLeft.pPhase = StartPhase + 0.5f;
        LegRight.pPhase = StartPhase;
        ArmLeft.pPhase = StartPhase;
        ArmRight.pPhase = StartPhase + 0.5f;
    }
}
