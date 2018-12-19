using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DataGameOver : MonoBehaviour {

    // Use this for initialization
    private DataStructure dataStructure;
    
    private float score;
    private float remainingOpportunities;
    private int downcastEnemies;
    private float remainingLife;

    public Text textScore;
    public Text textRemainingOpportunities;
    public Text textDowncastEnemies;
    public Text textRemainingLife;
    
    void Start ()
    {
        if (DataStructure.auxiliaryDataStructure != null)
        {
            dataStructure = DataStructure.auxiliaryDataStructure;
            score = dataStructure.playerData.score;
            remainingOpportunities = dataStructure.playerData.opportunities;
            downcastEnemies = dataStructure.playerData.downcastEnemies;
            remainingLife = dataStructure.playerData.life;
        }
        
        ShowData();

    }

    // Update is called once per frame
    public void ShowData()
    {
        if (textScore != null)
        {
            textScore.text = "Puntaje: " + score;
        }
        if (textRemainingOpportunities != null)
        {
            textRemainingOpportunities.text = "Oportunidades Restantes: " + remainingOpportunities;
        }
        if (textDowncastEnemies != null)
        {
            textDowncastEnemies.text = "Enemigos Abatidos:" + downcastEnemies;
        }
        if (textRemainingLife)
        {
            textRemainingLife.text = "Vida Restante: " + remainingLife; 
        }
    }
}

