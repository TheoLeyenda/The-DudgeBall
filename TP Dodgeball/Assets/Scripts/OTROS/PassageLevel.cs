using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassageLevel : MonoBehaviour {

    // Use this for initialization
    public string level;
    public int numberLevel;
    public bool levelByNumber;
    private Player player;
    private void Start()
    {
        if (Player.InstancePlayer != null)
        {
            player = Player.InstancePlayer;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (DataStructure.GetAuxiliaryDataStructure() != null)
            {

                DataStructure.GetAuxiliaryDataStructure().SetPlayerData(player);
                if (player.armor > 0)
                {
                    DataStructure.GetAuxiliaryDataStructure().playerData.armor = player.armor;
                    player.logoArmor.SetActive(true);
                    player.textArmor.gameObject.SetActive(true);
                }
            }
            if (level != null && !levelByNumber)
            {
                SceneManager.LoadScene(level);
            }
            if(levelByNumber)
            {
                SceneManager.LoadScene(numberLevel);
            }
        }
    }
    public void NextLevel(string level)
    {

        if (level != null && !levelByNumber)
        {
            SceneManager.LoadScene(level);
        }
        if (levelByNumber)
        {
            SceneManager.LoadScene(numberLevel);
        }
    }
}