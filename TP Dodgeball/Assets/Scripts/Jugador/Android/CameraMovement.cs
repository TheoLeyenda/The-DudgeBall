using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    // Use this for initialization
    public float speed;
    public GameObject player;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float horRot = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        float verRot = -Input.GetAxis("Mouse Y") * speed * Time.deltaTime;

        if(verRot > 0)
        {
            transform.Rotate(0, speed, 0);
        }
    }
}
