using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Emot_EmotionController))]
public class Emot_EmotionControllerEditor : Editor {

    private Emot_EmotionController script;

    public override void OnInspectorGUI()
    {
        script = (Emot_EmotionController)target;
        DrawDefaultInspector();

        DrawEmotionTypeInspector(script.Emotions);

        if (GUI.changed ||
           (Event.current.type == EventType.ExecuteCommand &&
           Event.current.commandName == "UndoRedoPerformed"))
        {
            EditorUtility.SetDirty(script);
        }
    }

    private void DrawEmotionTypeInspector(Dictionary<string, Emot_Emotion> emotionList)
    {
        if (emotionList.Count <= 0) return;

        foreach (var emotion in emotionList)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField(emotion.Value.Name, EditorStyles.boldLabel);
            emotion.Value.MoodValue = EditorGUILayout.Slider("Mood", emotion.Value.MoodValue, 0f, 1f);
            emotion.Value.PersonalityValue = EditorGUILayout.Slider("Personality", emotion.Value.PersonalityValue, 0f, 1f);
            if(EditorGUI.EndChangeCheck())
            {
                script.NormalisePersonality(emotion.Value.Name);
            }
        }
    }
}