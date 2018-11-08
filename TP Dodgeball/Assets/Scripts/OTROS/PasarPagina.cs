using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasarPagina : MonoBehaviour {

    // Use this for initialization
    public GameObject[] paginas;
    private int id;
	void Start () {
        id = 0;
		for(int i = 0; i< paginas.Length; i++)
        {
            paginas[i].SetActive(false);
        }
	}

    // Update is called once per frame
    void Update() {
        MostrarPagina();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SiguientePagina();
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            AnteriorPagina();
        }
	}
    public void SiguientePagina()
    {
        if (id < paginas.Length)
        {
            id++;
        }
    }
    public void AnteriorPagina()
    {
        if (id > 0)
        {
            id--;
        }
    }
    public void MostrarPagina()
    {
        for(int i = 0; i< paginas.Length; i++)
        {
            if(i == id)
            {
                paginas[i].SetActive(true);
            }
            else
            {
                paginas[i].SetActive(false);
            }
        }
    }
}
