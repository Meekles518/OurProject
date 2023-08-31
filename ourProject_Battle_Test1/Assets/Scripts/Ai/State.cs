using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class State
{

    public enum STATE
    {

        IDLE,
        PATROL,
        PURSUE,
        ATTACK,
        SLEEP,
        RUNAWAY
    };

    public enum EVENT
    {

        ENTER,
        UPDATE,
        EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject enemy;
    protected Transform player;
    protected State nextState;

    float smallAgrro = 10f;
    float largeAgrro = 20.0f;
    public LayerMask Target_layer;
    public Vector2 Spawnposition;


    public State(GameObject _enemy, Transform _player)
    {

        enemy = _enemy;
        player = _player;
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {

        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {

            Exit();
            return nextState;
        }

        return this;
    }

    public bool Aggro()
    {

        Vector3 direction = player.position - enemy.transform.position;

        if (direction.magnitude <= largeAgrro)
        {

            if (direction.magnitude <= smallAgrro)
            {
                return true;
            }
            else
            {
                Collider2D Target = Physics2D.OverlapCircle(enemy.transform.position, largeAgrro, Target_layer);
                if (Target != null)
                {
                    return true;
                }
                return false;
            }


        }

        return false;
    }
    //¿©±â
    public class Patrol : State
    {

        int currentIndex = -1;

        public Patrol(GameObject _enemy, Transform _player)
            : base(_enemy, _player)
        {

            name = STATE.PATROL;


        }

        public override void Enter()
        {

            float lastDistance = Mathf.Infinity;

            for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; ++i)
            {

                GameObject thisWP = GameEnvironment.Singleton.Checkpoints[i];
                float distance = Vector3.Distance(enemy.transform.position, thisWP.transform.position);
                if (distance < lastDistance)
                {

                    currentIndex = i - 1;
                    lastDistance = distance;
                }
            }


            base.Enter();
        }

        public override void Update()
        {

            /*if (agent.remainingDistance < 1) {

                if (currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1) {

                    currentIndex = 0;
                } else {

                    currentIndex++;
                }

                agent.SetDestination(GameEnvironment.Singleton.Checkpoints[currentIndex].transform.position);
            }

            if (CanSeePlayer()) {

                nextState = new Pursue(npc, agent, anim, player);
                stage = EVENT.EXIT;
            } else if (IsPlayerBehind()) {

                nextState = new RunAway(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }*/
        }

        public override void Exit()
        {


            base.Exit();
        }
    }

    public class Pursue : State
    {

        public Pursue(GameObject _enemy, Transform _player)
            : base(_enemy, _player)
        {


            name = STATE.PURSUE;

        }

        public override void Enter()
        {


            base.Enter();
        }

        public override void Update()
        {



            if (enemy == null)
            {

                if (CanAttackPlayer())
                {

                    nextState = new Attack(enemy, player);
                    stage = EVENT.EXIT;
                }
                else if (!CanSeePlayer())
                {

                    nextState = new Patrol(enemy, player);
                    stage = EVENT.EXIT;
                }
            }
        }

        public override void Exit()
        {


            base.Exit();
        }
    }

    public class Attack : State
    {

        float rotationSpeed = 2.0f;
        AudioSource shoot;

        public Attack(GameObject _enemy, Transform _player)
            : base(_enemy, _player)
        {

            name = STATE.ATTACK;

        }

        public override void Enter()
        {


            shoot.Play();
            base.Enter();
        }

        public override void Update()
        {

            Vector3 direction = player.position - enemy.transform.position;
            float angle = Vector3.Angle(direction, enemy.transform.forward);
            direction.y = 0.0f;

            enemy.transform.rotation =
                Quaternion.Slerp(enemy.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

            if (!CanAttackPlayer())
            {

                nextState = new Idle(enemy, player);
                shoot.Stop();
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {


            base.Exit();
        }
    }

    public class RunAway : State
    {

        GameObject safeLocation;

        public RunAway(GameObject _enemy, Transform _player)
            : base(_enemy, _player)
        {

            name = STATE.RUNAWAY;
            safeLocation = GameObject.FindGameObjectWithTag("Safe");
        }

        public override void Enter()
        {


            base.Enter();
        }

        public override void Update()
        {

            if (enemy == null)
            {

                nextState = new Idle(enemy, player);
                stage = EVENT.EXIT;
            }
        }

        public override void Exit()
        {


            base.Exit();
        }
    }
}