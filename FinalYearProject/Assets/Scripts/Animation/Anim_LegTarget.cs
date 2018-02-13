using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_LegTarget : MonoBehaviour {

    [HideInInspector]
    public float Speed = 1f;
    [HideInInspector]
    public float Offset = 0f;
    [HideInInspector]
    public float Amplitude = 1f;
    [HideInInspector]
    public float Height = 0.5f;

    private Vector3 mStartPos;
    private float mAnimationPhase = 0f;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        mStartPos = transform.position;
    }

    private void Update()
    {
        mAnimationPhase = Mathf.Repeat(mAnimationPhase + Time.deltaTime * Speed, Mathf.PI * 2f);

        var xPos = mStartPos.x + Mathf.Sin(mAnimationPhase + Offset) * Amplitude;
        var yPos = mStartPos.y + Mathf.Clamp(Mathf.Sin(mAnimationPhase - Mathf.PI/2f + Offset) * Height, 0f, Height);
        transform.position = new Vector3(xPos, yPos, 0f);
    }
}
