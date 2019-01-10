using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    public void Respawn()
    {
        for (int i = 0; i < Player.InstancePlayer.ObjectsOtherCamvas.Length; i++)
        {
            Player.InstancePlayer.ObjectsOtherCamvas[i].SetActive(true);
        }
        Player.InstancePlayer.CamvasDeath.SetActive(false);
        Player.InstancePlayer.transform.position =  Player.InstancePlayer.posRespawn.position;
        Player.InstancePlayer.life = 100;
        GameManager.instanceGameManager.pause = false;
        Player.InstancePlayer.pause = false;
        Player.InstancePlayer.SetImmune(true);
        Player.InstancePlayer.SetCountImmune(3.7f);
    }
}
