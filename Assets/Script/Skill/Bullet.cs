using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IObjectPool
{
    public int _mindamage { get; set; } = 1;
    public int _maxdamage { get; set; } = 4;

    [SerializeField] float _speed = 10f;
    float _timer = 0f;
    Enemybase _target;
    Vector3 _shootVec;
    public void Shoot(Enemybase target)
    {
        _target = target;
        if (_target == null) return;

        _shootVec = _target.transform.position - GameManager.Instance._player.transform.position;
        _shootVec.Normalize();
    }
    void Update()
    {
        transform.position += _shootVec * _speed * Time.deltaTime;
        
        _timer += Time.deltaTime;
        if (_timer > 1.0f)
        {
            Destroy();
        }
    }
    void OnDisable()
    {
        _timer = 0.0f;
    }
        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            var damge = Random.Range(_mindamage, _maxdamage);
            collision.gameObject.GetComponent<EnemyHPController>().Damege(damge);
            Destroy();
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
        gameObject.SetActive(false);
        _isActrive = false;
    }
}
