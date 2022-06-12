using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IObjectPool
{
    public int _mindamage { get; set; } = 3;
    public int _maxdamage { get; set; } = 5;
    float _movePower = 10f;
    int _def = 2;
    int _damageCount = 0;
    GameObject _player;
    Rigidbody2D _rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _damageCount++;
            var damge = Random.Range(_mindamage, _maxdamage);
            collision.gameObject.GetComponent<EnemyHPController>().Damege(damge);
            if (_damageCount >= _def)
            {
                Destroy();
            }
        }
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) > 10f)
        {
            Destroy();
        }
    }
    //ObjectPool
    bool _isActrive = false;
    public bool IsActive => _isActrive;
    public void InactiveInstantiate()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
        _isActrive = false;
    }
    public void Create()
    {
        gameObject.SetActive(true);
        _isActrive = true;
        Vector3 dir = new Vector3(Random.Range(0.5f, -0.5f), 1, 0).normalized;
        _rb.AddForce(dir * _movePower, ForceMode2D.Impulse);
        _rb.AddTorque(300f, ForceMode2D.Force);
    }
    public void Destroy()
    {
        gameObject.SetActive(false);
        _isActrive = false;
    }
}
