using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAttack : MonoBehaviour {

    // Use this for initialization
    public Wizard wizard;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (wizard != null)
        {
            if (wizard.GetDead() == false)
            {

                if (other.gameObject.tag == "Player")
                {
                    wizard.Attaking = true;
                    wizard.animator.SetBool("Run", false);
                    wizard.animator.SetBool("Attack_B", false);
                    wizard.animator.SetBool("Attack_A", false);

                    if (wizard.ataqueFrontal)
                    {
                        wizard.animator.Play("UD_mage_07_attack_A");
                    }
                    else if (wizard.ataqueHorizontal)
                    {
                        wizard.animator.Play("UD_mage_08_attack_B");
                    }
                    wizard.animator.SetBool("Idle", true);
                    wizard.animator.SetBool("Death_B", false);
                    wizard.animator.SetBool("Death_A", false);
                    wizard.animator.SetBool("Damage", false);
                }

            }
        }
    }
    


}
