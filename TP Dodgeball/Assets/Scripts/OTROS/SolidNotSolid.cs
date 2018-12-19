using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidNotSolid : MonoBehaviour {

    // Use this for initialization
    public bool makeSolid;
    private bool once = true;
    public GameObject[] objects;
    public void Solidify(GameObject[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] != null)
            {
                if (obj[i].GetComponent<BoxCollider>() != null)
                {
                    obj[i].GetComponent<BoxCollider>().isTrigger = false;
                }
                else if (obj[i].GetComponent<CapsuleCollider>() != null)
                {
                    obj[i].GetComponent<CapsuleCollider>().isTrigger = false;
                }
                else if (obj[i].GetComponent<SphereCollider>() != null)
                {
                    obj[i].GetComponent<SphereCollider>().isTrigger = false;
                }
                else if (obj[i].GetComponent<MeshCollider>() != null)
                {
                    obj[i].GetComponent<MeshCollider>().isTrigger = false;
                }
                else if (obj[i].GetComponent<TerrainCollider>() != null)
                {
                    obj[i].GetComponent<TerrainCollider>().isTrigger = false;
                }
                else if (obj[i].GetComponent<WheelCollider>() != null)
                {
                    obj[i].GetComponent<WheelCollider>().isTrigger = false;
                }
            }
        }
    }
    public void Desolidify(GameObject[] obj)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i] != null)
            {
                if (obj[i].GetComponent<BoxCollider>() != null)
                {
                    obj[i].GetComponent<BoxCollider>().isTrigger = true;
                }
                else if (obj[i].GetComponent<CapsuleCollider>() != null)
                {
                    obj[i].GetComponent<CapsuleCollider>().isTrigger = true;
                }
                else if (obj[i].GetComponent<SphereCollider>() != null)
                {
                    obj[i].GetComponent<SphereCollider>().isTrigger = true;
                }
                else if (obj[i].GetComponent<MeshCollider>() != null)
                {
                    obj[i].GetComponent<MeshCollider>().isTrigger = true;
                }
                else if (obj[i].GetComponent<TerrainCollider>() != null)
                {
                    obj[i].GetComponent<TerrainCollider>().isTrigger = true;
                }
                else if (obj[i].GetComponent<WheelCollider>() != null)
                {
                    obj[i].GetComponent<WheelCollider>().isTrigger = true;
                }
            }
        }
    }
    private void OnDisable()
    {
        if (once)
        {
            if (makeSolid)
            {
                Solidify(objects);
            }
            else
            {
                Desolidify(objects);
            }
            once = false;
        }
    }
}