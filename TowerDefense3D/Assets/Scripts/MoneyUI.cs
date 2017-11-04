using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour {

    public Text textoDinero;
	
	// Update is called once per frame
	void Update () {
        textoDinero.text = PlayerManager.Dinero.ToString() + "€";
	}
}
