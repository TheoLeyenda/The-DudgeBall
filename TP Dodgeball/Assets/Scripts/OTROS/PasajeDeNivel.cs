using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)

public class PasajeDeNivel : MonoBehaviour {

    // Use this for initialization
    public string level;
    public int numberLevel;
    public bool levelByNumber;
    private Jugador player;
    private void Start()
    {
        if (Jugador.InstancePlayer != null)
        {
            player = Jugador.InstancePlayer;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (DataStructure.GetEstructuraDatosAuxiliares() != null)
            {

                DataStructure.GetEstructuraDatosAuxiliares().SetPlayerData(player);
                if (player.armor > 0)
                {
                    DataStructure.GetEstructuraDatosAuxiliares().playerData.armor = player.armor;
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
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)