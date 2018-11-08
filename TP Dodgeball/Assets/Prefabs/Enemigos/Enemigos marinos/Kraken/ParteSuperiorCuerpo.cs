using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParteSuperiorCuerpo : MonoBehaviour {

    // Use this for initialization
    public Kraken kraken;
    public float Danio;
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
            rigP.AddRelativeForce(-Jugador.GetJugador().transform.forward * kraken.potenciaAtaque, ForceMode.Impulse);
            Jugador.GetJugador().rigJugador = rigP;
            Jugador.GetJugador().vida = Jugador.GetJugador().vida - Danio;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Entre");
            Rigidbody rigP = collision.gameObject.GetComponent<Rigidbody>();
            rigP.velocity = Vector3.zero;
            rigP.AddRelativeForce(-Jugador.GetJugador().transform.forward * kraken.potenciaAtaque, ForceMode.Impulse);
            Jugador.GetJugador().rigJugador = rigP;
        }
    }
}
