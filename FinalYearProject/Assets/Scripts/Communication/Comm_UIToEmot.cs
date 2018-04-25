using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Comm_UIToEmot : MonoBehaviour {

    [System.Serializable]
    public struct EmotUISliders
    {
        public Slider MoodSlider;
        public Slider PersonalitySlider;
    }

    [SerializeField]
    private Emot_EmotionController EmotController;
    [SerializeField]
    private EmotUISliders HappinessSliders;
    [SerializeField]
    private EmotUISliders ConfidenceSliders;
    [SerializeField]
    private EmotUISliders PeaceSliders;
    [SerializeField]
    private EmotUISliders SadnessSliders;
    [SerializeField]
    private EmotUISliders FearSliders;
    [SerializeField]
    private EmotUISliders AngerSliders;

    private Dictionary<string, EmotUISliders> mSliderDict = new Dictionary<string, EmotUISliders>();


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        mSliderDict["Happiness"] = HappinessSliders;
        mSliderDict["Confidence"] = ConfidenceSliders;
        mSliderDict["Peace"] = PeaceSliders;
        mSliderDict["Sadness"] = SadnessSliders;
        mSliderDict["Fear"] = FearSliders;
        mSliderDict["Anger"] = AngerSliders;

        foreach (var sliders in mSliderDict)
        {
            UpdateSlidersFromEmotion(sliders.Value, sliders.Key);
            sliders.Value.MoodSlider.onValueChanged.AddListener(delegate { OnMoodSliderChanged(sliders.Key); });
            sliders.Value.PersonalitySlider.onValueChanged.AddListener(delegate { OnPeronalitySliderChanged(sliders.Key); });
        }
    }

    private void Update()
    {
        foreach (var sliders in mSliderDict)
            UpdateSlidersFromEmotion(sliders.Value, sliders.Key);
    }


    //-----------------------------------Public Functions----------------------------------

    public void OnMoodSliderChanged(string emotName)
    {
        EmotController.SetMoodValue(emotName, mSliderDict[emotName].MoodSlider.value);
    }

    public void OnPeronalitySliderChanged(string emotName)
    {
        EmotController.SetPersonalityValue(emotName, mSliderDict[emotName].PersonalitySlider.value);
    }


    //----------------------------------Private Functions----------------------------------

    private void UpdateSlidersFromEmotion(EmotUISliders sliders, string emotName)
    {
        sliders.MoodSlider.value = EmotController.GetMoodValue(emotName);
        sliders.PersonalitySlider.value = EmotController.GetPersonalityValue(emotName);
    }
}
        
