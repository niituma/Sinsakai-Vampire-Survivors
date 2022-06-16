using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Tooltip("スポーンエリア")] Vector2 _spawnArea;
    [SerializeField, Tooltip("スポーンする時間")] float _time = 0.05f;
    [SerializeField] Enemybase _prefab = null;
    [SerializeField] Transform _root = null;
    [SerializeField, Tooltip("最大スポーン数")] int _prefabCapacity = 30;
    [SerializeField, Tooltip("最初に一気にスポーンする割合"), Range(0, 100)] int _startInstantiateRatio = 10;

    float _spawnTime = 0.0f;
    [SerializeField] float _fadeTiming = 0.0f;
    [SerializeField] float _changeEnemyTiming = 5.0f;

    GameObject _player;
    Timer _gameTimer;
    ObjectPool<Enemybase> _enemyPool = new ObjectPool<Enemybase>();
    public EnemyDate _date { get; private set; }
    float StartPrefab = 0f;

    [Header("Bossスポーンの設定")]
    public ObjectPool<EnemyBigBullet> _bigBulletPool = new ObjectPool<EnemyBigBullet>();
    public ObjectPool<EnemySmallBullet> _smallBulletPool = new ObjectPool<EnemySmallBullet>();
    [SerializeField] EnemyBigBullet _bigBullet = null;
    [SerializeField] EnemySmallBullet _smallBullet = null;
    [SerializeField] Transform _bBulletroot = null;
    [SerializeField] Transform _sBulletroot = null;
    [SerializeField] int _bBulletnum = 10;
    [SerializeField] int _sbulletnum = 10;
    [SerializeField] GameObject _boss = null;
    [SerializeField] float _bossSpawnTime = 10f;
    [SerializeField] int _bossHP = 100;
    enum Enemy
    {
        Bat,
        Skull,
        Slime,
        Golem
    }

    Enemy _enemy = Enemy.Bat;

    private void Start()
    {
        _gameTimer = FindObjectOfType<Timer>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyPool.SetBaseObj(_prefab, _root);
        _enemyPool.SetCapacity(_prefabCapacity);
        _bigBulletPool.SetBaseObj(_bigBullet, _bBulletroot);
        _bigBulletPool.SetCapacity(_bBulletnum);
        _smallBulletPool.SetBaseObj(_smallBullet, _sBulletroot);
        _smallBulletPool.SetCapacity(_sbulletnum);
        StartPrefab = _prefabCapacity * ((float)_startInstantiateRatio / 100f);
    }

    private void Update()
    {
        _spawnTime += Time.deltaTime;

        if (_bossSpawnTime == _gameTimer._minute)
        {
            var boss = Instantiate(_boss, SpawnRandomPos() + _player.transform.position, Quaternion.identity);
            boss.transform.SetParent(_root);
            boss.GetComponent<EnemyHPController>()._currenthp = _bossHP;
            GameManager.Instance._enemies.Add(boss.GetComponent<Enemybase>());
            _bossSpawnTime += _bossSpawnTime;
        }

        if (_fadeTiming == _gameTimer._minute)
        {
            StartCoroutine(FadeSpawn());
            _fadeTiming += _fadeTiming;
        }

        if (_spawnTime > _time)
        {
            Spawn();
            _spawnTime -= _time;
        }
    }

    void Spawn()
    {
        if (!_player)
        {
            return;
        }

        if (_changeEnemyTiming <= _gameTimer._minute)
        {
            if ((int)_enemy != System.Enum.GetValues(typeof(Enemy)).Length - 1)
            {
                _enemy++;
                _changeEnemyTiming += _changeEnemyTiming;
                _time -= 0.2f;
            }
        }
        _date = Resources.Load<EnemyDate>($"EnemyDates/Enemy {_enemy}");
        _enemyPool.LoadDate(_date);
        var script = _enemyPool.Instantiate();
        if (!script)
        {
            return;
        }

        var SpawnPos = SpawnRandomPos() + _player.transform.position;

        script.transform.position = SpawnPos;
    }

    private IEnumerator FadeSpawn()
    {
        for (int c = 0; c < 3; ++c)
        {

            for (int i = 0; i < StartPrefab; ++i)
            {
                Spawn();
            }

            yield return new WaitForSeconds(2f);
        }
    }

    Vector3 SpawnRandomPos()
    {
        Vector3 pos = new Vector3();

        float f = Random.value > 0.5f ? -1f : 1f;

        if (Random.value > 0.5f)
        {
            pos.x = Random.Range(-_spawnArea.x, _spawnArea.x);
            pos.y = _spawnArea.y * f;
        }
        else
        {
            pos.x = _spawnArea.x * f;
            pos.y = Random.Range(-_spawnArea.y, _spawnArea.y);
        }

        pos.z = 0;

        return pos;
    }
}
