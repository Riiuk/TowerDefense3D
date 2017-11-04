using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [Header("Atributos")]
    public int daño = 50;
    public float velocidad = 70f;           // Velocidad de movimiento de la bala
    public float radioExplosion = 0f;       // Radio de efecto del proyectil
    public float delayDestruccion = 2f;     // Variable que usaremos para calcular el tiempo que vamos a tardar en destruir el objeto
    public GameObject efectoImpactoBala;    // Objeto de Particulas de impacto
    public string tagEnemigo = "Enemigo";               // Asignamos el Tag que buscaremos al calcular el radio de impacto
    
    private Transform objetivo;             // Transform del objetivo de la bala

    /// <summary>
    /// Funcion que usamos para asignar el objetivo a la bala
    /// </summary>
    /// <param name="_objetivo"></param>
    public void Buscar(Transform _objetivo) {
        objetivo = _objetivo;
    }
	
	// Update is called once per frame
	void Update () {
        // Si el objetivo es null, destrulle la bala
        if (objetivo == null) {
            Destroy(gameObject);
            return;
        }
        // Llama a mover la bala
        MovimientoBala();

        
	}

    /// <summary>
    /// Damos movimiento a la bala
    /// </summary>
    void MovimientoBala() {
        // Creamos un Vector3 con la direccion hacia el objetivo
        Vector3 direccion = objetivo.position - transform.position;
        // Calculamos la velocidad de la bala en cada frame
        float distanciaEsteFrame = Time.deltaTime * velocidad;
        // Si la posicion de la bala es menor que la velocidad entonces impacta
        if (direccion.magnitude <= distanciaEsteFrame) {
            // Llamamos a impactar la bala
            Impacto();
            return;
        }

        // Movemos la bala desde nuestra posicion, hacia la del objetivo normalizada segun la posicion del mundo
        transform.Translate(direccion.normalized * distanciaEsteFrame, Space.World);
        // Le decimos al objeto que siempre mire al objetivo
        transform.LookAt(objetivo.transform);
    }

    /// <summary>
    /// Impactamos sobre el objetivo
    /// </summary>
    void Impacto() {
        // Instanciamos como un nuevo objeto el ParticleSystem de efecto de impacto
        GameObject instanciaObj = (GameObject)Instantiate(efectoImpactoBala, transform.position, transform.rotation);
        // Destruimos el efecto de particulas con un delay
        Destroy(instanciaObj, delayDestruccion);

        if (radioExplosion > 0f) {
            // Si el radio de explosion esta asignado, llamamos a la funcion que calcula el radio de explosion
            Explosion();
        } else {
            // Si no, simplemente hacemos daño al objetivo de la bala
            RealizarDaño(objetivo);
        }
        
        // Destruimos la bala
        Destroy(gameObject);
    }

    /// <summary>
    /// Función que calculará el daño en area
    /// </summary>
    void Explosion() {
        // Creamos un array de colliders con todos los que encontremos en el radio de la explosion cuando impactemos en el objetivo
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioExplosion);

        foreach (Collider collider in colliders) {
            if (collider.tag == tagEnemigo) {
                RealizarDaño(collider.transform);
            }
        }
    }

    /// <summary>
    /// Función que calculara el daño a un solo enemigo
    /// </summary>
    /// <param name="enemigo"></param>
    void RealizarDaño(Transform enemigo) {
        Enemy e = enemigo.GetComponent<Enemy>();
        if (e != null) {
            e.RecibirDaño(daño);
        }
    }

    /// <summary>
    /// Dibujamos un Gizmo igual al radio de explosion
    /// </summary>
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioExplosion);    
    }
}
