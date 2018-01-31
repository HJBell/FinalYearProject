﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_ArmTarget : MonoBehaviour {

    [SerializeField]
    private float Speed = 1f;
    [SerializeField]
    [Range(0f, Mathf.PI)]
    private float Offset = 0f;
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
        var yPos = mStartPos.y + (Mathf.Sin((Time.time * 2f * Speed) - Mathf.PI / 2f) - 1f) * 0.5f * Height;
        transform.position = new Vector3(xPos, yPos, 0f);
    }
}