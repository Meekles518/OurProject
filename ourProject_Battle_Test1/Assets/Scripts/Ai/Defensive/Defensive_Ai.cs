using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Defensive_AI : MonoBehaviour
{

    Defensive_State currentState;

    public Transform player;
    public Vector2 Spawnposition;
    static public float smallAgrro = 10f;
    static public float largeAgrro = 20f;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        currentState = new Defensive_Idle(gameObject, player);
        Spawnposition = this.transform.position;

    }


    void Update()
    {

        currentState = currentState.Process();
    }
}