using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class WaveManager : MonoBehaviour {

    [Header("VARIABLES DE CONTROL - TEMPORALES")]
    public int enemigosVivos;
    public int _oledaActual;
    public int _oleadasLenth;

    public static int EnemigosVivos = 0;            // Variable que usaremos para almacenar el numero de enemigos que salen en cada oleada

    [Header("Unity Objects")]
    public Wave[] oleadas;                          // Diferentes oleadas 
    public Transform puntoDeSalida;                 // Punto inicial donde apareceran los enemigos
    public GameManager gameManager;                 // Objeto publico de GameManager

    [Header("Contadores de tiempo")]
    public float tiempoOledas = 5f;                 // Tiempo entre 
    public float tiempoAtrasOleadas = 2f;           // Cuenta atras para oleadas 
    public float tiempoEntreEnemigos = 0.5f;        // TIempo entre enemigos

    [Header("UI")]
    public Text tiempoOleadasUI;                    // Variable tipo Texto con el contador entre oleadas
    public Text enemigosVivosTexto;
    public Text oleadasTexto;
    
    private int oleadaActual = 0;                   // Numero de oleada


    void Start() {
        oleadaActual = 0;
        EnemigosVivos = 0;
    }

    // Update is called once per frame
    void Update() {

        //VARIABLES DE CONTROL - TEMPORALES
        enemigosVivos = EnemigosVivos;
        _oledaActual = oleadaActual;
        _oleadasLenth = oleadas.Length;
        //=================================

        enemigosVivosTexto.text = EnemigosVivos.ToString();
        oleadasTexto.text = oleadaActual + " / " + oleadas.Length;

        if (EnemigosVivos > 0) {
            return;
        }
        // Si la cuenta atras inicial es menor o igual a 0
        if (tiempoAtrasOleadas <= 0f) {
            // Llamamos Spawneamos la oleada e igualamos la cuenta atras al tiempo entre oleadas.
            StartCoroutine(GenerarOleada());
            tiempoAtrasOleadas = tiempoOledas;
            // Si la oleada actual es igual al tamaño de la oleada del nivel
            // y el numero de enemigos de la escena es 0, ganamos la ronda
            if (oleadaActual == oleadas.Length && EnemigosVivos <= 0) {
                gameManager.GanarNivel();
                this.enabled = false;
            }
            return;
        }
        // Se le resta al contador el Time.deltaTime
        tiempoAtrasOleadas -= Time.deltaTime;

        tiempoAtrasOleadas = Mathf.Clamp(tiempoAtrasOleadas, 0f, Mathf.Infinity);
        // Ponemos el tiempo que queda entre oleada redondeado a int
        tiempoOleadasUI.text = string.Format("{0:00.00}", tiempoAtrasOleadas);
    }
    // =========================================
    /// <summary>
    /// Lanzamos las oleadas
    /// </summary>
    IEnumerator GenerarOleada() {
        
        Wave oleada = oleadas[oleadaActual];
        
        // Spawneamos 1 enemigo mas en cada oleada
        for (int i = 0; i < oleada.contador; i++) {

            GenerarEnemigo(oleada.enemigoPrefab);
            // Esperamos para generar enemigos antes de repetir o salir del for
            yield return new WaitForSeconds(1f / oleada.rate);
        }
        PlayerManager.Rondas++;
        oleadaActual++;
    }
    
    // =========================================
    /// <summary>
    /// Lanzamos los enemigos
    /// </summary>
    void GenerarEnemigo(GameObject prefabEnemigo) {
        // Lanzamos un enemigo en la posicion de spawn y la rotacion de este
        Instantiate(prefabEnemigo, puntoDeSalida.position, puntoDeSalida.rotation);
        EnemigosVivos++;
    }
}
