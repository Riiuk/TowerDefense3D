using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("Atributos")]
    public float speed = 30f;                   // Variable de velocidad de movimiento para la cámara
    public float tamañoBordePantalla = 10f;     // Variable para definir el borde de la pantalla
    public float velocidadZoom = 5f;            // Variable que limita la velocidad del zoom de la cámara
    public float minY = 10f;                    // Zoom mínimo
    public float maxY = 80f;                    // Zoom máximo

    private bool movimiento = true;                    // Variable tipo bool para controlar el movimiento

	// Update is called once per frame
	void Update () {

        if (GameManager.finDelJuego) {
            this.enabled = false;
            return;
        }

        // Si pulsamos la tecla escape, bloqueamos el movimiento de la camara
        if (Input.GetKeyDown(KeyCode.X)) {
            movimiento = !movimiento;
        }

        // Si movimiento esta activo, activmaos el control de la camara
        if (movimiento) {
            // Si pulsamos W o movemos el raton a la posicion superior de la pantalla, moveremos la camara hacia adelante,
            // y asi con las demas teclas de movimientos
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - tamañoBordePantalla) {
                transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= tamañoBordePantalla) {
                transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - tamañoBordePantalla) {
                transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= tamañoBordePantalla) {
                transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            }

            // Variable temporal donde almacenaremos el valor de la rueda del raton
            float mouseAxis = Input.GetAxis("Mouse ScrollWheel");

            // Variable temporal de tipo Vector3 donde almacenamos la posicion de la camara
            Vector3 posicion = transform.position;

            // Le restamos a la posición de la camara el valor optenido por el Input de la rueda del raton,
            // multimplicada por la velocidad que le tengamos asignada de desplazamiento y el Time.deltaTime
            posicion.y -= mouseAxis * 1000 * velocidadZoom * Time.deltaTime;
            // Aplicamos un mínimo y un máximo a dicho movimiento
            posicion.y = Mathf.Clamp(posicion.y, minY, maxY);
            // Movemos la cámara
            transform.position = posicion;
        }
            
    }
}
