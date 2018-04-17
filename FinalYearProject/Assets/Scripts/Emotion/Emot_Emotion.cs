using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Emot_Emotion {
    
    public string Name;
    [Range(0f, 1f)]
    public float PersonalityValue;
    [Range(0f, 1f)]
    public float MoodValue;    
    public Dictionary<string, float> AnimMappings = new Dictionary<string, float>();

    //-----------------------------------Public Functions----------------------------------

    public Emot_Emotion(string name)
    {
        Name = name;
        PersonalityValue = 0.0f;
        MoodValue = 0.0f;
    }

    public void UpdateValues()
    {
        MoodValue = Mathf.Lerp(MoodValue, PersonalityValue, Time.deltaTime);
    }

    public void SetPersonalityValue(float value)
    {
        PersonalityValue = Mathf.Clamp(value, -1.0f, 1.0f);
        MoodValue = PersonalityValue;
    }
}
