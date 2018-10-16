using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorSpawnerPickUp : MonoBehaviour {

    // Use this for initialization
    public SpawnerPickUps[] spawnersPickUps;
    private int tipoPickUp;
    public float dileyActivacion;
    public float AuxDileyActivacion;
	void Start () {
        dileyActivacion = 0;
		for(int i = 0; i<spawnersPickUps.Length; i++)
        {
            spawnersPickUps[i].gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(dileyActivacion > 0)
        {
            dileyActivacion = dileyActivacion - Time.deltaTime;
        }
        if (Jugador.GetJugador() != null)
        {
            if (dileyActivacion <= 0 && Jugador.GetJugador().contar)
            {
                tipoPickUp = Random.Range(1, 5);
                if (tipoPickUp == 1)
                {
                    spawnersPickUps[4].gameObject.SetActive(false);
                    spawnersPickUps[3].gameObject.SetActive(false);
                    spawnersPickUps[2].gameObject.SetActive(false);
                    spawnersPickUps[1].gameObject.SetActive(false);
                    spawnersPickUps[0].gameObject.SetActive(true);
                }
                if (tipoPickUp == 2)
                {
                    spawnersPickUps[4].gameObject.SetActive(false);
                    spawnersPickUps[3].gameObject.SetActive(false);
                    spawnersPickUps[2].gameObject.SetActive(false);
                    spawnersPickUps[1].gameObject.SetActive(true);
                    spawnersPickUps[0].gameObject.SetActive(false);
                }
                if (tipoPickUp == 3)
                {
                    spawnersPickUps[4].gameObject.SetActive(false);
                    spawnersPickUps[3].gameObject.SetActive(false);
                    spawnersPickUps[2].gameObject.SetActive(true);
                    spawnersPickUps[1].gameObject.SetActive(false);
                    spawnersPickUps[0].gameObject.SetActive(false);
                }
                if (tipoPickUp == 4)
                {
                    spawnersPickUps[4].gameObject.SetActive(false);
                    spawnersPickUps[3].gameObject.SetActive(true);
                    spawnersPickUps[2].gameObject.SetActive(false);
                    spawnersPickUps[1].gameObject.SetActive(false);
                    spawnersPickUps[0].gameObject.SetActive(false);
                }
                if (tipoPickUp == 5)
                {
                    spawnersPickUps[4].gameObject.SetActive(true);
                    spawnersPickUps[3].gameObject.SetActive(false);
                    spawnersPickUps[2].gameObject.SetActive(false);
                    spawnersPickUps[1].gameObject.SetActive(false);
                    spawnersPickUps[0].gameObject.SetActive(false);
                }
                dileyActivacion = AuxDileyActivacion;
                Jugador.GetJugador().contar = false;
            }
        }
        
	}
}
