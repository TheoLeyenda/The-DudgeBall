using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
public class ParteSuperiorCuerpo : MonoBehaviour {

    // Use this for initialization
    public Kraken kraken;
    public float Damage;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            
            Rigidbody rigP = other.GetComponent<Rigidbody>();
            rigP.velocity = Vector3.zero;
            rigP.AddRelativeForce(-Jugador.GetPlayer().transform.forward * kraken.powerAttack, ForceMode.Impulse);
            Jugador.GetPlayer().rigJugador = rigP;
            Jugador.GetPlayer().life = Jugador.GetPlayer().life - Damage;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Entre");
            Rigidbody rigP = collision.gameObject.GetComponent<Rigidbody>();
            rigP.velocity = Vector3.zero;
            rigP.AddRelativeForce(-Jugador.GetPlayer().transform.forward * kraken.powerAttack, ForceMode.Impulse);
            Jugador.GetPlayer().rigJugador = rigP;
        }
    }
}
//TRADUCIDO(FALTA TRADUCIR EL NOMBRE DE LA CLASE)
