using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : Enemybase
{
    bool _isShoot = false;
    bool _runAway = false;
    [SerializeField] float _shootTime = 5f;
    [SerializeField] Transform _bMuzzle = null;
    [SerializeField] Transform _sMuzzle = null;
    [SerializeField] int _runAwayTime = 3;
    [SerializeField] Vector2 _respawnArea;
    [SerializeField] int _runAwayTimelimit = 0;
    float _timer = 0;
    Spawner _spawner = null;
    AddOrignalMethod Method = new AddOrignalMethod();
    Timer _gametime = null;

    new void Start()
    {
        base.Start();
        _spawner = FindObjectOfType<Spawner>();
    }

    void OnEnable()
    {
        if (_gametime == null)
        {
            _gametime = FindObjectOfType<Timer>();
            _runAwayTimelimit = _gametime._minute + _runAwayTime;
        }
        _timer = 0;
    }

    new void Update()
    {
        if (_HP._currenthp <= 0)
        {
            for (int i = 0; i < 30; ++i)
            {
                var item = _itemSpawner.Spawn();
                if (item)
                {
                    item.transform.position = ItemSpawnRandomPos() + transform.position;
                }
            }
            Destroy();
        }

        if (!_runAway && _gametime._minute == _runAwayTimelimit)
        {
            _runAway = true;
        }

        if (!_isShoot)
        {
            _timer += Time.deltaTime;

            Vector2 Dir = (_player.transform.position - transform.position).normalized;
            var speed = _runAway ? _speed * -2 : _speed;
            _rb.velocity = Dir.normalized * speed;
        }

        if (Vector2.Distance(transform.position, GameManager.Instance._player.transform.position) > 15f)
        {
            if (_runAway)
            {
                Destroy();
            }
            else
            {
                transform.position = SpawnRandomPos() + GameManager.Instance._player.transform.position;
            }

        }

        Attack();

        LeftRightDir();
    }
    void Attack()
    {
        if (_timer >= _shootTime && !_runAway)
        {
            float f = Random.value > 0.5f ? -1f : 1f;
            if (f < 0)
            {
                for (int i = 0; i < 10; i++)
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
    }

    Vector3 SpawnRandomPos()
    {
        Vector3 pos = new Vector3();

        float f = Random.value > 0.5f ? -1f : 1f;

        if (Random.value > 0.5f)
        {
            pos.x = Random.Range(-_respawnArea.x, _respawnArea.x);
            pos.y = _respawnArea.y * f;
        }
        else
        {
            pos.x = _respawnArea.x * f;
            pos.y = Random.Range(-_respawnArea.y, _respawnArea.y);
        }

        pos.z = 0;

        return pos;
    }
    Vector3 ItemSpawnRandomPos()
    {
        Vector3 pos = new Vector3();

        float f = Random.value > 0.5f ? -1f : 1f;

        if (Random.value > 0.5f)
        {
            pos.x = Random.Range(-1f, 1f);
            pos.y = 1 * f;
        }
        else
        {
            pos.x = 1 * f;
            pos.y = Random.Range(-1f, 1f);
        }

        pos.z = 0;

        return pos;
    }
}
