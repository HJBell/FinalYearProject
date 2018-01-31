using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_IKBone : MonoBehaviour {

    [Range(-360f, 360f)]
    public float AngleConstraintMin = -90f;
    [Range(-360f, 360f)]
    public float AngleConstraintMax = 90f;

    public Vector2 pStartNode { get { return this.transform.position; } }
    public Vector2 pBoneVector { get { return this.transform.up * Length; } }
    public Vector2 pEndNode { get { return pStartNode + pBoneVector; } }
    public float pAngle { get { return this.transform.eulerAngles.z; } }

    [SerializeField]
    private float Length = 1f;    


    //-----------------------------------Unity Functions-----------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pStartNode, pEndNode);

        var angConstVectorMin = Quaternion.AngleAxis(AngleConstraintMin, Vector3.forward) * pBoneVector;
        var angConstVectorMax = Quaternion.AngleAxis(AngleConstraintMax, Vector3.forward) * pBoneVector;

        //Gizmos.color = Color.blue;
        //Gizmos.DrawLine(pEndNode, pEndNode + new Vector2(angConstVectorMin.x, angConstVectorMin.y).normalized * 0.2f);
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawLine(pEndNode, pEndNode + new Vector2(angConstVectorMax.x, angConstVectorMax.y).normalized * 0.2f);
    }


    //-----------------------------------Public Functions----------------------------------

    public void PositionStartNode(Vector2 pos)
    {
        var currentEndNode = pEndNode;
        this.transform.position = pos;
        LookAt(currentEndNode);
    }

    public void PositionEndNode(Vector2 pos)
    {
        LookAt(pos);
        transform.Translate(Vector3.up * ((pos - pStartNode).magnitude - Length));
    }

    public void RotateAroundStartNode(float angle)
    {
        transform.Rotate(0f, 0f, angle);
    }


    //-----------------------------------Private Functions----------------------------------

    private void LookAt(Vector2 pos)
    {
        Vector3 diff3D = (new Vector3(pos.x, pos.y, 0f) - transform.position).normalized;
        float zRot = Mathf.Atan2(diff3D.y, diff3D.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90f);
    }
}
