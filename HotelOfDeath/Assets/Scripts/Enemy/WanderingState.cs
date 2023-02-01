using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class WanderingState : State
{

    private StateMachine _stateMachine;

    [Header("Wandering Settings: ")] 
    [SerializeField] private float sightRange = 10f;
    [SerializeField] private float hearingRange = 5f;
    [SerializeField] private float wanderRadius = 10f;
    [SerializeField] private float wanderSpeed = 1f;
    [SerializeField] private float chaseSpeed = 2f;
    [SerializeField] private float pathfindingInterval;

    private Transform _sawPlayer;
    private Vector3 _lastKnownPlayerPos;
    private Vector3 _wanderTarget;
    private float _pathfindingTimer;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        _sawPlayer = GameObject.FindWithTag("Player").transform;
        _audioSource = GetComponent<AudioSource>();
        _wanderTarget = RandomWanderTarget();
    }

    // Update is called once per frame
    private void Update()
    {
        if (CanHearPlayer())
        {
            _stateMachine.SetState(EnemyStates.Alert);
        }

        if (CanSeePlayer())
        {
            _stateMachine.SetState(EnemyStates.Alert);
        }
    }

    private bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, _sawPlayer.position) > sightRange)
            return false;

        RaycastHit hit;
        if (Physics.Linecast(transform.position, _sawPlayer.position, out hit))
        {
            _stateMachine.SetState(EnemyStates.Alert);
            return hit.transform == _sawPlayer;
        }
            

        return false;
    }

    private bool CanHearPlayer()
    {
        if (Vector3.Distance(transform.position, _sawPlayer.position) > hearingRange)
            return false;

        _stateMachine.SetState(EnemyStates.Alert);
        return _audioSource.isPlaying;
    }

    private void Search()
    {
        _pathfindingTimer += Time.deltaTime;
        if (_pathfindingTimer >= pathfindingInterval)
        {
            _pathfindingTimer = 0f;
            if (!CanSeePlayer())
            {
                _stateMachine.SetState(EnemyStates.Wandering);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _lastKnownPlayerPos, chaseSpeed * Time.deltaTime);
    }

    private void Chase()
    {
        transform.position = Vector3.MoveTowards(transform.position, _sawPlayer.position, chaseSpeed * Time.deltaTime);
    }

    private Vector3 RandomWanderTarget()
    {
        var wanderTarget = transform.position + (Random.insideUnitSphere * wanderRadius);
        wanderTarget.y = transform.position.y;
        return wanderTarget;
    }
    
    public override void Act()
    {
        throw new System.NotImplementedException();
    }

    public override void Reason()
    {
        throw new System.NotImplementedException();
    }
}
