using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_IKTarget : MonoBehaviour {

    public Vector3 pPosition { get { return GetPointOnPath(mPhase); } }

    [Header("Global Settings")]
    public float GlobalSpeed = 1f;
    [Range(0f, 1f)]
    public float GlobalOffset = 0f;
    public bool DrawPath = false;

    [Header("X Settings")]
    public float XFrequency = 1f;
    [Range(0f, 1f)]
    public float XOffset = 0f;
    [Range(-1f, 1f)]
    public float XMax = 1f;
    [Range(-1f, 1f)]
    public float XMin = -1f;
    public float XAmplitude = 1f;

    [Header("Y Settings")]
    public float YFrequency = 1f;
    [Range(0f, 1f)]
    public float YOffset = 0.5f;
    [Range(-1f, 1f)]
    public float YMax = 1f;
    [Range(-1f, 1f)]
    public float YMin = -1f;
    public float YAmplitude = 1f;

    private float mPhase = 0f;


    //-----------------------------------Unity Functions-----------------------------------

    private void Update()
    {
        mPhase = Mathf.Repeat(mPhase + Time.deltaTime * GlobalSpeed, Mathf.PI * 2f);
    }

    private void OnDrawGizmos()
    {
        if (!DrawPath) return;

        int pathResolution = 50;
        var previousPoint = GetPointOnPath(0f);
        for(int i = 1; i < pathResolution; i++)
        {
            var phase = ((float)i / (float)pathResolution) * Mathf.PI * 2f;
            var currentPoint = GetPointOnPath(phase);
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }

        Gizmos.DrawWireSphere(pPosition, 0.05f);
    }


    //-----------------------------------Private Functions----------------------------------

    private Vector3 GetPointOnPath(float phase)
    {
        var xPos = transform.position.x + Mathf.Clamp(Mathf.Sin(phase * XFrequency + GlobalOffset * XFrequency * 2f * Mathf.PI + XOffset * 2f * Mathf.PI), XMin, XMax) * XAmplitude;
        var yPos = transform.position.y + Mathf.Clamp(Mathf.Sin(phase * YFrequency + GlobalOffset * YFrequency * 2f * Mathf.PI + YOffset * 2f * Mathf.PI), YMin, YMax) * YAmplitude;

        return new Vector3(xPos, yPos, 0f);
    }
}
