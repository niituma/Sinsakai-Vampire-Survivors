using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Tooltip("Playerの移動スピード")] float _speed = 7f;
    [SerializeField, Tooltip("Playerのステップ移動のスピード")] float _stepSpeed = 10f;
    [SerializeField, Tooltip("Playerの移動制限する")] float _moveCooltime = 0.3f;
    [SerializeField] float _stepMoveCooltime = 2f;
    bool _isMoving = true;
    bool _isStepMoving = false;
    float _h, _v;
    Vector2 _dir;
    Vector2 _lastdir = new Vector2(0, -1);

    Rigidbody2D _rb;
    PlayerHPController _hp;
    GameManager _gm;
    Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _hp = GetComponent<PlayerHPController>();
        _rb = GetComponent<Rigidbody2D>();
        _gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        _dir = new Vector2(_h, _v);


        if (Input.GetButtonDown("Jump") && _isMoving && !_isStepMoving)
        {
            var _scale = transform.localScale;

            _isStepMoving = true;
            _isMoving = false;
            _rb.velocity = _dir == Vector2.zero ? new Vector2(_stepSpeed * _lastdir.x, _stepSpeed * _lastdir.y) :
                _rb.velocity = new Vector2(_stepSpeed * _h, _stepSpeed * _v);
            StartCoroutine(DelayMethod(_stepMoveCooltime, () => _isStepMoving = false));
            StartCoroutine(DelayMethod(_moveCooltime, () => _isMoving = true));
        }
    }
    private void FixedUpdate()
    {
        if (_isMoving)
        {
            float speed = _dir == Vector2.zero ? 0 : _speed;
            _rb.velocity = new Vector2(speed * _h, speed * _v);
        }
    }
    private void LateUpdate()
    {
        AnimateDir();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (_isMoving)
            {
                _hp.Damege();
            }
            else
            {
                collision.gameObject.GetComponent<Enemybase>().Destroy();
            }
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

    private IEnumerator DelayMethod(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
