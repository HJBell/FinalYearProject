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
    private float LeanForward = 0f;

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
    private Transform SpineTarget;
    [SerializeField]
    private bool DrawAll = false;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        // Setting limb offsets.
        LegLeft.GlobalOffset = 0f;
        LegRight.GlobalOffset = 1f;
        ArmLeft.GlobalOffset = 0f;
        ArmRight.GlobalOffset = 1f;
    }

    private void Update()
    {
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
        var spineTargetPos = SpineTarget.position;
        spineTargetPos.x = transform.position.x - LeanForward;
        SpineTarget.position = spineTargetPos;
    }

    private void OnDrawGizmos()
    {
        // Updating draw all.
        LegLeft.DrawPath = DrawAll;
        LegRight.DrawPath = DrawAll;
        ArmLeft.DrawPath = DrawAll;
        ArmRight.DrawPath = DrawAll;
        Body.DrawPath = DrawAll;
    }
}
