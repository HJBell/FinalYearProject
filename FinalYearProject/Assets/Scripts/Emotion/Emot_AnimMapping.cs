using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Emot_AnimMapping {

    public float Min, Max;


    //-----------------------------------Public Functions----------------------------------

    public Emot_AnimMapping(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public float GetValue(float t)
    {
        return Mathf.Lerp(Min, Max, t);
    }
}