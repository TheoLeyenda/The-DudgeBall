using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour {

    // Use this for initialization
    public PoolPelota poolEnemigo;
    private PoolObject poolObject;
    //public GameObject Enemigo;
    public int cantEnemigosInicial;
    public float dileyCreacion;
    private int TOPE_CREACION;
    private float auxDileyCreacion;
    public float rangoX;
    public float rangoZ;
    private int creaciones;
    private int creacionesLocales;
    public int cantCrearLocal;
    public float velocidadCreacion;
    private bool enFuncionamiento;
    public float IncrementoCreacion;
    private int TOPE_MAXIMO;
    public int tipoEnemigo;
    public int patronEnemigo;
    void Start() {
        auxDileyCreacion = dileyCreacion;
        TOPE_CREACION = cantEnemigosInicial;
        enFuncionamiento = true;
        TOPE_MAXIMO = poolEnemigo.count - 50;
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.GetGameManager().cantEnemigosEnPantalla <= 0)
        {
            GameManager.GetGameManager().SetEntrarRonda(true);
        }
        if (enFuncionamiento && TOPE_CREACION < TOPE_MAXIMO && GameManager.GetGameManager().supervivencia)
        {
            if (dileyCreacion > 0)
            {
                dileyCreacion = dileyCreacion - Time.deltaTime;
            }
            if (dileyCreacion <= 0 && creaciones < TOPE_CREACION)
            {
                if (GameManager.GetGameManager() != null)
                {
                    GameManager.GetGameManager().SumarEnemigoEnPantalla();
                }
                creaciones++;
                if (tipoEnemigo == 1)
                {
                    GameObject go = poolEnemigo.GetObject();
                    Corredor corredor = go.GetComponent<Corredor>();
                    go.transform.position = transform.position + new Vector3(Random.Range(0, rangoX), transform.position.y, Random.Range(0, rangoZ));
                    go.transform.rotation = transform.rotation;
                    corredor.Prendido();
                    corredor.PatronDeMovimiento = patronEnemigo;
                    if (GameManager.GetGameManager().GetRonda() > 0)
                    {
                        corredor.SumarVelocidad();
                    }


                }
                if (tipoEnemigo == 2)
                {
                    GameObject go = poolEnemigo.GetObject();
                    Tirador tirador = go.GetComponent<Tirador>();
                    go.transform.position = transform.position + new Vector3(Random.Range(0, rangoX), transform.position.y, Random.Range(0, rangoZ));
                    go.transform.rotation = transform.rotation;
                    tirador.Prendido();
                    tirador.tipoPatron = patronEnemigo;
                    if (GameManager.GetGameManager().GetRonda() > 1)
                    {
                        tirador.SumarVelocidad();
                    }
                }
                dileyCreacion = auxDileyCreacion;
            }
            if (creaciones >= TOPE_CREACION)
            {
                creaciones = 0;
                TOPE_CREACION = TOPE_CREACION + (int)IncrementoCreacion;
                enFuncionamiento = false;
            }
        }
        if (enFuncionamiento && GameManager.GetGameManager().historia && creaciones < cantCrearLocal)
        {
            if (dileyCreacion > 0)
            {
                dileyCreacion = dileyCreacion - Time.deltaTime;
            }
            if (dileyCreacion <= 0)
            {
                creaciones++;
                if (tipoEnemigo == 1)
                {
                    GameObject go = poolEnemigo.GetObject();
                    Corredor corredor = go.GetComponent<Corredor>();
                    go.transform.position = transform.position + new Vector3(Random.Range(0, rangoX), transform.position.y, Random.Range(0, rangoZ));
                    go.transform.rotation = transform.rotation;
                    corredor.Prendido();
                    corredor.velocidad = velocidadCreacion;
                    corredor.PatronDeMovimiento = patronEnemigo;
                    if (GameManager.GetGameManager().GetRonda() > 0)
                    {
                        corredor.SumarVelocidad();
                    }


                }
                if (tipoEnemigo == 2)
                {
                    GameObject go = poolEnemigo.GetObject();
                    Tirador tirador = go.GetComponent<Tirador>();
                    go.transform.position = transform.position + new Vector3(Random.Range(0, rangoX), transform.position.y, Random.Range(0, rangoZ));
                    go.transform.rotation = transform.rotation;
                    tirador.Prendido();
                    tirador.velocidad = velocidadCreacion;
                    tirador.tipoPatron = patronEnemigo;
                    if (GameManager.GetGameManager().GetRonda() > 1)
                    {
                        tirador.SumarVelocidad();
                    }
                }
                dileyCreacion = auxDileyCreacion;
            }
        }
    }
    public void SetEnFuncionamiento(bool _enFuncionamiento)
    {
        enFuncionamiento = _enFuncionamiento;
    }
    public bool GetEnFuncionamiento()
    {
        return enFuncionamiento;
    }
    public int GetCreaciones()
    {
        return creaciones;
    }
    public void SetCreaciones(int _creaciones)
    {
        creaciones = _creaciones;
    }
    public void SetTopeCreacion(int _TOPE_CREACION)
    {
        TOPE_CREACION = _TOPE_CREACION;
    }
    public int GetTopeCreacion()
    {
        return TOPE_CREACION;
    }
}
