using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Itembase _prefab = null;
    [SerializeField] Transform _root = null;
    [SerializeField, Tooltip("ç≈ëÂÉXÉ|Å[Éìêî")] int _prefabCapacity = 30;
    ObjectPool<Itembase> _itemPool = new ObjectPool<Itembase>();
    // Start is called before the first frame update
    void Start()
    {
        _itemPool.SetBaseObj(_prefab, _root);
        _itemPool.SetCapacity(_prefabCapacity);
    }

    public Itembase Spawn()
    {
        var script = _itemPool.Instantiate();
        /*
        var go = GameObject.Instantiate(_prefab);
        var script = go.GetComponent<Enemy>();
        */
        return script;
    }
}
