using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    public string nombreMenu  = "MainMenu";
    
    public SceneFader fader;
    
    public void Reintentar() {
        fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu() {
        fader.FadeTo(nombreMenu);
    }

    void OnEnable() {
        Time.timeScale = 1f;    
    }
}
