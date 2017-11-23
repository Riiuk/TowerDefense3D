using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {
    
    [Tooltip("Variale publica de tipo SceneFader")]
    public SceneFader fader;

    [Tooltip("Array de botones de niveles")]
    public Button[] botonesNivel;
    
    void Start() {
        // Variable que almacena el nivel máximo alcanzado por el jugador,
        // guardado en un PlayerPrefs para tenerlo aunque cerremos el juego
        int nivelAlcanzado = PlayerPrefs.GetInt("nivelAlcanzado", 1);

        // Recorremos la lista de botones
        for (int i = 0; i < botonesNivel.Length; i++) {
            // Si el indice de la lista + 1 (para igualar al nivel) es > que el máximo nivel actual,
            // desactivaremos los botones
            if (i + 1 > nivelAlcanzado) {
                botonesNivel[i].interactable = false;
            }
        }    
    }

    /// <summary>
    /// Función llamada por los botones de selección de nivel
    /// </summary>
    /// <param name="nombreNivel"></param>
    public void Seleccionar(string nombreNivel) {
        fader.FadeTo(nombreNivel);
        FindObjectOfType<AudioManager>().Play("Click");
    }
}
