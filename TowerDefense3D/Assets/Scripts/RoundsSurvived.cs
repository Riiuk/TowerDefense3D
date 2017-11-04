using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour {
    
    public float tiempoAnimacionTexto = .05f;
    public Text rondasText;

    void OnEnable() {
        StartCoroutine(TextoAnimado());
    }

    IEnumerator TextoAnimado() {

        rondasText.text = "0";

        int rondas = 0;

        yield return new WaitForSeconds(.07f);

        while (rondas < PlayerManager.Rondas) {
            rondas++;
            rondasText.text = (rondas).ToString();

            yield return new WaitForSeconds(tiempoAnimacionTexto);
        }
    }

}
