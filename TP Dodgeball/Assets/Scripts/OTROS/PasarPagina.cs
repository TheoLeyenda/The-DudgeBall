using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class PasarPagina : MonoBehaviour {

    // Use this for initialization
    public GameObject[] pages;
    private int id;
	void Start () {
        id = 0;
		for(int i = 0; i< pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
	}

    // Update is called once per frame
    void Update() {
        DrawPages();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            NextPages();
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            BackPages();
        }
	}
    public void NextPages()
    {
        if (id < pages.Length)
        {
            id++;
        }
    }
    public void BackPages()
    {
        if (id > 0)
        {
            id--;
        }
    }
    public void DrawPages()
    {
        for(int i = 0; i< pages.Length; i++)
        {
            if(i == id)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
