using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform objetivo;             // Punto de referencia para el movimiento del enemigo
    private int numeroPuntoDeControl = 0;   // Posición donde se moverá el enemigo
    private Enemy enemigo;

    void Start() {
        enemigo = GetComponent<Enemy>();
        // Asignamos el transform de objetivo al inicial del array de Waypoints.puntosDeControl
        objetivo = Waypoints.puntosDeControl[0];
    }

    void Update() {
        // Creamos un Vector3 de direccion igual a la posicion del objetivo restada a la nuestra,
        // con lo que creamos una linea recta entre nosotros y el objetivo
        Vector3 direccion = objetivo.position - transform.position;
        // Nos movemos desde nuestra posicion, hacia la posicion del objetivo, de forma "normalizada",
        // a la velocidad del velocidad y con coordenadas relacionadas con el mundo
        transform.Translate(direccion.normalized * enemigo.velocidad * Time.deltaTime, Space.World);

        // Si la distancia entre la posicion del waypoint y nosotros es menor de 0.4,
        // llamamos a coger el siguiente punto de control
        if (Vector3.Distance(objetivo.position, transform.position) <= 0.4f) {
            SiguientePuntoDeControl();
        }

        enemigo.velocidad = enemigo.velocidadInicial;
    }

    /// <summary>
    /// Cambiamos el objetivo al siguiente hijo de Waypoints
    /// </summary>
    void SiguientePuntoDeControl() {

        // Si el indice de waypoints es mayor o igual al tamaño del array de puntosDeControl -1 (porque empieza en 0),
        // llamamos a la funcion de llegar al ultimo punto
        if (numeroPuntoDeControl >= Waypoints.puntosDeControl.Length - 1) {
            FinDelCamino();
            return;
        }

        // Suma 1 al indice de waypoints
        numeroPuntoDeControl++;
        // Cambiamos el objetivo al transform guardado en el array de puntosDeControl
        objetivo = Waypoints.puntosDeControl[numeroPuntoDeControl];
    }
    // =========================================
    /// <summary>
    /// Funcion que se llama cuando llegamos al último punto, con la que quitamos una vida y destruimos el objeto
    /// </summary>
    void FinDelCamino() {
        PlayerManager.Vidas--;
        WaveManager.EnemigosVivos--;
        Destroy(gameObject);
    }
}
