using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [Header("Atributos")]
    [Tooltip("Velocidad de movimiento")]
    public float velocidadInicial = 10f;
    [HideInInspector]
    public float velocidad;
    [Tooltip("Vida del enemigo")]
    public float vida = 100f;
    [Tooltip("Coste de construcción")]
    public int valor = 50;

    [Header("Elementos UI")]
    [Tooltip("Objeto tipo imagen de la barra de vida")]
    public Image barraVida;

    [Header("Efectos")]
    [Tooltip("Efecto de particulas de muerte")]
    public GameObject efectoMuerte;
    [Tooltip("Tiempo que tarda en destruirse el efecto de muerte")]
    public float duracionEfectoMuerte;

    private float vidaInicial;

    void Start() {
        velocidad = velocidadInicial;
        vidaInicial = vida;
    }

    public void RecibirDaño(float cantidad) {
        vida -= cantidad;

        barraVida.fillAmount = vida / vidaInicial;

        if (vida <= 0) {
            Muerte();
        }
    }

    public void Frenar(float porcentaje) {
        velocidad = velocidad * (1f - porcentaje);
    }

    /// <summary>
    /// Funcion que ejecuta la muerte del enemigo
    /// </summary>
    void Muerte() {
        // Nos damos el dinero que cuesta el enemigo
        PlayerManager.Dinero += valor;
        // Instanciamos el efecto de muerte
        GameObject efecto = (GameObject)Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        // Destruimos el efecto de particulas despues de unos segundos
        Destroy(efecto, duracionEfectoMuerte);
        // Destruimos el objeto
        Destroy(gameObject);
        // Restamos uno al contador de enemigos vivos en la escena 
        WaveManager.EnemigosVivos--;
    }
}
