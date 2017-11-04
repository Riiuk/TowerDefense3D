using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [Tooltip("Variable tipo texto para elegir el nombre del nivel a cargar")]
    public string escena;
    [Tooltip("Objeto tipo SceneFader")]
    public SceneFader sceneFader;

    void Start() {
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Variable que llamaremos con el boton empezar
    /// </summary>
    public void Empezar() {
        sceneFader.FadeTo(escena);
    }

    /// <summary>
    /// Variable que llamaremos con el boton salir
    /// </summary>
    public void Salir() {
        Debug.Log("Saliendo de la aplicacion...");
        Application.Quit();
    }
}
