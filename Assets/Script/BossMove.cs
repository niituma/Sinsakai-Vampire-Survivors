using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : Enemybase
{
    bool _isShoot = false;
    [SerializeField] float _shootTime = 5f;
    [SerializeField] Transform _bMuzzle = null;
    [SerializeField] Transform _sMuzzle = null;
    float _timer = 0;
    Spawner _spawner = null;
    AddOrignalMethod Method = new AddOrignalMethod();

    new void Start()
    {
        base.Start();
        _spawner = FindObjectOfType<Spawner>();
    }

    new void Update()
    {
        if (!_isShoot)
        {
            _timer += Time.deltaTime;

            Vector2 Dir = (_player.transform.position - transform.position).normalized;
            _rb.velocity = Dir.normalized * _speed;
        }

        if (_timer >= _shootTime)
        {
            float f = Random.value > 0.5f ? -1f : 1f;
            if (f < 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    var bullet = _spawner._smallBulletPool.Instantiate();
                    bullet.transform.position = _sMuzzle.position;
                }
            }
            else
            {
                _isShoot = true;
                _rb.velocity = Vector2.zero;
                var bullet = _spawner._bigBulletPool.Instantiate();
                bullet.transform.position = _bMuzzle.position;
                StartCoroutine(Method.DelayMethod(1f, () =>
                {
                    Vector2 dir = _player.transform.position - bullet.transform.position;
                    bullet.GetComponent<Rigidbody2D>().velocity = dir.normalized * bullet.Speed;
                }));
                StartCoroutine(Method.DelayMethod(3f, () => _isShoot = false));
            }

            _timer -= _shootTime;
        }
        

        LeftRightDir();
    }
}
