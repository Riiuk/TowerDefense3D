using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    [Header("Torretas")]
    public GameObject torretaEstandar;          // GameObject de la torreta estandar
    public GameObject torretaMisiles;           // GameObject de la torreta de misiles

    [Header("Efectos")]
    public GameObject efectoConstruccion;       // GameObject del efecto de particulas al construir
    public GameObject efectoVenta;
    public float timepoDeDestruccion;           // Tiempo que tardará el efecto en destruirse

    [Header("Añadimos el NodoUI por defecto")]
    public NodeUI nodeUI;

    [HideInInspector]
    private TurretBlueprint torretaParaConstruir;    // GameObject de la torreta para construir

    private Node nodoSeleccionado;              // Nodo seleccionado actualmente
    
    public static BuildManager BM;              // Variable tipo static para poder llamar a este script desde otro

    void Update() {
        // Si pulsamos el boton izquierdo del raton, desseleccionamos lo que tengamos seleccionado
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            torretaParaConstruir = null;
            Desseleccionar();
        }    
    }

    void Awake() {
        if (BM != null) {
            Debug.LogError("¡Mas de un BuildManager en la escena!");
            return;
        } else {
            BM = this;
        }
    }
    /// <summary>
    /// Funcion bool para comprobar si se tiene una torreta seleccionada
    /// </summary>
    public bool PuedoConstruir { get { return torretaParaConstruir != null; } }
    /// <summary>
    /// Funcion bool para comprobar si se tiene Dinero para construir la torreta seleccionada
    /// </summary>
    public bool TengoDinero { get { return PlayerManager.Dinero >= torretaParaConstruir.coste; } }
    
    public void SeleccionarNodo(Node nodo) {
        if (nodoSeleccionado == nodo) {
            Desseleccionar();
            return;
        }
        nodoSeleccionado = nodo;
        torretaParaConstruir = null;
        nodeUI.SeleccionarObjetivo(nodo);
    }

    public void Desseleccionar() {
        nodoSeleccionado = null;
        nodeUI.Esconder();
    }

    /// <summary>
    /// Función que usamos para seleccionar la torreta a construir, la cual nos traemos desde shop
    /// </summary>
    /// <param name="torreta"></param>
    public void SeleccionarTorretaParaConstruir(TurretBlueprint torreta) {
        // Igualamos la torreta la cual construiremos con la torreta traida desde shop
        torretaParaConstruir = torreta;
        Desseleccionar();
    }

    public TurretBlueprint ConseguirTorretaParaConstruir() {
        return torretaParaConstruir;
    }
}
