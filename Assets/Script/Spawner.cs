using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Tooltip("スポーンエリア")] Vector2 _spawnArea;
    [SerializeField, Tooltip("スポーンする時間")] float _time = 0.05f;
    [SerializeField] Enemybase _prefab = null;
    [SerializeField] Transform _root = null;
    [SerializeField, Tooltip("最大スポーン数")] int _prefabCapacity = 30;
    [SerializeField, Tooltip("最初に一気にスポーンする割合"), Range(0, 100)] int _startInstantiateRatio = 10;

    float _timer = 0.0f;
    float _fadetimer = 0.0f;
    Vector3 _position = new Vector3(0, 0, 0);
    GameObject _player;
    ObjectPool<Enemybase> _enemyPool = new ObjectPool<Enemybase>();

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyPool.SetBaseObj(_prefab, _root);
        _enemyPool.SetCapacity(_prefabCapacity);

        var StartPrefab = _prefabCapacity * ((float)_startInstantiateRatio / 100f);

        for (int i = 0; i < StartPrefab; ++i) Spawn();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _fadetimer += Time.deltaTime;
        if (_fadetimer > 20f)
        {
            FadeSpawn();
            _fadetimer = 0.0f;
        }

        if (_timer > _time)
        {
            Spawn();
            _timer -= _time;
        }
    }

    void Spawn()
    {
        if (!_player)
        {
            return;
        }
        var script = _enemyPool.Instantiate();

        if (!script)
        {
            return;
        }

        EnemyDate _date = Resources.Load<EnemyDate>("EnemyDates/Enemy Bat");
        script.GetComponent<SpriteRenderer>().sprite = _date._model;
        script.GetComponent<Animator>().runtimeAnimatorController = _date._animator;

        _position = SpawnRandomPos() + _player.transform.position;

        script.transform.position = _position;
    }

    void FadeSpawn()
    {
        for (int i = 0; i < 20; ++i)
        {
            Spawn();
        }
        return;
    }

    Vector3 SpawnRandomPos()
    {
        Vector3 pos = new Vector3();

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;

        if (UnityEngine.Random.value > 0.5f)
        {
            pos.x = UnityEngine.Random.Range(-_spawnArea.x, _spawnArea.x);
            pos.y = _spawnArea.y * f;
        }
        else
        {
            pos.x = _spawnArea.x * f;
            pos.y = UnityEngine.Random.Range(-_spawnArea.y, _spawnArea.y);
        }

        pos.z = 0;

        return pos;
    }
}
