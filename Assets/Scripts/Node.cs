using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color colorSobreNodo;    // Color del que se colorea el GameObject cuando pasamos el raton por encima
    public Color colorNoConstruir;  // Color que usamos para mostrar los nodos ocupados
    public Vector3 offsetPosicion;  // Vector3 de posicion del spawn de la torreta

    
    public GameObject torreta;     // Objeto de torreta
    [HideInInspector]
    public TurretBlueprint torretaMejorada;
    [HideInInspector]
    public bool estaMejorada = false;

    private Renderer render;        // Componente Render del nodo
    private Color colorInicial;     // Color inicial del GameObject

    BuildManager buildManager;      // Variable de BuildManager para poder ser llamado desde otro sitio

    void Start() {

        buildManager = BuildManager.BM;
        render = GetComponent<Renderer>(); 
        // Asignamos el color inicial a una variable
        colorInicial = render.material.color;
    }

    /// <summary>
    /// Función de tipo Vector3 la cual la llenamos con la posición donde se debe construir la torreta
    /// </summary>
    /// <returns></returns>
    public Vector3 ConseguirPosicion() {
        return transform.position + offsetPosicion;
    }

    /// <summary>
    /// Función que llamamos cuando queramos construir una torreta sobre el nodo seleccionado
    /// </summary>
    /// <param name="torretaBlueprint"></param>
    void ConstruirTorreta(TurretBlueprint torretaBlueprint) {
        // Si nuestro Dinero actual es menor que el coste de la torreta a construir, nos muetra un aviso
        if (PlayerManager.Dinero < torretaBlueprint.coste) {
            Debug.Log("¡No hay suficiente Dinero! - TODO: Mostrar en pantalla");
            return;
        }

        // Si por el contrario, tenemos Dinero, nos lo resta de nuestro total
        PlayerManager.Dinero -= torretaBlueprint.coste;
        // Instanciamos la torreta preseleccionada como un nuevo GameObject
        GameObject _torreta = (GameObject)Instantiate(torretaBlueprint.prefab, ConseguirPosicion(), Quaternion.identity);
        // Asignamos este GameObject al que tiene el Nodo, para así colocarla
        torreta = _torreta;

        torretaMejorada = torretaBlueprint;

        GameObject efecto = (GameObject)Instantiate(buildManager.efectoConstruccion, ConseguirPosicion(), Quaternion.identity);
        Destroy(efecto, buildManager.timepoDeDestruccion);
        // Sacamos un aviso por consola de ok y el Dinero que nos queda.
        Debug.Log("¡Torreta construida!");
    }

    /// <summary>
    /// Función que llamamos para mejorar la torreta
    /// </summary>
    public void MejorarTorreta() {
        // Si nuestro Dinero actual es menor que el coste de la torreta a construir, nos muetra un aviso
        if (PlayerManager.Dinero < torretaMejorada.costeMejora) {
            Debug.Log("¡No hay suficiente Dinero! - TODO: Mostrar en pantalla");
            return;
        }

        // Si por el contrario, tenemos Dinero, nos lo resta de nuestro total
        PlayerManager.Dinero -= torretaMejorada.costeMejora;
        // Destruimos la torreta por defecto
        Destroy(torreta);
        // Instanciamos la torreta preseleccionada como un nuevo GameObject
        GameObject _torreta = (GameObject)Instantiate(torretaMejorada.prefabMejora, ConseguirPosicion(), Quaternion.identity);
        // Asignamos este GameObject al que tiene el Nodo, para así colocarla
        torreta = _torreta;

        GameObject efecto = (GameObject)Instantiate(buildManager.efectoConstruccion, ConseguirPosicion(), Quaternion.identity);
        Destroy(efecto, buildManager.timepoDeDestruccion);

        estaMejorada = true;
        // Sacamos un aviso por consola de ok y el Dinero que nos queda.
        Debug.Log("¡Torreta mejorada!");
    }

    /// <summary>
    /// Función para vender una torreta
    /// </summary>
    public void VenderTorreta() {

        // Nos añadimos el dinero
        if (estaMejorada) {
            PlayerManager.Dinero += torretaMejorada.costeMejora / 2 + torretaMejorada.coste / 2;
        } else {
            PlayerManager.Dinero += torretaMejorada.coste / 2;
        }

        // Destruimos la torreta
        Destroy(torreta);
        // Instanciamos el efecto de particulas de vender
        GameObject efectosVenta = Instantiate(buildManager.efectoVenta, transform.position, Quaternion.identity);
        // Destruimos este efecto pasado un tiempo específico
        Destroy(efectosVenta, buildManager.timepoDeDestruccion);
        // Desseleccionamos la torreta del nodo
        torretaMejorada = null;
        // Ponemos que la torreta esta mejorada en false, para poder mejorar otra que pongamos en esa posicion
        estaMejorada = false;
    }

    /// <summary>
    /// Cuando hacemos clic con el raton
    /// </summary>
    void OnMouseDown() {

        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        // Si torreta no es null
        if (torreta != null) {
            buildManager.SeleccionarNodo(this);
            return;
        }

        // Si la funcion ObtenerTorretaParaConstruir es nula, salimos de la función actual
        if (!buildManager.PuedoConstruir)
            return;

        ConstruirTorreta(buildManager.ConseguirTorretaParaConstruir());

        /*
        // Creamos un GameObject con la torreta para construir, que sacamos del BuildManager
        GameObject torretaParaConstruir = buildManager.ObtenerTorretaParaConstruir();
        // Asignamos al GameObject torreta e instanciandolo la torreta para construir, en la posicion y rotacion del nodo y
        // con una posicion vertical asiganada por el inspector
        torreta = (GameObject)Instantiate(torretaParaConstruir, transform.position + offsetPosicion, transform.rotation);
        */
    }

    /// <summary>
    /// Cuanto pasamos el raton sobre el GameObject
    /// </summary>
    void OnMouseEnter() {

        if (EventSystem.current.IsPointerOverGameObject())
        
        // Si puedo construir y el Dinero actual del jugador es menor que el coste de la torreta seleccionada
        if (buildManager.PuedoConstruir && !buildManager.TengoDinero) {
            //Cambiamos el color del material al de no construir y salimos de la funcion, dejando así el color hasta que la llamemos de nuevo
            render.material.color = colorNoConstruir;
            return;
        }
        //Si tenemos una torreta seleccionada y entramos en un nodo que ya tiene una torreta construida
        if (buildManager.PuedoConstruir && torreta != null) {
            // Cambiamos el color del material al de no construir y salimos de la funcion
            render.material.color = colorNoConstruir;
            return;
        }
        // Si la funcion ObtenerTorretaParaConstruir es nula, salimos de la función actual
        if (!buildManager.PuedoConstruir) {
            return;
        }
        render.material.color = colorSobreNodo;

    }

    /// <summary>
    /// Cuando sacamos el raton del GameObject
    /// </summary>
    void OnMouseExit() {
        // Asignamos el color del GameObject al color que asignamos previamente como inicial
        render.material.color = colorInicial;    
    }
}
