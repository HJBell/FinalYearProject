using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Body : MonoBehaviour {

    [SerializeField]
    private float Speed = 1f;
    [SerializeField]
    [Range(0f, Mathf.PI)]
    private float Offset = 0f;
    [SerializeField]
    private float Amplitude = 0.1f;

    private Vector3 mStartPos;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        mStartPos = transform.position;
    }

    private void Update()
    {
        var yPos = mStartPos.y + Mathf.Sin((Time.time * Speed) + Offset) * Amplitude;
        transform.position = new Vector3(mStartPos.x, yPos, 0f);
    }
}
