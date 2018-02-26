using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_IKTarget : MonoBehaviour {

    public Vector3 pPosition { get { return GetPointOnPath(pPhase); } }
    public float pPhase { set { Phase = Mathf.Repeat(value, 1f); } get { return Phase; } }

    [Header("Global Settings")]
    [SerializeField]
    [Range(0f, 1f)]
    private float Phase = 0f;
    public float GlobalSpeed = 1f;
    [Range(0f, 1f)]
    public float GlobalOffset = 0f;
    [SerializeField]
    private Transform PosParentTrans;
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

    private Vector3 mPosParentTransOffset;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        if (PosParentTrans != null)
            mPosParentTransOffset = transform.position - PosParentTrans.position;
    }

    private void Update()
    {
        pPhase = pPhase + Time.deltaTime * GlobalSpeed;
        if (PosParentTrans != null)
            transform.position = PosParentTrans.position + mPosParentTransOffset;
    }

    private void OnDrawGizmos()
    {
        if (!DrawPath) return;

        int pathResolution = 50;
        var previousPoint = GetPointOnPath(0f);
        for(int i = 1; i < pathResolution; i++)
        {
            var phase = ((float)i / ((float)pathResolution -1f));
            var currentPoint = GetPointOnPath(phase);
            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }

        Gizmos.DrawWireSphere(pPosition, 0.05f);
    }


    //----------------------------------Private Functions----------------------------------

    private Vector3 GetPointOnPath(float phase)
    {
        var xPos = transform.position.x + Mathf.Clamp(Mathf.Sin((phase * XFrequency + GlobalOffset * XFrequency + XOffset) * 2f * Mathf.PI), XMin, XMax) * XAmplitude;
        var yPos = transform.position.y + Mathf.Clamp(Mathf.Sin((phase * YFrequency + GlobalOffset * YFrequency + YOffset) * 2f * Mathf.PI), YMin, YMax) * YAmplitude;

        return new Vector3(xPos, yPos, 0f);
    }
}
