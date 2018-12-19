using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {

    // Use this for initialization
    public PuzzleDoor PuzleDoor;
    private bool once;
	void Start () {
        once = true;
	}
    private void OnDisable()
    {
        if (once)
        {
            PuzleDoor.AddBarrelDown();
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