using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barril : MonoBehaviour {

    // Use this for initialization
    public PuertaPuzle puertaPuzle;
    private bool unaVez;
	void Start () {
        unaVez = true;
	}
    private void OnDisable()
    {
        if (unaVez)
        {
            puertaPuzle.sumarBarrilDerribado();
            //unaVez = false;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MiniPelota")
        {
            gameObject.SetActive(false);
        }
    }
}
