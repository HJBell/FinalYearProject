using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_UIManager : MonoBehaviour {

    public static UI_UIManager Instance;


    //-----------------------------------Unity Functions-----------------------------------

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }


    //-----------------------------------Public Functions----------------------------------

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }


    //----------------------------------Private Functions----------------------------------
   
}
