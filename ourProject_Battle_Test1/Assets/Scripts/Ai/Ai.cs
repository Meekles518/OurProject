using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{

    State currentState;

    public Transform player;
    public Vector2 Spawnposition;

    void Start()
    {

        currentState = new Idle(gameObject, player);
        Spawnposition = this.transform.position;

    }


    void Update()
    {

        currentState = currentState.Process();
    }
}