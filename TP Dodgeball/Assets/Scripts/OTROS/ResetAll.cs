using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAll : MonoBehaviour {

    // Use this for initialization
    private DataStructure structure;
	void Start () {
		if(DataStructure.auxiliaryDataStructure != null)
        {
            structure = DataStructure.auxiliaryDataStructure;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Restart()
    {
        if(structure != null)
        {
            structure.playerData.powerUpAddLife = false;
            structure.playerData.powerUpChalecoAntiGolpes = false;
            structure.playerData.powerUpDobleDamage = false;
            structure.playerData.armor = 0;
            structure.playerData.downcastEnemies = 0;
            structure.playerData.ammoDanceBall = 0;
            structure.playerData.ammoFireBall = 0;
            structure.playerData.ammoIceBall = 0;
            structure.playerData.ammoExplociveBall = 0;
            structure.playerData.ammoFragmentBall = 0;
            structure.playerData.opportunities = 10;
            structure.playerData.score = 0;
            structure.playerData.life = structure.playerData.maxLife;
        }
    }
}