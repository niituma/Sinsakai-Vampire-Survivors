using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody2D _rb;
    GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        Vector2 dir = _player.transform.position - transform.position;
        _rb.velocity = dir.normalized * 5;
        Debug.Log("Test");
    }
}
