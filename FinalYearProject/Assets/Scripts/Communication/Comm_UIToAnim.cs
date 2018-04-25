using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comm_UIToAnim : MonoBehaviour {

    [SerializeField]
    private Anim_AnimationController AnimController;
    [SerializeField]
    private Slider WalkSpeedSlider;
    [SerializeField]
    private Slider StrideSlider;
    [SerializeField]
    private Slider StepHeightSlider;
    [SerializeField]
    private Slider BounceHeightSlider;
    [SerializeField]
    private Slider ArmSwingSlider;
    [SerializeField]
    private Slider ArmBendSlider;
    [SerializeField]
    private Slider LeanSlider;
    [SerializeField]
    private Slider HunchSlider;
    [SerializeField]
    private Slider HeadTiltSlider;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        WalkSpeedSlider.value = AnimController.pWalkSpeed;
        StrideSlider.value = AnimController.pStride;
        StepHeightSlider.value = AnimController.pStepHeight;
        BounceHeightSlider.value = AnimController.pBounceHeight;
        ArmSwingSlider.value = AnimController.pArmSwing;
        ArmBendSlider.value = AnimController.pArmBend;
        LeanSlider.value = AnimController.pLean;
        HunchSlider.value = AnimController.pHunch;
        HeadTiltSlider.value = AnimController.pHeadTilt;
    }

    private void Update()
    {
        AnimController.pWalkSpeed = WalkSpeedSlider.value;
        AnimController.pStride = StrideSlider.value;
        AnimController.pStepHeight = StepHeightSlider.value;
        AnimController.pBounceHeight = BounceHeightSlider.value;
        AnimController.pArmSwing = ArmSwingSlider.value;
        AnimController.pArmBend = ArmBendSlider.value;
        AnimController.pLean = LeanSlider.value;
        AnimController.pHunch = HunchSlider.value;
        AnimController.pHeadTilt = HeadTiltSlider.value;
    }
}
