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
    private Vector3 _feetPos = new Vector3(-1.36f, 74.52f, 124.41f);

    // reference
    private CharacterController _characterController;
    private Animator _anim;
    private Ledge_Check _activeLedge;


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            ClimbUp();
        }
    }

    private void Movement()
    {
        if (_characterController.isGrounded == true)
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

    public void GrabLedge(Vector3 handPos, Ledge_Check currentLedge) 
    {
        _characterController.enabled = false;
        _anim.SetBool("GrabLedge", true);
        _anim.SetBool("Jump", false);
        _anim.SetFloat("Speed", 0f);
        transform.position = handPos;
        _activeLedge = currentLedge;
    }

    private void ClimbUp()
    {
        _anim.SetTrigger("ClimbUp");
    }

    public void StandUp()
    {
        transform.position = _activeLedge.GetStandPos();
        _anim.SetBool("GrabLedge", false);
        _characterController.enabled = true;
    }
}
