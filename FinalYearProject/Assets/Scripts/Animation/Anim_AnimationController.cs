using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_AnimationController : MonoBehaviour {
    
    [Header("Character Settings")]
    [SerializeField]
    [Range(0f, 20f)]
    private float WalkSpeed = 4f;
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
    [Range(-1f, 1f)]
    private float Lean = 0f;
    [SerializeField]
    [Range(0f, 1f)]
    private float StartPhaseTime = 0f;

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
    private bool DrawAllPaths = false;



    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        UpdateTargetsProperties();   
    }


    //-----------------------------------Public Functions----------------------------------

    public void UpdateTargetsProperties()
    {
        // Setting global offsets.
        if (!Application.isPlaying)
        {
            Body.GlobalOffset = StartPhaseTime;
            LegLeft.GlobalOffset = StartPhaseTime;
            LegRight.GlobalOffset = StartPhaseTime + 0.5f;
            ArmLeft.GlobalOffset = StartPhaseTime;
            ArmRight.GlobalOffset = StartPhaseTime + 0.5f;
            Spine.GlobalOffset = StartPhaseTime;
        }        

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

        // Updating the lean.
        Spine.GlobalOffset = Lean * 0.25f;
    }

    public void UpdateDrawAllPaths()
    {
        LegLeft.DrawPath = DrawAllPaths;
        LegRight.DrawPath = DrawAllPaths;
        ArmLeft.DrawPath = DrawAllPaths;
        ArmRight.DrawPath = DrawAllPaths;
        Body.DrawPath = DrawAllPaths;
        Spine.DrawPath = DrawAllPaths;
    }

    public void UpdateIKRoots()
    {
        foreach (var root in GetComponentsInChildren<Anim_IKRoot>())
            root.UpdateRoot(10);
    }
}
