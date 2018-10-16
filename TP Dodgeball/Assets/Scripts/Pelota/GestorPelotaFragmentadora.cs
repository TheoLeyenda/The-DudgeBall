using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorPelotaFragmentadora : MonoBehaviour {

    // Use this for initialization
    public PelotaFragmentadora codeFragmentadora;
    public MiniPelota codeMiniPelota1;
    public MiniPelota codeMiniPelota2;
    public MiniPelota codeMiniPelota3;
    public GameObject pelotaFragmentadora;
    public GameObject miniPelota1;
    public GameObject miniPelota2;
    public GameObject miniPelota3;
    public PoolPelota pool;
    private PoolObject poolObject;
    public float auxTiempoVidaPelotaFragmentadora;
    //public float auxTiempoVidaMiniPelota1;
    //public float auxTiempoVidaMiniPelota2;
    //public float auxTiempoVidaMiniPelota3;
    private void Start()
    {
        auxTiempoVidaPelotaFragmentadora = codeFragmentadora.GetTiempoVida();
    }
    void OnEnable() {
        
        poolObject = GetComponent<PoolObject>();
        pelotaFragmentadora.SetActive(true);
        miniPelota1.SetActive(false);
        miniPelota2.SetActive(false);
        miniPelota3.SetActive(false);
        codeFragmentadora.SetResiclar(false);
        codeMiniPelota1.SetResiclar(false);
        codeMiniPelota2.SetResiclar(false);
        codeMiniPelota3.SetResiclar(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(codeFragmentadora.GetResiclar())
        {
            codeFragmentadora.SetTiempoVida(auxTiempoVidaPelotaFragmentadora);
            pelotaFragmentadora.SetActive(false);
            miniPelota1.SetActive(true);
            miniPelota2.SetActive(true);
            miniPelota3.SetActive(true);

            if (codeMiniPelota1.GetResiclar() && codeMiniPelota2.GetResiclar() && codeMiniPelota3.GetResiclar())
            {
                poolObject.Resiclarme();
            }
        }
	}
    public void Disparar()
    {
        codeFragmentadora.SetTiempoVida(auxTiempoVidaPelotaFragmentadora);
        pelotaFragmentadora.SetActive(true);
        codeFragmentadora.SetResiclar(false);
        codeMiniPelota1.SetResiclar(false);
        codeMiniPelota2.SetResiclar(false);
        codeMiniPelota3.SetResiclar(false);
        pelotaFragmentadora.transform.position = (transform.position + transform.right)-transform.forward;
        codeFragmentadora.Disparar();
    }
}
