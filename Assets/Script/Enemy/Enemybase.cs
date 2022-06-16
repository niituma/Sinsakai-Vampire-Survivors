using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybase : MonoBehaviour, IObjectPool
{
    [SerializeField] protected float _speed = 4f;
    protected GameObject _player;
    protected ItemSpawner _itemSpawner;
    protected EnemyHPController _HP;
    protected Rigidbody2D _rb;
    protected bool _isDrop = true;
    // Start is called before the first frame update
    public void Start()
    {
        _HP = GetComponent<EnemyHPController>();
        _itemSpawner = FindObjectOfType<ItemSpawner>();
        _rb = GetComponent<Rigidbody2D>();
        _player = GameManager.Instance._player.gameObject;
    }

    // Update is called once per frame
    public void Update()
    {
        if (_HP._currenthp <= 0 && GetComponent<SpriteRenderer>().color == Color.white)
        {
            Destroy();
        }
        if (_player)
        {
            if (Vector2.Distance(transform.position, _player.transform.position) > 15f)
            {
                _isDrop = false;
                Destroy();
            }

            Vector2 Dir = (_player.transform.position - transform.position).normalized;
            _rb.velocity = Dir.normalized * _speed;

            LeftRightDir();
        }
    }

    public void LeftRightDir()
    {
        var scale = transform.localScale;
        if (transform.position.x > _player.transform.position.x && scale.x != -1)
        {
            scale.x = -1;
            transform.localScale = scale;
        }
        else if (transform.position.x < _player.transform.position.x && scale.x != 1)
        {
            scale.x = 1;
            transform.localScale = scale;
        }
    }

    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;
    public void InactiveInstantiate()
    {
        gameObject.SetActive(false);
        _isActrive = false;
    }
    public void Create()
    {
        gameObject.SetActive(true);
        _isActrive = true;
    }
    public void Destroy()
    {
        if (_isDrop)
        {
            var item = _itemSpawner.Spawn();
            if (item)
            {
                item.transform.position = transform.position;
            }
        }
        else
        {
            _isDrop = true;
        }

        gameObject.SetActive(false);
        _isActrive = false;
    }
}
