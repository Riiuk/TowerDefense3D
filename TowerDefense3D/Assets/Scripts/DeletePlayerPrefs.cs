using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayerPrefs : MonoBehaviour {

    [Header("Variables")]
    public string nombreMenu = "MainMenu";

    [Header("Unity Objects")]
    public GameObject noticePanel;
    public SceneFader fader;

    public void DeleteDATA() {
        FindObjectOfType<AudioManager>().Play("Click");
        PlayerPrefs.DeleteAll();
        noticePanel.SetActive(false);
        fader.FadeTo(nombreMenu);
    }

    public void DeleteButton() {
        FindObjectOfType<AudioManager>().Play("Click");
        noticePanel.SetActive(true);
    }

    public void Cancel() {
        FindObjectOfType<AudioManager>().Play("Click");
        noticePanel.SetActive(false);
    }
}
