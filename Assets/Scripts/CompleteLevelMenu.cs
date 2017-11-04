using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevelMenu : MonoBehaviour {

    public string nombreMenu = "MainMenu";
    public SceneFader fader;
    public GameManager gameManager;

    [Header("Variables de Siguiente Nivel")]
    public string siguienteNivel = "Level01";
    public int nivelParaDesbloquear = 1;

    public void Continuar() {
        PlayerPrefs.SetInt("nivelAlcanzado", nivelParaDesbloquear);
        fader.FadeTo(siguienteNivel);
    }

    public void Menu() {
        PlayerPrefs.SetInt("nivelAlcanzado", nivelParaDesbloquear);
        fader.FadeTo(nombreMenu);
    }

    void OnEnable() {
        Time.timeScale = 1f;
    }
}
