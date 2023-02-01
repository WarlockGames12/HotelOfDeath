using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    private StateMachine _stateMachine;
    
    // Start is called before the first frame update
    private void Awake()
    {
        _stateMachine = GetComponent<StateMachine>();

        _stateMachine.AddState(EnemyStates.Wandering, GetComponent<WanderingState>());
        _stateMachine.AddState(EnemyStates.Alert, GetComponent<AlertState>());
        _stateMachine.AddState(EnemyStates.Chase, GetComponent<ChaseState>());
        
        _stateMachine.SetState(EnemyStates.Wandering);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
