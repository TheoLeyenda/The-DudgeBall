using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dificulty : MonoBehaviour {

	public void Easy()
    {
        DataStructure.auxiliaryDataStructure.dificulty = 1;
    }
    public void Medium()
    {
        DataStructure.auxiliaryDataStructure.dificulty = 2;
    }
    public void Hard()
    {
        DataStructure.auxiliaryDataStructure.dificulty = 3;
    }
}
