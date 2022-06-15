using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallBullet : MonoBehaviour, IObjectPool
{
    float _movePower = 10f;
    public int _def { get; set; } = 2;
    GameObject _player;
    Rigidbody2D _rb;

    private void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) > 15f)
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
