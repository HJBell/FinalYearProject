using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_IKTarget : MonoBehaviour {

    public Vector3 pPosition { get { return GetPointOnPath(mXPhase, mYPhase); } }

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

    private float mXPhase = 0f;
    private float mYPhase = 0f;


    //-----------------------------------Unity Functions-----------------------------------

    private void Update()
    {
        mXPhase = Mathf.Repeat(mXPhase + Time.deltaTime * GlobalSpeed * XSpeed, Mathf.PI * 2f);
        mYPhase = Mathf.Repeat(mYPhase + Time.deltaTime * GlobalSpeed * YSpeed, Mathf.PI * 2f);
    }

    private void OnDrawGizmos()
    {
        if (!DrawPath) return;

        var startPoint = GetPointOnPath(0f, 0f);
        var previousPoint = startPoint;

        int pathResolution = 50;
        for(int i = 1; i < pathResolution; i++)
        {
            var phase = ((float)i / (float)pathResolution) * Mathf.PI * 2f;
            var currentPoint = GetPointOnPath(phase, phase);
            Gizmos.DrawLine(previousPoint, (i == pathResolution-1) ? startPoint : currentPoint);
            previousPoint = currentPoint;
        }

        Gizmos.DrawWireSphere(pPosition, 0.05f);
    }


    //-----------------------------------Private Functions----------------------------------

    private Vector3 GetPointOnPath(float xPhase, float yPhase)
    {
        var xPos = transform.position.x + Mathf.Clamp(Mathf.Sin(xPhase + GlobalOffset * 2f * Mathf.PI + XOffset * 2f * Mathf.PI), XMin, XMax) * XAmplitude;
        var yPos = transform.position.y + Mathf.Clamp(Mathf.Sin(yPhase + GlobalOffset * 2f * Mathf.PI + YOffset * 2f * Mathf.PI), YMin, YMax) * YAmplitude;

        return new Vector3(xPos, yPos, 0f);
    }
}
