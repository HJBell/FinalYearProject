using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Emot_EmotionController))]
[RequireComponent(typeof(Anim_AnimationController))]
public class Comm_EmotToAnim : MonoBehaviour {

    private Emot_EmotionController mEmotController;
    private Anim_AnimationController mAnimController;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        mEmotController = GetComponent<Emot_EmotionController>();
        mAnimController = GetComponent<Anim_AnimationController>();
    }

    private void Update()
    {
        mAnimController.pWalkSpeed = mEmotController.GetAnimPropertyValue("WalkSpeed");
        mAnimController.pStride = mEmotController.GetAnimPropertyValue("Stride");
        mAnimController.pStepHeight = mEmotController.GetAnimPropertyValue("StepHeight");
        mAnimController.pBounceHeight = mEmotController.GetAnimPropertyValue("BounceHeight");
        mAnimController.pArmSwing = mEmotController.GetAnimPropertyValue("ArmSwing");
        mAnimController.pArmBend = mEmotController.GetAnimPropertyValue("ArmBend");
        mAnimController.pLean = mEmotController.GetAnimPropertyValue("Lean");
        mAnimController.pHunch = mEmotController.GetAnimPropertyValue("Hunch");
        mAnimController.pHeadTilt = mEmotController.GetAnimPropertyValue("HeadTilt");
    }
}
