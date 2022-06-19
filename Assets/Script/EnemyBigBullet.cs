using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigBullet : MonoBehaviour, IObjectPool
{
    GameObject _player;
    [SerializeField] float _speed = 5f;
    public float Speed { get => _speed; }
    Rigidbody2D _rb;
    private void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) > 15f || !FindObjectOfType<BossMove>())
        {
            Destroy();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _player.GetComponent<PlayerHPController>().Damege();
            Destroy();
        }
    }
    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;


    public void InactiveInstantiate()
    {
        _player = GameManager.Instance._player.gameObject;
        _rb = GetComponent<Rigidbody2D>();
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
    }
}
