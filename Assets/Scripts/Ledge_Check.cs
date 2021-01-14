using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge_Check : MonoBehaviour
{
    // state variable
    private Vector3 _handPos;
    
    // cached reference
    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _handPos = new Vector3(-1.36f, 67.73f, 123.44f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ledge_grab_checker"))
        {
            _player.GrabLedge(_handPos);
            Debug.Log("Grab");
        }
    }

}
