using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class Stim_SimuliSystem : MonoBehaviour {

    [System.Serializable]
    public struct StimulusImage
    {
        public string StimulusName;
        public Texture2D Texture;
    }

    [SerializeField]
    private TextAsset StimConfigXML;
    [SerializeField]
    private float InfluenceMultiplier = 1f;
    [SerializeField]
    private MeshRenderer Background;
    [SerializeField]
    private List<StimulusImage> StimulusImages = new List<StimulusImage>();

    private Emot_EmotionController mEmotionController;
    private Dictionary<string, Stim_Stimulus> mStimuli = new Dictionary<string, Stim_Stimulus>();
    private Stim_Stimulus mCurrentStimulus = null;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        mEmotionController = FindObjectOfType<Emot_EmotionController>();
        LoadStimuliFromXML(StimConfigXML);
        SetStimulus("Sun");
    }

    private void Update()
    {
        if (mCurrentStimulus == null) return;
        foreach (var emotInfluence in mCurrentStimulus.EmotionInfluences)
            mEmotionController.InfluenceMood(emotInfluence.Key, emotInfluence.Value * InfluenceMultiplier);
    }


    //-----------------------------------Public Functions----------------------------------

    public void SetStimulus(string stimName)
    {
        if (!mStimuli.ContainsKey(stimName)) return;
        mCurrentStimulus = mStimuli[stimName];
        foreach (var stimImage in StimulusImages)
        {
            if (stimImage.StimulusName == stimName)
            {
                Background.material.mainTexture = stimImage.Texture;
                break;
            }
        }
    }


    //----------------------------------Private Functions----------------------------------

    private void LoadStimuliFromXML(TextAsset asset)
    {
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(asset.text);
        foreach (XmlNode xmlStimulus in xmlDoc.GetElementsByTagName("Stimulus"))
        {
            Stim_Stimulus stimulus = NewStimulusFromXMLNode(xmlStimulus);
            mStimuli[stimulus.Name] = stimulus;
        }
    }

    private Stim_Stimulus NewStimulusFromXMLNode(XmlNode xmlNode)
    {
        Stim_Stimulus stimulus = new Stim_Stimulus(xmlNode.Attributes["Name"].Value);
        foreach (XmlNode xmlEmotionMapping in xmlNode.ChildNodes)
        {
            string emotionName = xmlEmotionMapping.Attributes["Name"].Value;
            float influence = float.Parse(xmlEmotionMapping.Attributes["Influence"].Value);
            stimulus.EmotionInfluences[emotionName] = influence;
        }
        return stimulus;
    }
}
