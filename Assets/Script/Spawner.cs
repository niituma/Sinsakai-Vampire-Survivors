using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] Vector2 _spawnArea;
    [SerializeField] float _time = 0.05f;
    [SerializeField] Enemybase _prefab = null;
    [SerializeField] Transform _root = null;
    [SerializeField] int _prefabCapacity = 30;

    float _timer = 0.0f;
    Vector3 _popPos = new Vector3(0, 0, 0);
    GameObject _player;
    ObjectPool<Enemybase> _enemyPool = new ObjectPool<Enemybase>();

    private void Start()
    {
        _enemyPool.SetBaseObj(_prefab, _root);
        _enemyPool.SetCapacity(_prefabCapacity);
        _player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < _prefabCapacity / 3; ++i) Spawn();
    }

    private void Update()
    {
        bool IsmaxprefabPool = true;

        foreach (var a in _enemyPool.GetPool)
        {
            if (!a.gameObject.activeSelf)
            {
                IsmaxprefabPool = false;
            }
        }

        if (!IsmaxprefabPool)
        {
            _timer += Time.deltaTime;
            if (_timer > _time)
            {
                Spawn();
                _timer -= _time;
            }
        }
    }

    void Spawn()
    {
        var script = _enemyPool.Instantiate();
        /*
        var go = GameObject.Instantiate(_prefab);
        var script = go.GetComponent<Enemy>();
        */
        _popPos.x = _player.transform.position.x + UnityEngine.Random.Range(-_spawnArea.x, _spawnArea.x);
        _popPos.y = _player.transform.position.y + UnityEngine.Random.Range(-_spawnArea.y, _spawnArea.y);
        script.transform.position = _popPos;
    }
}
