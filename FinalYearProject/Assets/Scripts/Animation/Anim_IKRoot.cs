using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Anim_IKRoot : MonoBehaviour {

    [SerializeField]
    private List<Anim_IKBone> mBones = new List<Anim_IKBone>();

    private Vector2 pPos2D { get { return new Vector2(transform.position.x, transform.position.y); } }

    // TESTING
    public Transform TargetTrans;
    Vector3 mTarget;


    //-----------------------------------Unity Functions-----------------------------------

    private void Update()
    {
        mTarget = (TargetTrans == null) ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : TargetTrans.position;
        var targetPos = mTarget;

        // Forward kinematics.
        for (int i = mBones.Count - 1; i >= 0; i--)
        {
            mBones[i].PositionEndNode(targetPos);
            targetPos = mBones[i].pStartNode;
        }

        // Inverse kinematics.
        for (int i = 0; i < mBones.Count; i++)
        {
            var parentEndPos = (i == 0) ? pPos2D : mBones[i - 1].pEndNode;
            mBones[i].PositionStartNode(parentEndPos);

            // Applying joint constraints.
            if (i <= 0) continue;

            var childBoneAngle = mBones[i].pAngle;
            var parentBoneAngle = mBones[i-1].pAngle;
            var childBoneMinAngle = parentBoneAngle + mBones[i - 1].AngleConstraintMin;
            var childBoneMaxAngle = parentBoneAngle + mBones[i - 1].AngleConstraintMax;

            var minDiff = Mathf.DeltaAngle(childBoneAngle, childBoneMinAngle);
            var maxDiff = Mathf.DeltaAngle(childBoneAngle, childBoneMaxAngle);

            if (minDiff < 0f) minDiff = Mathf.Infinity;
            if (maxDiff > 0f) maxDiff = Mathf.Infinity;

            var finalRot = Mathf.Abs(minDiff) < Mathf.Abs(maxDiff) ? minDiff : maxDiff;
            if (finalRot < 360f) mBones[i].RotateAroundStartNode(finalRot);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        //Gizmos.DrawSphere(mTarget, 0.25f);
    }
}
