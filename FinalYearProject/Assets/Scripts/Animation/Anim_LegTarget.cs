using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_LegTarget : MonoBehaviour {

    [HideInInspector]
    public float Speed = 1f;
    [HideInInspector]
    public float Offset = 0f;

    [SerializeField]
    private float Amplitude = 1f;
    [SerializeField]
    private float Height = 0.5f;

    private Vector3 mStartPos;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        mStartPos = transform.position;
    }

    private void Update()
    {
        var xPos = mStartPos.x + Mathf.Sin((Time.time * Speed) + Offset) * Amplitude;
        var yPos = mStartPos.y + Mathf.Clamp(Mathf.Sin((Time.time * Speed) - Mathf.PI/2f + Offset) * Height, 0f, Height);
        transform.position = new Vector3(xPos, yPos, 0f);
    }
}
