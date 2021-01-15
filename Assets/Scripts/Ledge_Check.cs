using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge_Check : MonoBehaviour
{
    // state variable
    [SerializeField]
    private Vector3 _handPos, _standPos;

    // cached reference
    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_grab_checker"))
        {
            _player.GrabLedge(_handPos, this);
        }
    }

    public Vector3 GetStandPos() 
    {
        return _standPos;
    }

}
