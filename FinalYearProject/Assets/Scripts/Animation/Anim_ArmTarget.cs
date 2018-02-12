using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_ArmTarget : MonoBehaviour {

    [HideInInspector]
    public float Speed = 1f;
    [HideInInspector]
    public float Offset = 0f;
    [HideInInspector]
    public float Amplitude = 1f;
    [HideInInspector]
    public float Height = 0.5f;

    private Vector3 mStartPos;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        mStartPos = transform.position;
    }

    private void Update()
    {
        var xPos = mStartPos.x + Mathf.Sin((Time.time * Speed) + Offset) * Amplitude;
        var yPos = mStartPos.y + (Mathf.Sin((Time.time * 2f * Speed) - Mathf.PI / 2f) + 1f) * Height;
        transform.position = new Vector3(xPos, yPos, 0f);
    }
}
