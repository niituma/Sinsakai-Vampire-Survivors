using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Playerの動くスピード")] float _speed = 7f;
    float _h,_v;
    Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       _h = Input.GetAxisRaw("Horizontal");
       _v = Input.GetAxisRaw("Vertical");
        _rb.velocity = new Vector2(_speed * _h, _speed * _v);
    }
}
