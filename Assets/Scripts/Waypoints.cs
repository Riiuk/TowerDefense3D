using UnityEngine;

public class Waypoints : MonoBehaviour {

    // Array de puntos de controll static, para poder llamarlo desde otro script
    public static Transform[] puntosDeControl;

    void Awake() {
        // Rellenamos el array de puntosDeControl con los transforms del hijo donde este este script
        puntosDeControl = new Transform[transform.childCount];
        // Recerremos el array asignando en cada pasada el el hijo
        for (int i = 0; i < puntosDeControl.Length; i++) {
            puntosDeControl[i] = transform.GetChild(i);
        }
    }
}
