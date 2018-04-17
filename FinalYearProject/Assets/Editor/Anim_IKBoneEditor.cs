using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Anim_IKBone)), CanEditMultipleObjects]
public class Anim_IKBoneEditor : Editor {

    public override void OnInspectorGUI()
    {
        var script = (Anim_IKBone)target;
        DrawDefaultInspector();

        if (GUI.changed ||
           (Event.current.type == EventType.ExecuteCommand &&
           Event.current.commandName == "UndoRedoPerformed"))
        {
            foreach (var obj in Selection.gameObjects)
                obj.GetComponent<Anim_IKBone>().UpdateGraphic();

            EditorUtility.SetDirty(script);
        }
    }
}
