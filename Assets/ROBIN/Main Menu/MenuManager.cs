using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void playButtonPress(){
        SceneManager.LoadSceneAsync(1);
    }
    public void tutorialButtonPress(){
        
    }
    public void optionButtonPress(){
        
    }
    public void creditsButtonPress(){
        SceneManager.LoadSceneAsync(2);
    }
    public void quitButtonPress(){
        Application.Quit();
    }
}
