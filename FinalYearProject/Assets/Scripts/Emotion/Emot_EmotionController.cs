using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Emot_EmotionController : MonoBehaviour {

    [SerializeField]
    private List<Emot_EmotionScale> EmotionScales = new List<Emot_EmotionScale>();
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
        foreach (var emotionScale in EmotionScales)
            emotionScale.UpdateValues();
    }


    //-----------------------------------Public Functions----------------------------------

    public float GetAnimPropertyValue(string propertyName)
    {
        float total = 0f;
        foreach (var scale in EmotionScales)
            total += scale.GetAnimPropertyValue(propertyName);
        return total /= EmotionScales.Count;
    }


    //----------------------------------Private Functions----------------------------------

    private void LoadEmotionData(TextAsset asset)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(asset.text);

        var xmlEmotionScales = xmlDoc.GetElementsByTagName("EmotionScale");

        foreach (XmlNode xmlEmotionScale in xmlEmotionScales)
        {
            var emotionScale = new Emot_EmotionScale();
            emotionScale.NegativeName = xmlEmotionScale.Attributes["Negative"].Value;
            emotionScale.PositiveName = xmlEmotionScale.Attributes["Positive"].Value;

            var xmlScaleProperties = xmlEmotionScale.ChildNodes;

            foreach(XmlNode xmlScaleProperty in xmlScaleProperties)
            {
                Emot_AnimMapping animMapping = new Emot_AnimMapping(float.Parse(xmlScaleProperty.Attributes["Min"].Value),
                                                                    float.Parse(xmlScaleProperty.Attributes["Max"].Value));
                emotionScale.AnimMappings.Add(xmlScaleProperty.Attributes["Name"].Value, animMapping);
            }

            EmotionScales.Add(emotionScale);
        }
    }

    private void LoadPersonality(TextAsset asset)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(asset.text);

        var xmlEmotionScales = xmlDoc.GetElementsByTagName("EmotionScale");

        foreach (XmlNode xmlEmotionScale in xmlEmotionScales)
        {
            foreach(var emotionScale in EmotionScales)
            {
                if(emotionScale.NegativeName == xmlEmotionScale.Attributes["Negative"].Value &&
                   emotionScale.PositiveName == xmlEmotionScale.Attributes["Positive"].Value)
                {
                    emotionScale.SetPersonalityValue(float.Parse(xmlEmotionScale.Attributes["Value"].Value));
                    break;
                }
            }
        }
    }
}
