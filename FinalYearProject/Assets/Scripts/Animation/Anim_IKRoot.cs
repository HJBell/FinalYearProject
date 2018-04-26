using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Anim_IKRoot : MonoBehaviour {

    private enum Mode { Smoothness, Accuracy }

    [SerializeField]
    private Anim_IKTarget LimbTarget;
    [SerializeField]
    private Anim_IKTarget RootTarget;
    [SerializeField]
    private Mode IKMode = Mode.Smoothness;
    [SerializeField]
    private List<Anim_IKBone> mBones = new List<Anim_IKBone>();

    private Vector2 pPos2D { get { return new Vector2(transform.position.x, transform.position.y); } }



    //-----------------------------------Unity Functions-----------------------------------

    private void LateUpdate()
    {
        UpdateRoot();
    }


    //-----------------------------------Public Functions----------------------------------

    public void UpdateRoot(int iterations = 1)
    {
        if (RootTarget != null) transform.position = RootTarget.pPosition;

        if (LimbTarget == null) return;

        for (int j = 0; j < iterations; j++)
        {
            var target = LimbTarget.pPosition;
            var targetTemp = target;


            // FUDGE FACTOR...NEEDS REFACTORING AND COMMENTING
            if (IKMode == Mode.Accuracy)
            {
                float totalLength = 0f;
                foreach (var bone in mBones)
                    totalLength += bone.Length;

                var fudgeTarget = Vector3.zero;
                if (Vector3.Distance(transform.position, targetTemp) < totalLength)
                {
                    var averageAngleLimit = Vector2.zero;
                    foreach (var bone in mBones)
                        averageAngleLimit += ((bone.pMinAngleVector + bone.pMaxAngleVector) / 2f).normalized;
                    averageAngleLimit /= mBones.Count;
                    averageAngleLimit.Normalize();

                    var multiplier = totalLength - Vector3.Distance(transform.position, targetTemp);

                    fudgeTarget = target - new Vector3(averageAngleLimit.x, averageAngleLimit.y, 0f) * multiplier * 8f;
                }
                else
                {
                    fudgeTarget = targetTemp;
                }

                for (int i = 0; i < mBones.Count; i++)
                {
                    mBones[i].PositionEndNode(fudgeTarget);
                    var parentEndPos = (i == 0) ? pPos2D : mBones[i - 1].pEndNode;
                    mBones[i].PositionStartNode(parentEndPos);
                }
            }



            // Forward kinematics.
            for (int i = mBones.Count - 1; i >= 0; i--)
            {
                mBones[i].PositionEndNode(targetTemp);
                targetTemp = mBones[i].pStartNode;
            }

            // Inverse kinematics.
            for (int i = 0; i < mBones.Count; i++)
            {
                var parentEndPos = (i == 0) ? pPos2D : mBones[i - 1].pEndNode;
                mBones[i].PositionStartNode(parentEndPos);

                //Applying joint constraints.
                if (i <= 0) continue;

                var childBoneAngle = mBones[i].pAngle;
                var parentBoneAngle = mBones[i - 1].pAngle;
                var childBoneMinAngle = parentBoneAngle + mBones[i - 1].MinAngle;
                var childBoneMaxAngle = parentBoneAngle + mBones[i - 1].MaxAngle;

                var minDiff = Mathf.DeltaAngle(childBoneAngle, childBoneMinAngle);
                var maxDiff = Mathf.DeltaAngle(childBoneAngle, childBoneMaxAngle);

                if (minDiff < 0f) minDiff = Mathf.Infinity;
                if (maxDiff > 0f) maxDiff = Mathf.Infinity;

                var finalRot = Mathf.Abs(minDiff) < Mathf.Abs(maxDiff) ? minDiff : maxDiff;
                if (finalRot < 360f) mBones[i].RotateAroundStartNode(finalRot);
            }
        }
    }
}
