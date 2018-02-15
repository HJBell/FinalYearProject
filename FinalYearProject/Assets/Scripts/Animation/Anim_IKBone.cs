using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_IKBone : MonoBehaviour {

    public float Length = 1f;    
    [Range(-360f, 360f)]
    public float MinAngle = -90f;
    [Range(-360f, 360f)]
    public float MaxAngle = 90f;

    public Vector2 pBoneVector { get { return this.transform.up * Length; } }
    public Vector2 pStartNode { get { return this.transform.position; } }
    public Vector2 pEndNode { get { return pStartNode + pBoneVector; } }
    public float pAngle { get { return this.transform.eulerAngles.z; } }
    public Vector2 pMinAngleVector { get { return (Quaternion.AngleAxis(MinAngle, Vector3.forward) * pBoneVector).normalized; } }
    public Vector2 pMaxAngleVector { get { return (Quaternion.AngleAxis(MaxAngle, Vector3.forward) * pBoneVector).normalized; } }

    [SerializeField]
    [Range(0f, 1f)]
    private float GraphicWidth = 0.1f;
    [SerializeField]
    private Color GraphicColour = Color.white;
    [SerializeField]
    [Range(0f, 1f)]
    private float GraphicOverextension = 0f;
    [SerializeField]
    private int GraphicOrderIndex = 1;

    [HideInInspector, SerializeField]
    private Transform mGraphicTransform;


    //-----------------------------------Unity Functions-----------------------------------

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(pStartNode, pEndNode);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pEndNode, pEndNode + pMinAngleVector * 0.2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(pEndNode, pEndNode + pMaxAngleVector * 0.2f);

        //var averageAngleLimit = ((pMinAngleVector + pMaxAngleVector) / 2f).normalized;
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(pEndNode, pEndNode - averageAngleLimit * 0.2f);
    }


    //-----------------------------------Public Functions----------------------------------

    public void PositionStartNode(Vector2 pos)
    {
        var currentEndNode = pEndNode;
        this.transform.position = pos;
        LookAt(currentEndNode);
        UpdateGraphic();
    }

    public void PositionEndNode(Vector2 pos)
    {
        LookAt(pos);
        transform.Translate(Vector3.up * ((pos - pStartNode).magnitude - Length));
        UpdateGraphic();
    }

    public void RotateAroundStartNode(float angle)
    {
        transform.Rotate(0f, 0f, angle);
        UpdateGraphic();
    }

    public void UpdateGraphic()
    {
        if (mGraphicTransform == null)
        {
            mGraphicTransform = (Instantiate(Resources.Load("Res_LimbGraphic")) as GameObject).transform;
            mGraphicTransform.SetParent(transform);

            //var sprites = GetComponentsInChildren<SpriteRenderer>();

            //if(sprites.Length > 0)
            //{
            //    for (int i = 0; i < sprites.Length - 1; i++)
            //        DestroyImmediate(sprites[i].gameObject);
            //    mGraphicTransform = sprites[0].transform;
            //}
            //else
            //{

            //}            
        }
        mGraphicTransform.GetComponent<SpriteRenderer>().color = GraphicColour;
        mGraphicTransform.GetComponent<SpriteRenderer>().sortingOrder = GraphicOrderIndex;
        mGraphicTransform.position = pStartNode + pBoneVector * 0.5f;
        mGraphicTransform.rotation = Quaternion.Euler(0f, 0f, pAngle);
        mGraphicTransform.localScale = new Vector3(GraphicWidth, Length + GraphicOverextension, 1f);
    }


    //-----------------------------------Private Functions----------------------------------

    private void LookAt(Vector2 pos)
    {
        Vector3 diff3D = (new Vector3(pos.x, pos.y, 0f) - transform.position).normalized;
        float zRot = Mathf.Atan2(diff3D.y, diff3D.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, zRot - 90f);
    }
}
