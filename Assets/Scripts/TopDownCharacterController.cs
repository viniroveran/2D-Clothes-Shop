using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D _characterRigidBody2D;
    private GameManager _gameManager;

    private void Start()
    {
        _characterRigidBody2D = GetComponent<Rigidbody2D>();
        _gameManager = GameManager.Instance;
    }

    private void Update()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            dir.y = 1;
        
        if (Input.GetKey(KeyCode.A))
            dir.x = -1;

        if (Input.GetKey(KeyCode.S))
            dir.y = -1;
        
        if (Input.GetKey(KeyCode.D))
            dir.x = 1;

        if (Input.GetKeyDown(KeyCode.Escape))
            _gameManager.Pause(!_gameManager.isPaused);

        dir.Normalize();

        _characterRigidBody2D.velocity = speed * dir;
    }
}
