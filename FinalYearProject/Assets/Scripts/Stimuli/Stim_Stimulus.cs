using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stim_Stimulus {

    public string Name = "";
    public Dictionary<string, float> EmotionInfluences = new Dictionary<string, float>();


    //-----------------------------------Public Functions----------------------------------

    public Stim_Stimulus(string name)
    {
        Name = name;
    }
}
