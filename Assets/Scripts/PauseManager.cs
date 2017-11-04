using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    public string nombreMenu = "MainMenu";
    public GameObject UI;
    public SceneFader fader;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))  {
            Activado();
        }	
	}

    public void Activado() {
        UI.SetActive(!UI.activeSelf);

        if (UI.activeSelf) {
            Time.timeScale = 0f;
        } else
            Time.timeScale = 1f;
    }

    public void Reintentar() {
        Activado();
        fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu() {
        Activado();
        fader.FadeTo(nombreMenu);
    }
}
