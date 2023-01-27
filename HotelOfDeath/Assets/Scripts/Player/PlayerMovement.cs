using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player Movement: ")]
    [SerializeField] [Range(0, 12)] private float playerSpeed;
    [SerializeField] [Range(-10, 10)] private float playerGravity;

    [Header("Step Sound Settings: ")] 
    [SerializeField] private AudioSource stepSound;
    [SerializeField] private AudioClip[] stepSounds;

    [Header("Flashlight Settings: ")] 
    [SerializeField] private AudioSource flashLightSound;
    [SerializeField] private GameObject flashLight;
    private bool isLit = true;
    
    // private things that is needed
    private CharacterController _playerController;
    
    // Player Movement Settings: 
    private float _moveX;
    private float _moveY;
    private float _stepDelay = 0.5f;
    private float _nextStepTime;
    private Vector3 _playerVelocity;

    // Start is called before the first frame update
    private void Start()
    {
        _playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            ToggleLight();
        }
    }
    
    private void ToggleLight()
    {
        isLit = !isLit;
        flashLight.SetActive(isLit);
    }

    private void Move()
    {
        _moveX = Input.GetAxis("Horizontal");
        _moveY = Input.GetAxis("Vertical");

        var thisTransform = transform;
        var playerMoves = thisTransform.right * _moveX + thisTransform.forward * _moveY;
        _playerController.Move(playerMoves * playerSpeed * Time.deltaTime);

        if (_moveX != 0 || _moveY != 0)
        {
            PlayStepSound();
        }
        
        _playerVelocity.y += playerGravity * Time.deltaTime;
        _playerController.Move(_playerVelocity * Time.deltaTime);
    }

    private void PlayStepSound()
    {
        switch (Time.time > _nextStepTime)
        {
            case true:
            {
                _nextStepTime = Time.time + _stepDelay;
                var randomIndex = Random.Range(0, stepSounds.Length);
                stepSound.clip = stepSounds[randomIndex];
                stepSound.Play();
                break;
            }
        }
    }
}
