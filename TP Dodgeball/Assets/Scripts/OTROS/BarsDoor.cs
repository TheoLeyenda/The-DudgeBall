using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsDoor : MonoBehaviour {

    // Use this for initialization
    private Animation animationDoor;
    public AnimationClip animationClip;
    public float speed;
    private bool checkDestroy;
    private bool ActivateOnce;
    public float timeMov;
    private bool closeDoor;
    private bool openDoor;
    private float y;
    private float auxTimeMov;
    public GameObject[] ActivateObjects;
    void Start ()
    {
        y = transform.position.y;
        auxTimeMov = timeMov;
        animationDoor = GetComponent<Animation>();
        if (animationClip != null && animationDoor != null)
        {
            animationDoor.clip = animationClip;
        }
    }

    // Update is called once per frame
    private void OnDisable()
    {
        if (!ActivateOnce)
        {
            for (int i = 0; i < ActivateObjects.Length; i++)
            {
                if (ActivateObjects[i] != null)
                {
                    ActivateObjects[i].SetActive(true);
                }
            }
            ActivateOnce = true;
        }
    }
    void Update ()
    {
        if(closeDoor)
        {
            CloseDoor();
        }
        if(openDoor)
        {
            OpenDoorWithoutAnimation();
        }
		if(checkDestroy)
        {
            if(!animationDoor.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
	}
    public void SetCloseDoor(bool _close)
    {
        closeDoor = _close;
    }
    public void SetOpenDoor(bool _abrir)
    {
        openDoor = _abrir;
    }
    public void OpenDoor()
    {
        animationDoor.clip = animationClip;        
        animationDoor.Play();
        checkDestroy = true;
    }
    public void OpenDoorWithoutAnimation()
    {
        if (timeMov > 0)
        {
            timeMov = timeMov - Time.deltaTime;
            y = y - Time.deltaTime * speed;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (timeMov <= 0)
        {
            timeMov = auxTimeMov;
            openDoor = false;
        }
        if (!ActivateOnce)
        {
            for (int i = 0; i < ActivateObjects.Length; i++)
            {
                if (ActivateObjects[i] != null)
                {
                    ActivateObjects[i].SetActive(true);
                }
            }
            ActivateOnce = true;
        }
    }
    public void CloseDoor()
    {
        if(timeMov > 0)
        {
            timeMov = timeMov - Time.deltaTime;
            y = y + Time.deltaTime * speed;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if(timeMov <= 0)
        {
            timeMov = auxTimeMov;
            closeDoor = false;
        }
    }
}
