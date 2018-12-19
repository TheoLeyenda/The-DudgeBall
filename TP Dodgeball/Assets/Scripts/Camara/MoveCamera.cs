using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    // Use this for initialization
    public GameObject _camera;
    private float x;
    private float y;
    private float z;
    private Vector3 dir;
	void Start () {
        x = _camera.transform.position.x;
        y = _camera.transform.position.y;
        z = _camera.transform.position.z;
        dir = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}
    public void Move()
    {
        if(Input.GetKey(KeyCode.S))
        {
            y++;
        }
        if(Input.GetKey(KeyCode.W))
        {
            y--;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            x--;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            x++;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            z++;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            z--;
        }
        dir = new Vector3(x, y, z);
        _camera.transform.position = dir;
    }
    public void Up()
    {
        y++;
        dir = new Vector3(x, y, z);
        _camera.transform.position = dir;
    }
    public void Down()
    {
        y--;
        dir = new Vector3(x, y, z);
        _camera.transform.position = dir;
    }
    public void MoveForward()
    {
        z++;
        dir = new Vector3(x, y, z);
        _camera.transform.position = dir;
    }
    public void MoveBack()
    {
        z--;
        dir = new Vector3(x, y, z);
        _camera.transform.position = dir;
    }
    public void MoveLeft()
    {
        x--;
        dir = new Vector3(x, y, z);
        _camera.transform.position = dir;
    }
    public void MoveRight()
    {
        x++;
        dir = new Vector3(x, y, z);
        _camera.transform.position = dir;
    }
}


