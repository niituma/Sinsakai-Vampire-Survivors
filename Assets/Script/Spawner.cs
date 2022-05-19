using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField, Tooltip("�X�|�[���G���A")] Vector2 _spawnArea;
    [SerializeField, Tooltip("�X�|�[�����鎞��")] float _time = 0.05f;
    [SerializeField] Enemybase _prefab = null;
    [SerializeField] Transform _root = null;
    [SerializeField, Tooltip("�ő�X�|�[����")] int _prefabCapacity = 30;
    [SerializeField, Tooltip("�ŏ��Ɉ�C�ɃX�|�[�����銄��"), Range(0, 100)] int _startInstantiateRatio = 10;

    float _timer = 0.0f;
    Vector3 _popPos = new Vector3(0, 0, 0);
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
        if (!_player)
        {
            return;
        }
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
