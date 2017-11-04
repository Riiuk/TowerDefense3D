using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

    public Text textoVidas;

    // Update is called once per frame
    void Update() {

        if (PlayerManager.Vidas == 1) {
            textoVidas.text = PlayerManager.Vidas + " VIDA";
        } else {
            textoVidas.text = PlayerManager.Vidas + " VIDAS";
        }
        
    }
}
