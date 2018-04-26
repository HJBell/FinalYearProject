using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_UserTestManager : MonoBehaviour {

    [SerializeField]
    private Text EmotionUIText;

    private Emot_EmotionController mEmotionController;
    private string[] mEmotionNames;
    private int mEmotionIndex = -1;


    //-----------------------------------Unity Functions-----------------------------------

    private void Start()
    {
        mEmotionController = FindObjectOfType<Emot_EmotionController>();
        mEmotionNames = mEmotionController.GetEmotionNames();

        RegenEmotionOrder();
        SetEmotionText("");
        UpdateEmotionController();
    }


    //-----------------------------------Public Functions----------------------------------

    public void NextEmotion()
    {
        mEmotionIndex++;
        if (mEmotionIndex == mEmotionNames.Length)
            RegenEmotionOrder();

        SetEmotionText("");
        UpdateEmotionController();
    }

    public void ShowCurrentEmotion()
    {
        SetEmotionText(mEmotionNames[mEmotionIndex]);
    }


    //----------------------------------Private Functions----------------------------------

    private void UpdateEmotionController()
    {
        mEmotionController.SetPersonalityValue(mEmotionNames[mEmotionIndex], 1f);
    }

    private void SetEmotionText(string text)
    {
        EmotionUIText.text = text;
    }

    private void RegenEmotionOrder()
    {
        mEmotionNames = HJBUnityUtils.RandomiseArrayOrder(mEmotionNames);
        mEmotionIndex = 0;
    }
}
