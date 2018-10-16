using UnityEngine;
using UnityEngine.EventSystems;

public class FixedButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool Pressed;
    private float contador;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Pressed)
        {
            contador = contador + Time.deltaTime;
            if(contador > 0.1f)
            {
                Pressed = false;
            }
        }
        if(!Pressed)
        {
            contador = 0;
        }
    }
    public void cambiarBoleano()
    {
        Pressed = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}
