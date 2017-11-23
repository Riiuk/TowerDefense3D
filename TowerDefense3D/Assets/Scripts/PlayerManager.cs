using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    [Header("Atributo comercio")]
    [Tooltip("Dinero inicial del jugador")]
    public int dineroInicial = 400;
    
    [Header("Atributor de vida")]
    [Tooltip("Vidas iniciales del jugador")]
    public int vidasIniciales;

    public static int Rondas;   // Rondas a las que ha sobrevivido el jugador
    public static int Dinero;   // Dinero del que dispone el jugador
    public static int Vidas;    // Vidas actuales del jugador

    // Use this for initialization
    void Start() {
        Dinero = dineroInicial;
        Vidas = vidasIniciales;
        Rondas = 0;
    }

    /// <summary>
    /// Cheat
    /// </summary>
    void Update()
    {
        // Cheat Mode
        if (Input.GetKeyDown(KeyCode.M))
        {
            Dinero += 1000;
        }    
    }
}
