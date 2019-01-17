using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAndroid : MonoBehaviour {

    // Use this for initialization
    public GameObject player;
    public Rigidbody rig;
    private Vector3 dir;
    public float speed;
    private bool moveForward;
    private bool moveBack;
    private bool moveLeft;
    private bool moveRight;
    private bool rotateLeft;
    private bool rotateRight;
    private float x;
    private float z;
	void Start () {
        x = player.transform.position.x;
        z = player.transform.position.z;
        dir = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
		if(moveForward)
        {
            MoveForward();
        }
        if(moveBack)
        {
            MoveBack();
        }
        if(moveLeft)
        {
            MoveLeft();
        }
        if(moveRight)
        {
            MoveRight();
        }
        if(rotateLeft)
        {
            RotateLeft();
        }
        if(rotateRight)
        {
            RotateRight();
        }
	}
    public void SetMoveForward(bool _forward)
    {
        moveForward = _forward;
    }
    public void SetMoveBack(bool _back)
    {
        moveBack = _back;
    }
    public void SetMoveLeft(bool _left)
    {
        moveLeft = _left;
    }
    public void SetMoveRight(bool _right)
    {
        moveRight = _right;
    }
    public void SetRotationLeft(bool _rotationLeft)
    {
        rotateLeft = _rotationLeft;
    }
    public void SetRotationRight(bool _rotationRight)
    {
        rotateRight = _rotationRight;
    }
    public void MoveForward()
    {
        rig.velocity = Vector3.zero;
        z = z + Time.deltaTime * speed;
        rig.velocity = new Vector3(transform.position.x, transform.position.y, z);

    }
    public void MoveBack()
    {
        rig.velocity = Vector3.zero;
        z = z - Time.deltaTime * speed;
        rig.velocity = new Vector3(transform.position.x, transform.position.y, z);
    }
    public void MoveLeft()
    {
        rig.velocity = Vector3.zero;
        x = x - Time.deltaTime * speed;
        rig.velocity = new Vector3(x, transform.position.y, transform.position.z);
    }
    public void MoveRight()
    {
        x = x + Time.deltaTime * speed;
        rig.velocity = new Vector3(x, transform.position.y, transform.position.z);
       
    }
    public void RotateRight()
    {
        transform.Rotate(0, 90, 0);
    }
    public void RotateLeft()
    {
        transform.Rotate(0, -90, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pared")
        {
            moveForward = false;
            moveBack = false;
            moveLeft = false;
            moveRight = false;
        }
    }
}
