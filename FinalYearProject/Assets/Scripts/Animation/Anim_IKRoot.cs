using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Anim_IKRoot : MonoBehaviour {

    [SerializeField]
    private List<Anim_IKBone> mBones = new List<Anim_IKBone>();

    private Vector2 pPos2D { get { return new Vector2(transform.position.x, transform.position.y); } }

    // TESTING
    Vector3 mousePos;


    //-----------------------------------Unity Functions-----------------------------------

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var targetPos = mousePos;

        for (int i = mBones.Count - 1; i >= 0; i--)
        {
            mBones[i].PositionEndNode(targetPos);
            targetPos = mBones[i].pStartNode;
        }

        for (int i = 0; i < mBones.Count; i++)
        {
            var parentEndPos = (i == 0) ? pPos2D : mBones[i - 1].pEndNode;
            mBones[i].PositionStartNode(parentEndPos);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(mousePos, 0.25f);
    }
}
