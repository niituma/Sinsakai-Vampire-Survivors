using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

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

    List<ISkill> _skill = new List<ISkill>();

    Rigidbody2D _rb;
    PlayerHPController _hp;
    Animator _anim;

    public Vector2 Lastdir { get => _lastdir; set => _lastdir = value; }
    AddOrignalMethod Method = new AddOrignalMethod();

    private void Awake()
    {
        AddSkill(1);
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

        if (Input.GetButtonDown("Jump") && _isMoving && !_isStepMoving)
        {
            var _scale = transform.localScale;

            _isStepMoving = true;
            _isMoving = false;
            _rb.velocity = _dir == Vector2.zero ? new Vector2(_stepSpeed * _lastdir.x, _stepSpeed * _lastdir.y) :
                _rb.velocity = new Vector2(_stepSpeed * _h, _stepSpeed * _v);
            StartCoroutine(Method.DelayMethod(_stepMoveCooltime, () => _isStepMoving = false));
            StartCoroutine(Method.DelayMethod(_moveCooltime, () => _isMoving = true));
        }
    }
    private void FixedUpdate()
    {
        if (_isMoving)
        {
            float speed = _dir == Vector2.zero ? 0 : _speed;
            _rb.velocity = _dir.normalized * speed;
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
                collision.gameObject.GetComponent<EnemyHPController>().Damege(1);
            }
        }
    }

    public void AddSkill(int skillId)
    {
        var having = _skill.Where(s => s.SkillId == (SkillDef)skillId);
        if (having.Count() > 0)
        {
            having.Single().Levelup();
        }
        else
        {
            ISkill newskill = null;
            switch ((SkillDef)skillId)
            {
                case SkillDef.ShotBullet:
                    newskill = new ShotBullet();
                    break;

                case SkillDef.AreaAttack:
                    newskill = new AreaAttack();
                    break;
            }

            if (newskill != null)
            {
                newskill.Setup();
                _skill.Add(newskill);
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
}
