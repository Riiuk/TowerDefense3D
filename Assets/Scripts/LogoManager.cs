using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoManager : MonoBehaviour {

    public string nombreMenu = "MainMenu";
    public GameObject continueText;
    public SceneFader fader;

    private bool continueActive = false;

	// Use this for initialization
	void Start () {
        Invoke("Continue", 3f);
	}
	
	// Update is called once per frame
	void Update () {
        if (continueActive) {
            if (Input.anyKey) {
                fader.FadeTo(nombreMenu);
            }
        }
	}

    void Continue() {
        continueText.SetActive(true);
        continueActive = true;
    }
}
