using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Anim_AnimationController))]
public class Anim_AnimationControllerEditor : Editor {

    public override void OnInspectorGUI()
    {
        var script = (Anim_AnimationController)target;
        DrawDefaultInspector();

        if (GUI.changed ||
           (Event.current.type == EventType.ExecuteCommand &&
           Event.current.commandName == "UndoRedoPerformed"))
        {
            script.UpdateDrawAllPaths();
            script.UpdateTargetsProperties();
            script.UpdateIKRoots();

            EditorUtility.SetDirty(script);
        }
    }
}
