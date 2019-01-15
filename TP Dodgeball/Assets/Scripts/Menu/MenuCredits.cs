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
    public float limit;
    //public float riseTime;
    void Start() {
        y = credits.transform.position.y;
    }

    // Update is called once per frame
    void Update() {
        if (credits.activeSelf == true)
        {
            MoveVertical();
        }
    }
    public void CreditsMenu()
    {
        credits.SetActive(true);
        buttonPinUp.SetActive(true);
        completedGame.SetActive(false);
    }
    public void MoveVertical()
    {
        y = y + Time.deltaTime * speed;
        credits.transform.position = new Vector3(credits.transform.position.x, y, credits.transform.position.z);
    }
}
