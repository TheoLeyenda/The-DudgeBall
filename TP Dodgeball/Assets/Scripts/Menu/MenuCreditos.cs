using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCreditos : MonoBehaviour {

    // Use this for initialization
    public GameObject JuegoCompletado;
    public GameObject Creditos;
    public float Velocidad;
    private float y;
    public float tiempoSubida;
    void Start() {
        y = Creditos.transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        if (Creditos.activeSelf == true)
        {
            MoverVertical();
        }
    }
    public void CreditosMenu()
    {
        Creditos.SetActive(true);
        JuegoCompletado.SetActive(false);
    }
    public void MoverVertical()
    {
        if (tiempoSubida > 0)
        {
            y = y + Time.deltaTime * Velocidad;
            Creditos.transform.position = new Vector3(Creditos.transform.position.x, y, Creditos.transform.position.z);
            tiempoSubida = tiempoSubida - Time.deltaTime;
        }
    }
}
