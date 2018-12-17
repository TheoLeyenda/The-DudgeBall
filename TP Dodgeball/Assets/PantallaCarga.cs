using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class PantallaCarga : MonoBehaviour {

    // Use this for initialization
    public Text percentage;
    public GameObject loadBarFrame;
    public GameObject load;
    public float percentageLoadMaximun;
    public float loadSpeed;
    public string loadScreen;
    private float percentageLoad;
    private bool nextLevel;
    public bool usingString;
    private int loadLevel;
    public bool notLoad;
    private DataStructure dataStructure;
	void Start () {
        System.GC.Collect();
        percentageLoad = 0;
        if(DataStructure.auxiliaryDataStructure != null)
        {
            dataStructure = DataStructure.auxiliaryDataStructure;
        }
        nextLevel = false;
	}

    // Update is called once per frame
    void Update() {

        if (!notLoad)
        {
            UpdateLoadBar();
            if (usingString)
            {
                dataStructure.NextLevel();
            }
            if (nextLevel && !usingString)
            {
                dataStructure.NextLevel();
            }
        }
	}
    public void UpdateLoadBar()
    {
        percentageLoad = percentageLoad + Time.deltaTime * loadSpeed;
        if (load != null)
        {
            float z = (float)percentageLoad / (float)percentageLoadMaximun;
            Vector3 ScaleBar = new Vector3(1, 1, z);
            load.transform.localScale = ScaleBar;
        }
        percentage.text = "" + (int)percentageLoad + "%";
        if (percentageLoad >= percentageLoadMaximun && !nextLevel && !usingString)
        {
            dataStructure.SetLevel(dataStructure.levelData.I_levelLoad + 1);
            percentageLoad = 0;
            nextLevel = true;
        }
        if(percentageLoad >= percentageLoadMaximun && !nextLevel && usingString)
        {
            dataStructure.SetLevel(loadScreen);
            percentageLoad = 0;
            nextLevel = true;
        }
    }
    public void ResetLevel()
    {
        dataStructure.SetLevel(0);
        SceneManager.LoadScene("SplashScreen");
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)