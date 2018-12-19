using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PricePlate : MonoBehaviour {

	// Use this for initialization
    private Animation ANIMATION;
    public AnimationClip AnimationClip;
    public GameObject[] offObjects;
    public Enemy enemy;
    public BarsDoor door;
    public bool checkOpenDoor;
    public bool activateByCube;
    public bool activateByPlayer;
    private bool once;
	void Start () {
        ANIMATION = GetComponent<Animation>();
        if (enemy != null)
        {
            enemy.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(checkOpenDoor)
        {
            if (enemy != null)
            {
                if (enemy.life <= 0)
                {
                    if(door != null && !once)
                    {
                        door.OpenDoor();
                        once = true;
                    }
                }
            }
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (activateByCube)
        {
            if (collision.gameObject.tag == "CuboActivador")
            {
                ANIMATION.clip = AnimationClip;
                ANIMATION.Play();
                for (int i = 0; i < offObjects.Length; i++)
                {
                    offObjects[i].SetActive(false);
                }
                if (enemy != null)
                {
                    enemy.gameObject.SetActive(true);
                }
            }
        }
        if(activateByPlayer)
        {
            if (collision.gameObject.tag == "Player")
            {
                ANIMATION.clip = AnimationClip;
                ANIMATION.Play();
                for (int i = 0; i < offObjects.Length; i++)
                {
                    offObjects[i].SetActive(false);
                }
                if (enemy != null)
                {
                    enemy.gameObject.SetActive(true);
                }
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        
        ANIMATION.transform.localScale = new Vector3(transform.localScale.x, 0.1f, transform.localScale.z);

    }
}
