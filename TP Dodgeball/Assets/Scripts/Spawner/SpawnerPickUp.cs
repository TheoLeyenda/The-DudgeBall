using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class SpawnerPickUp : MonoBehaviour {

    // Use this for initialization
    public GameObject[] spawnersPickUps;
    private int pickUpsType;
    public float dileyActived;
    public float AuxDileyActived;
	void Start () {
        dileyActived = 0;
		for(int i = 0; i<spawnersPickUps.Length; i++)
        {
            spawnersPickUps[i].gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(dileyActived > 0)
        {
            dileyActived = dileyActived - Time.deltaTime;
        }
        if (Jugador.GetPlayer() != null)
        {
            if (dileyActived <= 0 )
            {
                pickUpsType = Random.Range(0, 4);
                if (pickUpsType == 0)
                {
                    spawnersPickUps[4].gameObject.SetActive(false);
                    spawnersPickUps[3].gameObject.SetActive(false);
                    spawnersPickUps[2].gameObject.SetActive(false);
                    spawnersPickUps[1].gameObject.SetActive(false);
                    spawnersPickUps[0].gameObject.SetActive(true);
                }
                if (pickUpsType == 1)
                {
                    spawnersPickUps[4].gameObject.SetActive(false);
                    spawnersPickUps[3].gameObject.SetActive(false);
                    spawnersPickUps[2].gameObject.SetActive(false);
                    spawnersPickUps[1].gameObject.SetActive(true);
                    spawnersPickUps[0].gameObject.SetActive(false);
                }
                if (pickUpsType == 2)
                {
                    spawnersPickUps[4].gameObject.SetActive(false);
                    spawnersPickUps[3].gameObject.SetActive(false);
                    spawnersPickUps[2].gameObject.SetActive(true);
                    spawnersPickUps[1].gameObject.SetActive(false);
                    spawnersPickUps[0].gameObject.SetActive(false);
                }
                if (pickUpsType == 3)
                {
                    spawnersPickUps[4].gameObject.SetActive(false);
                    spawnersPickUps[3].gameObject.SetActive(true);
                    spawnersPickUps[2].gameObject.SetActive(false);
                    spawnersPickUps[1].gameObject.SetActive(false);
                    spawnersPickUps[0].gameObject.SetActive(false);
                }
                if (pickUpsType == 4)
                {
                    //spawnersPickUps[4].gameObject.SetActive(true);
                    spawnersPickUps[3].gameObject.SetActive(false);
                    spawnersPickUps[2].gameObject.SetActive(false);
                    spawnersPickUps[1].gameObject.SetActive(false);
                    spawnersPickUps[0].gameObject.SetActive(false);
                }
                dileyActived = AuxDileyActived;
                
            }
        }
        
	}
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)