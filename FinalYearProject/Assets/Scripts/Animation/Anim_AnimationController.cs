using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_AnimationController : MonoBehaviour {

    [SerializeField]
    [Range(0f, 20f)]
    private float WalkSpeed = 1f;
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


    //-----------------------------------Unity Functions-----------------------------------

    private void Update()
    {
        Body.Speed = WalkSpeed * 2f;

        LegLeft.Speed = WalkSpeed;
        LegLeft.Offset = 0f;
        LegRight.Speed = WalkSpeed;
        LegRight.Offset = Mathf.PI;

        ArmLeft.Speed = WalkSpeed;
        ArmLeft.Offset = 0f;
        ArmRight.Speed = WalkSpeed;
        ArmRight.Offset = Mathf.PI;
    }
}
