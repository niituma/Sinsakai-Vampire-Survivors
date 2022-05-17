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

    Rigidbody2D _rb;
    GameManager _gm;

    // Start is called before the first frame update
    void Start()
    {
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
            _rb.velocity = _dir == Vector2.zero ? new Vector2(_stepSpeed * _scale.x, _rb.velocity.y) :
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

            if (_h > 0)
            {
                var scale = transform.localScale;
                scale.x = 1;
                transform.localScale = scale;
            }
            else if(_h < 0)
            {
                var scale = transform.localScale;
                scale.x = -1;
                transform.localScale = scale;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !_isMoving)
        {
            collision.gameObject.SetActive(false);
        }
    }
    private IEnumerator DelayMethod(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
