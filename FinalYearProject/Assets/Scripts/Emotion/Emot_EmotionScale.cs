using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Emot_EmotionScale {

    public string NegativeName;
    public string PositiveName;
    [Range(-1.0f, 1.0f)]
    public float PersonalityValue;
    [Range(-1.0f, 1.0f)]
    public float MoodValue;
    public Dictionary<string, Emot_AnimMapping> AnimMappings = new Dictionary<string, Emot_AnimMapping>();


    //-----------------------------------Public Functions----------------------------------

    public Emot_EmotionScale()
    {
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

    public float GetAnimPropertyValue(string propertyName)
    {
        return AnimMappings[propertyName].GetValue(MoodValue * 0.5f + 0.5f);
    }
}
