using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Unity Objects")]
    [Tooltip("Panel de GameOver")]
    public GameObject gameOverUI;
    [Tooltip("Panel de Nivel Completado")]
    public GameObject nivelCompletoUI;

    public static bool finDelJuego;

    void Start() {
        finDelJuego = false;    
    }

    // Update is called once per frame
    void Update () {

        if (finDelJuego)
            return;
        
        if (PlayerManager.Vidas <= 0) {
            FinDelJuego();
        }
	}

    void FinDelJuego() {
        finDelJuego = true;
        gameOverUI.SetActive(true);
        Debug.Log("¡¡Fin del Juego!!");
    }

    public void GanarNivel() {
        finDelJuego = true;
        nivelCompletoUI.SetActive(true);
        
    }
}
