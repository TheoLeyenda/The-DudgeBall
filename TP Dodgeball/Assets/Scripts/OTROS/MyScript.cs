﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class MyScript : MonoBehaviour {

    // Use this for initialization
    public FixedJoystick MoveJoystick;
    public FixedButton JumpButton;
    public FixedTouchField TouchField;
	void Start () {
	}

    // Update is called once per frame
    void Update()
    {
        if (MoveJoystick.gameObject.activeSelf == true)
        {
            var fps = GetComponent<RigidbodyFirstPersonController>();
            fps.RunAxis = MoveJoystick.inputVector;
            fps.JumpAxis = JumpButton.Pressed;
            fps.mouseLook.LookAxis = TouchField.TouchDist;
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
