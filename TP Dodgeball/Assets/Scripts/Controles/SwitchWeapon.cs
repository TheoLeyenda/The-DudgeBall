using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SwitchWeapon : MonoBehaviour {

    // Use this for initialization
    public Material Black;
    public Material Ice;
    public Material Normal;
    public Material Fragment;
    public Material Dancer;
    public Material Fire;
    public GameObject GeneratorExplocive;
    public GameObject GeneratorCommonBall;
    public bool JugadorWindows;

    private Renderer ren;

	void Start () {

        gameObject.GetComponent<Renderer>().material = Normal;
	}
	
	// Update is called once per frame
	void Update () {
        if (JugadorWindows)
        {
            CheckWeapon();
        }
        if(Player.GetPlayer().ballType == 1 && Normal != null)
        {
            GeneratorExplocive.SetActive(false);
            GeneratorCommonBall.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Normal;
        }
        if(Player.GetPlayer().ballType == 2 && Ice != null)
        {
            GeneratorExplocive.SetActive(false);
            GeneratorCommonBall.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Ice;
        }
        if(Player.GetPlayer().ballType == 3 && Fragment != null)
        {
            GeneratorExplocive.SetActive(false);
            GeneratorCommonBall.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Fragment;
        }
        if(Player.GetPlayer().ballType == 4 && Dancer != null)
        {
            GeneratorExplocive.SetActive(false);
            GeneratorCommonBall.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Dancer;
        }
        if (Player.GetPlayer().ballType == 5 && Fire != null)
        {
            GeneratorExplocive.SetActive(false);
            GeneratorCommonBall.SetActive(true);
            gameObject.GetComponent<Renderer>().material = Fire;
        }
        if(Player.GetPlayer().ballType == 6 && GeneratorExplocive != null)
        {
            GeneratorCommonBall.SetActive(false);
            gameObject.GetComponent<Renderer>().material = Black;
            GeneratorExplocive.SetActive(true);
            
        }
    }
    public void CheckWeapon()
    {
        if(Input.GetKey(KeyCode.Alpha1))
        {
            Player.GetPlayer().ballType = 1;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            Player.GetPlayer().ballType = 2;
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Player.GetPlayer().ballType = 3;
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            Player.GetPlayer().ballType = 4;
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            Player.GetPlayer().ballType = 5;
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            Player.GetPlayer().ballType = 6;
        }
    }
}
