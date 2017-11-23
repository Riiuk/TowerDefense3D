using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    [Header("Variables comunes")]
    [Tooltip("Variable para almacenar el precio de venta")]
    public int precioVenta;
    [Header("Localización de objetivos")]
    [Tooltip("Rango de disparo de las torretas")]
    public float rango = 15f;
    [Tooltip("Tag de Enemigo")]
    public string tagEnemigo = "Enemigo";

    [Header("Rotación de la torreta")]
    [Tooltip("Transform de la parte que gira de la torreta")]
    public Transform parteRotatoria;            
    [Tooltip("Velovidad del giro de la torreta")]
    public float velocidadDeGiro = 10f;         

    [Header("Disparo de la torreta")]
    [Tooltip("Cadencia de fuego")]
    public float cedenciaDisparo = 1f;          
    [Tooltip("GameObject de la bala para disparar")]
    public GameObject prefabBala;               
    [Tooltip("Punto donde instanciaremos la bala")]
    public Transform puntoDisparo;
    
    [Header("Disparo del laser")]
    [Tooltip("Variable booleana para establecer publicamente cuando esta activo el Laser")]
    public bool usarLaser = false;
    [Tooltip("Valor de daño que realiza el laser cada segundo")]
    public int dañoEnTiempo = 10;
    [Tooltip("Porcentaje que frenara la torreta")]
    public float porcentajeFrenado = .5f;
    [Tooltip("Variable públic que almacena el LineRenderer del efecto de impacto")]
    public LineRenderer lineRenderer;
    [Tooltip("Variable públic que almacena el ParticleSystem del efecto de impacto")]
    public ParticleSystem efectoImpacto;
    [Tooltip("Objeto del LightPoint de la torreta Laser")]
    public Light efectoLuz;
    public bool playLaser = false;

    
    private Transform objetivo;                 // Transform del objetivo
    private Transform rotacionInicial;          // Transform de la posicion inicial de la torreta
    private Enemy objetivoEnemy;
    private float cuentaAtrasDisparos = 0f;     // Float usado para almacenar la cadencia de fuego
    
    // Use this for initialization
    void Start () {
        // Llamamos de forma repetida a la funcion ActualizarObjetivo
        InvokeRepeating("ActualizarObjetivo", 0f, .75f);
        // Asignamos la rotacionInicial de la torreta al transform al body de la torreta
        rotacionInicial = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
        // Si el objetivo es null, llamamos a la funcion PosicionInicial
        if (objetivo == null) {
            PosicionInicial();
            // Y si estamos usando el Laser y el LineRenderer esta activado, lo desactivaremos,
            // De esta forma, al activarlo solo durante el disparo del mismo, al terminar este se desactivara.
            if (usarLaser) {
                if (lineRenderer.enabled) {
                    lineRenderer.enabled = false;
                    efectoImpacto.Stop();
                    efectoLuz.enabled = false;
                }
            }
            return;
        }
        // Giramos la torreta
        GirarTorreta();

        if (!playLaser)
            FindObjectOfType<AudioManager>().Stop("Laser");

        // Si el laser esta activo, llamamos a la funcion de lanzar el rayo,
        // Si no, disparamos la bala como normalmente
        if (usarLaser) {
            DispararLaser();
        } else {
            // Si la cuenta atras de disparar es menor o igual que 0,
            //disparamos y volemos a calcular la cuenta atras entre disparos
            if (cuentaAtrasDisparos <= 0f) {
                Disparar();
                cuentaAtrasDisparos = 1f / cedenciaDisparo;
            }
            // Descontamos 1 cada 1 segundos a cuenta atras
            cuentaAtrasDisparos -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Actualiza el objetivo al que apunta la torreta
    /// </summary>
    void ActualizarObjetivo() {
        
        // Creamos un array de enemigos que llenamos segun los tags que encontremos de enemigos
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigo);
        // Calculamos la distancia mas corta
        float distanciaMasCorta = Mathf.Infinity;
        // creamos el GameObject de enemigo mas cerca pero lo creamos vacio
        GameObject enemigoMasCerca = null;

        // Recorremos el array de enemigos, sacandolos de uno en uno
        foreach (GameObject enemigo in enemigos) {
            

            // Calculamos la distancia que hay entre el enemigo y la torreta
            float distanciaHaciaEnemigo = Vector3.Distance(transform.position, enemigo.transform.position);

            // Si esta distancia es menor a la distancia mas corta
            if (distanciaHaciaEnemigo < distanciaMasCorta) {
                // Asignamos la distancia hacia el enemigo a la distancia mas corta
                distanciaMasCorta = distanciaHaciaEnemigo;
                // Y asignamos el enemigo mas cerca a este enemigo que tenemos seleccionado
                enemigoMasCerca = enemigo;
            }
        }

        // Si tenemos un enemigo como el mas cercano
        // y la distancia mas corta es menor o igual al rango de disparo
        if (enemigoMasCerca != null && distanciaMasCorta <= rango) {
            // Asignamos al objetivo el transform del enemigo mas cercano
            objetivo = enemigoMasCerca.transform;
            // Instanciamos el Script de Enemy en la variable
            objetivoEnemy = enemigoMasCerca.GetComponent<Enemy>();

            if (usarLaser)
                playLaser = true;
                FindObjectOfType<AudioManager>().Play("Laser");
        } else {
            objetivo = null;
            FindObjectOfType<AudioManager>().Stop("Laser");
        }
    }

    /// <summary>
    /// Hacemos girar la torreta hacia el objetivo mas cercano
    /// </summary>
    void GirarTorreta() {
        // Sacamos un Vector3 de con la direccion entre el objetivo y nosotros
        Vector3 direccion = objetivo.position - transform.position;
        // Sacamos un Quaternion con la rotacion necesaria para llegar al objetivo
        Quaternion rotacionDestino = Quaternion.LookRotation(direccion);
        // Calculamos la velocidad de rotación de la torreta
        Vector3 rotacion = Quaternion.Lerp(parteRotatoria.rotation, rotacionDestino, Time.deltaTime * velocidadDeGiro).eulerAngles;
        // Giramos la torreta hacia el destino
        parteRotatoria.rotation = Quaternion.Euler(0f, rotacion.y, 0f);
    }

    /// <summary>
    /// Vuelve la torreta a la posicion inicial
    /// </summary>
    void PosicionInicial() {
        // Asignamos a un Vector3 de rotacion la rotacin que debe hacer la torreta para volver a la posicion inicial
        Vector3 rotacion = Quaternion.Lerp(parteRotatoria.rotation, rotacionInicial.rotation, Time.deltaTime * velocidadDeGiro).eulerAngles;
        // Movemos la parte rotatoria hacia dicha posicion, de forma suavizada
        parteRotatoria.rotation = Quaternion.Euler(0f, rotacion.y, 0f);

        playLaser = false;
    }

    /// <summary>
    /// Realizamos el disparo de la bala
    /// </summary>
    void Disparar() {
        // Instanciamos la bala como un nuevo GameObject llamado balaGO
        GameObject balaGO = (GameObject)Instantiate(prefabBala, puntoDisparo.position, puntoDisparo.rotation);
        // Llamamos al componente Bullet
        Bullet bala = balaGO.GetComponent<Bullet>();

        FindObjectOfType<AudioManager>().Play("Shoot");
        // Si el componente bala no esta vacio, llamamos a la función buscar asignandole el target actual del disparo
        if (bala != null) {
            bala.Buscar(objetivo);
        }
    }

    /// <summary>
    /// Disparamos el laser
    /// </summary>
    void DispararLaser() {
        // Dibujamos la linea del laser y llamamos a los efectos
        DibujarLaser();
        // Realizamos el daño de la torreta laser
        DañoLaser();

        objetivoEnemy.Frenar(porcentajeFrenado);
    }

    /// <summary>
    /// Función que usamos para el apartado grafico de la Torreta Laser (Dibujar el rayo y activar/desactivar los efectos)
    /// </summary>
    void DibujarLaser() {
        if (!lineRenderer.enabled) {
            lineRenderer.enabled = true;
            efectoImpacto.Play();
            efectoLuz.enabled = true;
        }
        // Asignamos la primera posicion del LineRenderer sobre el punto establecido de disparo 
        lineRenderer.SetPosition(0, puntoDisparo.position);
        // Asignamos la segunda posicion del LineRenderer sobre el objetivo de la torreta
        lineRenderer.SetPosition(1, objetivo.position);
        // Creamos un Vector3 de direccion en la direccion desde el objetivo hasta el punto de disparo
        Vector3 direccion = puntoDisparo.position - objetivo.position;
        // Movemos el ParticleSystem a la posicion del objetivo, en la direccion normalizada y añadiendo un offset
        efectoImpacto.transform.position = objetivo.position + direccion.normalized;
        // Rotamos el ParticleSystem hacia la posicion del cañon
        efectoImpacto.transform.rotation = Quaternion.LookRotation(direccion);
    }

    /// <summary>
    /// Función que llamamos para realizar el daño durante el disparo del laser
    /// </summary>
    void DañoLaser() {
        objetivoEnemy.RecibirDaño(dañoEnTiempo * Time.deltaTime);
    }

    /// <summary>
    /// Dibujamos Gizmos del GameObject seleccionado
    /// </summary>
    void OnDrawGizmosSelected() {
        // Elegimos el color del Gizmo
        Gizmos.color = Color.red;
        // Dibujamos las lineas de una esfera desde la posicion de la torreta y como radio el rango que tenemos establecido de disparo
        Gizmos.DrawWireSphere(transform.position, rango);
    }
}
