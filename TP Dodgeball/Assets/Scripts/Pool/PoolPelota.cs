﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class PoolPelota : MonoBehaviour {

    public GameObject Ball;
    private List<GameObject> CommonBalls;
    public int count;
    private int id;
    private bool substractValuesBalls;
    // Use this for initialization
    void Awake()
    {
        CommonBalls = new List<GameObject>();
        for(int i = 0; i< count; i++)
        {
            GameObject go = Instantiate(Ball);
            PoolObject po;
            go.SetActive(false);
            CommonBalls.Add(go);
            po = go.AddComponent<PoolObject>();
            po.pool = this;
        }
        id = 0;
    }

    // Update is called once per frame
    void Update () {
    }
    public List<GameObject> GetListPelotasComunes()
    {
        return CommonBalls;
    }
    public GameObject GetObject()
    {
        GameObject go = CommonBalls[id];
        go.SetActive(true);
        id++;
        return go;
    }
    public void Recycle(GameObject go)
    {
        id--;
        go.SetActive(false);
        CommonBalls[id] = go;
    }
    public void SetId(int _id)
    {
        id = _id;
    }
    public void SubstractId()
    {
        id = id - 1;
    }
    public void AddId()
    {
        id = id + 1;
    }
    public int GetId()
    {
        return id;
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)