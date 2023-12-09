using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int _state = Animator.StringToHash("State");
    
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private Transform raycastOBJ;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private bool block;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Movement();
        RaycastControl();
    }

    private void Movement()
    {
        Vector3 movementVector = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);

        if (movementVector != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(movementVector);

        if(!block)
            _rigidbody.MovePosition(transform.position + _speed * movementVector);

        AnimationControl();

    }

    private void RaycastControl()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOBJ.position, raycastOBJ.forward, out hit, 2f))
        {
            if(!hit.collider.isTrigger)
                block = true;
        }
        else
            block = false;
    }

    private void AnimationControl()
    {
        if (_joystick.Direction == Vector2.zero)
        {
            _animator.SetInteger(_state, 0);
            _animator.speed = 1;
        }
        else
        {
            _animator.SetInteger(_state, 1);
            _animator.speed = _joystick.Direction.magnitude;
        }

    }


}
