using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCredits : MonoBehaviour {

    // Use this for initialization
    public GameObject completedGame;
    public GameObject credits;
    public GameObject buttonPinUp;
    public float speed;
    private float y;
    private float yButtonPinUp;
    public float limit;
    //public float riseTime;
    void Start() {
        y = credits.transform.position.y;
        yButtonPinUp = buttonPinUp.transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(buttonPinUp.transform.position.y);
        if (buttonPinUp.transform.position.y < limit)
        {
            if (credits.activeSelf == true)
            {
                MoveVertical();
            }
        }
    }
    public void CreditsMenu()
    {
        credits.SetActive(true);
        completedGame.SetActive(false);
    }
    public void MoveVertical()
    {
        y = y + Time.deltaTime * speed;
        yButtonPinUp = yButtonPinUp + Time.deltaTime * speed;
        credits.transform.position = new Vector3(credits.transform.position.x, y, credits.transform.position.z);
        buttonPinUp.transform.position = new Vector3(buttonPinUp.transform.position.x, yButtonPinUp, buttonPinUp.transform.position.z);
    }
}
