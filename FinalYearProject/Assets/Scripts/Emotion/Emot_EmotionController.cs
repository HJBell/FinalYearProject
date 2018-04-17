using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Emot_EmotionController : MonoBehaviour {

    [HideInInspector]
    public List<Emot_Emotion> Emotions = new List<Emot_Emotion>();

    [SerializeField]
    private TextAsset EmotConfigXML;
    [SerializeField]
    private TextAsset PersonalityXML;
    


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        LoadEmotionData(EmotConfigXML);
        LoadPersonality(PersonalityXML);
    }

    private void Update()
    {
        foreach (var emotion in Emotions)
            emotion.UpdateValues();
    }


    //-----------------------------------Public Functions----------------------------------

    public float GetAnimPropertyValue(string propertyName)
    {
        float weightedPropertyTotal = 0f;
        float moodTotal = 0f;

        foreach (var emotion in Emotions)
        {
            var adjustedMoodValue = Mathf.Max(emotion.MoodValue, 0.001f);
            weightedPropertyTotal += emotion.AnimMappings[propertyName] * adjustedMoodValue;
            moodTotal += adjustedMoodValue;
        }
        
        return weightedPropertyTotal /= moodTotal;
    }

    public void NormalisePersonality(string emotionToPreserveName)
    {
        Emot_Emotion emotionToScale = null;
        List<Emot_Emotion> allOtherEmotions = new List<Emot_Emotion>();

        foreach (var emotion in Emotions)
        {
            if (emotion.Name == emotionToPreserveName)
                emotionToScale = emotion;
            else
                allOtherEmotions.Add(emotion);
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

    private void LoadEmotionsFromXMLNode(XmlNode parentNode, ref List<Emot_Emotion> emotionList)
    {
        foreach (XmlNode xmlEmotion in parentNode.ChildNodes)
        {
            var emotion = new Emot_Emotion(xmlEmotion.Attributes["Name"].Value);
            var xmlAnimMappings = xmlEmotion.ChildNodes;

            foreach (XmlNode xmlAnimMapping in xmlAnimMappings)
                emotion.AnimMappings.Add(xmlAnimMapping.Attributes["Name"].Value, float.Parse(xmlAnimMapping.Attributes["Value"].Value));

            emotionList.Add(emotion);
        }
    }

    private void LoadPersonalityFromXMLNode(XmlNode parentNode, ref List<Emot_Emotion> emotionList)
    {
        foreach (XmlNode xmlEmotion in parentNode.ChildNodes)
        {
            foreach (var emotion in emotionList)
            {
                if (emotion.Name == xmlEmotion.Attributes["Name"].Value)
                {
                    emotion.SetPersonalityValue(float.Parse(xmlEmotion.Attributes["Value"].Value));
                    break;
                }
            }
        }
    }
}
