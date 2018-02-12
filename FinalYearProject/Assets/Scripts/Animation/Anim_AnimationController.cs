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
    [Range(0f, 0.2f)]
    private float ArmBend = 0.01f;
    [SerializeField]
    [Range(-1f, 1f)]
    private float LeanForward = 0f;
    [Header("Animation Targets")]
    [SerializeField]
    private Anim_Body Body;
    [SerializeField]
    private Anim_LegTarget LegLeft;
    [SerializeField]
    private Anim_LegTarget LegRight;
    [SerializeField]
    private Anim_ArmTarget ArmLeft;
    [SerializeField]
    private Anim_ArmTarget ArmRight;
    [SerializeField]
    private Transform SpineTarget;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        // Setting limb offsets.
        LegLeft.Offset = 0f;
        LegRight.Offset = Mathf.PI;
        ArmLeft.Offset = 0f;
        ArmRight.Offset = Mathf.PI;
    }

    private void Update()
    {
        // Updating walk speed.
        Body.Speed = WalkSpeed * 2f;
        LegLeft.Speed = WalkSpeed;
        LegRight.Speed = WalkSpeed;
        ArmLeft.Speed = WalkSpeed;
        ArmRight.Speed = WalkSpeed;

        // Updating stride.
        LegLeft.Amplitude = Stride;
        LegRight.Amplitude = Stride;

        // Updating step height.
        LegLeft.Height = StepHeight;
        LegRight.Height = StepHeight;

        // Updating the body bounce height.
        Body.Amplitude = BounceHeight;

        // Updating arm swing.
        ArmLeft.Amplitude = ArmSwing;
        ArmRight.Amplitude = ArmSwing;

        // Updating arm bend.
        ArmLeft.Height = ArmBend;
        ArmRight.Height = ArmBend;

        // Updating the lean.
        var spineTargetPos = SpineTarget.position;
        spineTargetPos.x = transform.position.x - LeanForward;
        SpineTarget.position = spineTargetPos;
    }
}
