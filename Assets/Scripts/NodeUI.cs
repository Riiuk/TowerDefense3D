using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    [Tooltip("Objeto de UI")]
    public GameObject UI;
    public Text textoMejora;
    public Text textoVenta;
    public GameObject botonMejora;

    private Node objetivo;
    
    
    /// <summary>
    /// Función creada para llamarla desde el BuildManager para seleccionar un nodo.
    /// </summary>
    /// <param name="_objetivo"></param>
    public void SeleccionarObjetivo(Node _objetivo) {
        objetivo = _objetivo;

        if (!objetivo.estaMejorada) {
            botonMejora.SetActive(true);
            textoMejora.text = "<b>MEJORAR</b>" + "\n" + objetivo.torretaMejorada.costeMejora + "€";
        } else {
            textoMejora.text = "¡MEJORADA!";
            botonMejora.SetActive(false);
        }

        int precioVenta = objetivo.torretaMejorada.coste / 2;
        int precioVentaMejora = objetivo.torretaMejorada.costeMejora / 2 + objetivo.torretaMejorada.coste / 2;

        if (objetivo.estaMejorada) {
            textoVenta.text = "<b>VENDER</b>" + "\n" + precioVentaMejora + "€";
        } else {
            textoVenta.text = "<b>VENDER</b>" + "\n" + precioVenta + "€";
        }
        
        // Activamos el objeto
        UI.SetActive(true);
        // Novemos el objeto a la posicion del nodo seleccionado
        transform.position = objetivo.ConseguirPosicion();
    }

    /// <summary>
    /// Función que esconde el UI de seleccion de torreta
    /// </summary>
    public void Esconder() {
        UI.SetActive(false);
    }

    /// <summary>
    /// Función que llamamos para mejorar la torreta en el nodo seleccionado
    /// </summary>
    public void Mejorar() {
        objetivo.MejorarTorreta();
        BuildManager.BM.Desseleccionar();
    }
    /// <summary>
    /// Funcion que llamamos con el boton de vender la torreta
    /// </summary>
    public void Vender() {
        objetivo.VenderTorreta();
        BuildManager.BM.Desseleccionar();
    }
}
