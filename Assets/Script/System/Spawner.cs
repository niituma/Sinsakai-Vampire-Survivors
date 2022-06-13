using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Tooltip("スポーンエリア")] Vector2 _spawnArea;
    [SerializeField, Tooltip("スポーンする時間")] float _time = 0.05f;
    [SerializeField] Enemybase _prefab = null;
    [SerializeField] Transform _root = null;
    [SerializeField, Tooltip("最大スポーン数")] int _prefabCapacity = 30;
    [SerializeField, Tooltip("最初に一気にスポーンする割合"), Range(0, 100)] int _startInstantiateRatio = 10;

    float _countTimer = 0.0f;
    [SerializeField] float _fadeTiming = 0.0f;
    [SerializeField] float _changeEnemyTiming = 5.0f;

    bool _isFade = false;
    GameObject _player;
    Timer _gameTimer;
    ObjectPool<Enemybase> _enemyPool = new ObjectPool<Enemybase>();
    public EnemyDate _date { get; private set; }
    AddOrignalMethod Method = new AddOrignalMethod();

    [Header("Bossスポーンの設定")]
    [SerializeField] GameObject _boss;
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
        var StartPrefab = _prefabCapacity * ((float)_startInstantiateRatio / 100f);

        for (int i = 0; i < StartPrefab; ++i) Spawn();
    }

    private void Update()
    {
        _countTimer += Time.deltaTime;

        if (_bossSpawnTime == _gameTimer._minute)
        {
            var boss = Instantiate(_boss, SpawnRandomPos() + _player.transform.position, Quaternion.identity);
            boss.transform.SetParent(_root);
            boss.GetComponent<EnemyHPController>()._currenthp = _bossHP;
            _bossSpawnTime += 1;
        }

        if (_fadeTiming == _gameTimer._minute)
        {
            FadeSpawn();
            _fadeTiming += _fadeTiming;
        }

        if (_countTimer > _time)
        {
            Spawn();
            _countTimer -= _time;
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

    void FadeSpawn()
    {
        for (int i = 0; i < 10; ++i)
        {
            Spawn();
        }

        return;
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
