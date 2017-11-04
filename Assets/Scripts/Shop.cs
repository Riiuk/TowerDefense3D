using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint torretaEstandar;
    public TurretBlueprint torretaMisiles;
    public TurretBlueprint torretaLaser;
    public TurretBlueprint torretaHielo;

    BuildManager buildManager;

    void Start() {
        buildManager = BuildManager.BM;     
    }

    public void SeleccionarTorretaEstandar() {
        Debug.Log("Torreta Estandar Seleccionada");
        buildManager.SeleccionarTorretaParaConstruir(torretaEstandar);
    }

    public void SeleccionarTorretaMisiles() {
        buildManager.SeleccionarTorretaParaConstruir(torretaMisiles);
        Debug.Log("Torreta de Misiles Seleccionada");
    }

    public void SeleccionarTorretaLaser() {
        buildManager.SeleccionarTorretaParaConstruir(torretaLaser);
        Debug.Log("Torreta Laser Seleccionada");
    }

    public void SeleccionarTorretaHielo() {
        buildManager.SeleccionarTorretaParaConstruir(torretaHielo);
        Debug.Log("Torreta Hielo Seleccionada");
    }
}
