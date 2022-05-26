using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybase : MonoBehaviour, IObjectPool
{
    [SerializeField] float _speed = 4f;
    EnemyDate _date;
    GameObject _player;
    GameManager _gameManager;
    ItemSpawner _itemSpawner;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _itemSpawner = _gameManager.GetComponent<ItemSpawner>();
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (_player)
        {
            Vector2 Dir = (_player.transform.position - transform.position).normalized;
            _rb.velocity = Dir.normalized * _speed;

            var scale = transform.localScale;
            if (transform.position.x > _player.transform.position.x && scale.x != -1)
            {
                scale.x = -1;
                transform.localScale = scale;
            }
            else if(transform.position.x < _player.transform.position.x && scale.x != 1)
            {
                scale.x = 1;
                transform.localScale = scale;
            }
        }
    }

    void DateLoad()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = _date._model;
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
        gameObject.SetActive(false);
        _isActrive = false;
        var item = _itemSpawner.Spawn();
        item.transform.position = transform.position;
    }
}
