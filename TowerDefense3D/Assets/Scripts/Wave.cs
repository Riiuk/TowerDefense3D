using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave {

    [Tooltip("Prefab que contiene el enemigo")]
    public GameObject enemigoPrefab;
    [Tooltip("Número de enemigos que se Spawnean por oleada")]
    public int contador;
    [Tooltip("Ratio de spawn de enemigos")]
    public float rate;
}
