using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM 
{
    int[,] fsm;

    int currentState;

    public FSM(int statesCount, int eventsCount, int initState)
    {
        fsm = new int[statesCount, eventsCount];

        for (int i=0; i<statesCount; i++)
        {
            for (int j=0; j < eventsCount; j++)
            {
                fsm[i, j] = -1;
            }
        }

        currentState = initState;
    }

    public void SetRelation(int srcState, int evt, int dstState)
    {
        // Chequear no salirnos del tamaño de la matriz
        fsm[srcState, evt] = dstState;
    }

    public void SendEvent(int evt)
    {
        if (fsm[currentState, evt] != -1)
        {
            currentState = fsm[currentState, evt];
        }
    }

    public int GetState()
    {
        return currentState;
    }
}
