using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class MenuCreditos : MonoBehaviour {

    // Use this for initialization
    public GameObject completedGame;
    public GameObject credits;
    public float speed;
    private float y;
    public float riseTime;
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
        completedGame.SetActive(false);
    }
    public void MoveVertical()
    {
        if (riseTime > 0)
        {
            y = y + Time.deltaTime * speed;
            credits.transform.position = new Vector3(credits.transform.position.x, y, credits.transform.position.z);
            riseTime = riseTime - Time.deltaTime;
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
