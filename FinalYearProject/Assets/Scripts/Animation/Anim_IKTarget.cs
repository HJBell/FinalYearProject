using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_IKTarget : MonoBehaviour {

    [Header("Global Settings")]
    public float GlobalSpeed = 1f;
    [Range(0f, 1f)]
    public float GlobalOffset = 0f;
    public bool DrawPath = false;

    [Header("X Settings")]
    public float XSpeed = 1f;
    [Range(0f, 1f)]
    public float XOffset = 0f;
    [Range(-1f, 1f)]
    public float XMax = 1f;
    [Range(-1f, 1f)]
    public float XMin = -1f;
    public float XAmplitude = 1f;

    [Header("Y Settings")]
    public float YSpeed = 1f;
    [Range(0f, 1f)]
    public float YOffset = 0.5f;
    [Range(-1f, 1f)]
    public float YMax = 1f;
    [Range(-1f, 1f)]
    public float YMin = -1f;
    public float YAmplitude = 1f;

    private float mAnimationPhase = 0f;
    private Vector3 mStartPos = Vector3.zero;
    private Vector3 pStartPos { get { return Application.isPlaying ? mStartPos : transform.position; } }


    //-----------------------------------Unity Functions-----------------------------------

    private void Awake()
    {
        mStartPos = transform.position;
    }

    private void Update()
    {
        mAnimationPhase = Mathf.Repeat(mAnimationPhase + Time.deltaTime * GlobalSpeed, Mathf.PI * 2f);
        transform.position = GetPointOnPath(mAnimationPhase);        
    }

    private void OnDrawGizmos()
    {
        if (!DrawPath) return;

        var startPoint = GetPointOnPath(0f);
        var previousPoint = startPoint;

        int pathResolution = 50;
        for(int i = 1; i < pathResolution; i++)
        {
            var phase = ((float)i / (float)pathResolution) * Mathf.PI * 2f;
            var currentPoint = GetPointOnPath(phase);
            Gizmos.DrawLine(previousPoint, (i == pathResolution-1) ? startPoint : currentPoint);
            previousPoint = currentPoint;
        }
    }


    //-----------------------------------Private Functions----------------------------------

    private Vector3 GetPointOnPath(float phase)
    {
        var xPos = pStartPos.x + Mathf.Clamp(Mathf.Sin(phase * XSpeed + GlobalOffset * Mathf.PI + XOffset * Mathf.PI), XMin, XMax) * XAmplitude;
        var yPos = pStartPos.y + Mathf.Clamp(Mathf.Sin(phase * YSpeed + GlobalOffset * Mathf.PI + YOffset * Mathf.PI), YMin, YMax) * YAmplitude;

        return new Vector3(xPos, yPos, 0f);
    }
}
