using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolPelota : MonoBehaviour {

    public GameObject Pelota;
    private List<GameObject> PelotasComunes;
    public int count;
    private int id;
    private bool resetarValoresPelota;
    // Use this for initialization
    void Awake()
    {
        PelotasComunes = new List<GameObject>();
        for(int i = 0; i< count; i++)
        {
            GameObject go = Instantiate(Pelota);
            PoolObject po;
            go.SetActive(false);
            PelotasComunes.Add(go);
            po = go.AddComponent<PoolObject>();
            po.pool = this;
        }
        id = 0;
    }

    // Update is called once per frame
    void Update () {
    }
    public GameObject GetObject()
    {
        GameObject go = PelotasComunes[id];
        go.SetActive(true);
        id++;
        return go;
    }
    public void Recycle(GameObject go)
    {
        id--;
        go.SetActive(false);
        PelotasComunes[id] = go;
    }
    public void SetId(int _id)
    {
        id = _id;
    }
    public void RestarId()
    {
        id = id - 1;
    }
    public void SumarId()
    {
        id = id + 1;
    }
}
