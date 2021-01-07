using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    private float _speed = 15.0f;
    private float _jumpHeight = 15.0f;
    private float _gravity = 30.0f;
    private Vector3 _direction;

    // state parameter
    private bool _jumping = false;

    // reference
    private CharacterController _characterController;
    private Animator _anim;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_characterController.isGrounded == true)
        {
            float forward = Input.GetAxisRaw("Horizontal");
            _direction = new Vector3(0, 0, forward) * _speed;

            if (forward != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            _anim.SetFloat("Speed", Mathf.Abs(forward));

            if (_jumping is true)
            {
                _jumping = false;
                _anim.SetBool("Jump", _jumping);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _direction.y += _jumpHeight;
                _jumping = true;
                _anim.SetBool("Jump", _jumping);

            }
        }

        _direction.y -= _gravity * Time.deltaTime;
        // if jump
        // adjust jump height

        _characterController.Move(_direction * Time.deltaTime);
    }
}
