using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creamos la clase serializable, para poder acceder a ella desde otro script
[System.Serializable]
public class TurretBlueprint {
    [Tooltip("Variable que guardará nuestro prefab de torreta")]
    public GameObject prefab;
    [Tooltip("Vaiable que guardará el coste de producción que tiene nuestra torreta")]
    public int coste;
    [Tooltip("Variable que guardará el prefab de la mejora de la torreta")]
    public GameObject prefabMejora;
    [Tooltip("Variable que guardará el coste de la mejora de la torreta")]
    public int costeMejora;


    public int CalcularPrecioVenta() {
        return costeMejora;
    }
}
