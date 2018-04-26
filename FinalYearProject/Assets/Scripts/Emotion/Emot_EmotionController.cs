using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Emot_EmotionController : MonoBehaviour {

    [HideInInspector]
    public Dictionary<string, Emot_Emotion> Emotions = new Dictionary<string, Emot_Emotion>();

    [SerializeField]
    private float MoodChangeSpeed = 0.05f;
    [SerializeField]
    private TextAsset EmotConfigXML;
    [SerializeField]
    private TextAsset PersonalityXML;
    


    //-----------------------------------Unity Functions-----------------------------------

    private void Awake()
    {
        LoadEmotionData(EmotConfigXML);
        LoadPersonality(PersonalityXML);
    }

    private void Update()
    {
        foreach (var emotion in Emotions)
            emotion.Value.UpdateValues(MoodChangeSpeed);
    }


    //-----------------------------------Public Functions----------------------------------

    public float GetAnimPropertyValue(string propertyName)
    {
        float weightedPropertyTotal = 0f;
        float moodTotal = 0f;

        foreach (var emotion in Emotions)
        {
            var adjustedMoodValue = Mathf.Max(emotion.Value.MoodValue, 0.001f);
            weightedPropertyTotal += emotion.Value.AnimMappings[propertyName] * adjustedMoodValue;
            moodTotal += adjustedMoodValue;
        }
        
        return weightedPropertyTotal /= moodTotal;
    }

    public void NormalisePersonality(string emotionToPreserveName)
    {
        if (!Emotions.ContainsKey(emotionToPreserveName)) return;

        Emot_Emotion emotionToScale = null;
        List<Emot_Emotion> allOtherEmotions = new List<Emot_Emotion>();

        foreach (var emotion in Emotions)
        {
            if (emotion.Key == emotionToPreserveName)
                emotionToScale = emotion.Value;
            else
                allOtherEmotions.Add(emotion.Value);
        }

        // Find amount to change.
        float targetRemainder = 1f - emotionToScale.PersonalityValue;
        float remainder = 0f;
        foreach (var emotion in allOtherEmotions)
            remainder += emotion.PersonalityValue;
        float delta = targetRemainder - remainder;

        // Scale values.
        foreach (var emotion in allOtherEmotions)
        {
            if (targetRemainder <= float.Epsilon)
                emotion.PersonalityValue = 0f;
            else
            {
                float ratio = 0f;
                if (remainder <= float.Epsilon)
                    ratio = 1f / allOtherEmotions.Count;
                else
                    ratio = emotion.PersonalityValue / remainder;

                emotion.PersonalityValue += delta * ratio;
            }            
        }
    }

    public float GetMoodValue(string emotName)
    {
        if (!Emotions.ContainsKey(emotName)) return 0f;
        return Emotions[emotName].MoodValue;
    }

    public float GetPersonalityValue(string emotName)
    {
        if (!Emotions.ContainsKey(emotName)) return 0f;
        return Emotions[emotName].PersonalityValue;
    }

    public void SetMoodValue(string emotName, float value)
    {
        if (!Emotions.ContainsKey(emotName)) return;
        Emotions[emotName].MoodValue = value;
    }

    public void SetPersonalityValue(string emotName, float value)
    {
        if (!Emotions.ContainsKey(emotName)) return;
        Emotions[emotName].PersonalityValue = value;
        NormalisePersonality(emotName);
    }

    public string[] GetEmotionNames()
    {
        string[] emotName = new string[Emotions.Keys.Count];
        Emotions.Keys.CopyTo(emotName, 0);
        return emotName;
    }

    public void InfluenceMood(string emotName, float influenceAmount)
    {
        if (!Emotions.ContainsKey(emotName)) return;
        Emotions[emotName].MoodValue += influenceAmount * Time.deltaTime * MoodChangeSpeed;
    }


    //----------------------------------Private Functions----------------------------------

    private void LoadEmotionData(TextAsset asset)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(asset.text);
        LoadEmotionsFromXMLNode(xmlDoc.GetElementsByTagName("Emotions").Item(0), ref Emotions);
    }

    private void LoadPersonality(TextAsset asset)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(asset.text);
        LoadPersonalityFromXMLNode(xmlDoc.GetElementsByTagName("Emotions").Item(0), ref Emotions);
    }

    private void LoadEmotionsFromXMLNode(XmlNode parentNode, ref Dictionary<string, Emot_Emotion> emotionDict)
    {
        foreach (XmlNode xmlEmotion in parentNode.ChildNodes)
        {
            var emotion = new Emot_Emotion(xmlEmotion.Attributes["Name"].Value);
            var xmlAnimMappings = xmlEmotion.ChildNodes;

            foreach (XmlNode xmlAnimMapping in xmlAnimMappings)
                emotion.AnimMappings.Add(xmlAnimMapping.Attributes["Name"].Value, float.Parse(xmlAnimMapping.Attributes["Value"].Value));

            emotionDict.Add(emotion.Name, emotion);
        }
    }

    private void LoadPersonalityFromXMLNode(XmlNode parentNode, ref Dictionary<string, Emot_Emotion> emotionDict)
    {
        foreach (XmlNode xmlEmotion in parentNode.ChildNodes)
        {
            string name = xmlEmotion.Attributes["Name"].Value;
            if (emotionDict.ContainsKey(name))
                emotionDict[name].SetPersonalityValue(float.Parse(xmlEmotion.Attributes["Value"].Value));
        }
    }
}
