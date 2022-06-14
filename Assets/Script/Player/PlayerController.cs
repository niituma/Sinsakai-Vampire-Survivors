using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Playerの移動スピード")] float _speed = 7f;
    [SerializeField, Tooltip("Playerのステップ移動のスピード")] float _stepSpeed = 10f;
    float _h, _v;
    Vector2 _dir;
    Vector2 _lastdir = new Vector2(0, -1);

    Rigidbody2D _rb;
    PlayerHPController _hp;
    Animator _anim;

    public Vector2 Lastdir { get => _lastdir; set => _lastdir = value; }

    private void Awake()
    {
        GameManager.Instance.SetPlayer(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _hp = GetComponent<PlayerHPController>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        _dir = new Vector2(_h, _v);
    }
    private void FixedUpdate()
    {
        float speed = _dir == Vector2.zero ? 0 : _speed;
        _rb.velocity = _dir.normalized * speed;

    }
    private void LateUpdate()
    {
        AnimateDir();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _hp.Damege();
        }

    }

    void AnimateDir()
    {
        if (Mathf.Abs(_dir.x) > 0.5f)
        {
            _lastdir.x = _dir.x;
            _lastdir.y = 0;
        }

        if (Mathf.Abs(_dir.y) > 0.5f)
        {
            _lastdir.y = _dir.y;
            _lastdir.x = 0;
        }

        _anim.SetFloat("DirX", _dir.x);
        _anim.SetFloat("DirY", _dir.y);
        _anim.SetFloat("LastMoveX", _lastdir.x);
        _anim.SetFloat("LastMoveY", _lastdir.y);
        _anim.SetFloat("Input", _dir.magnitude);
    }
}
