using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSpawn : MonoBehaviour
{
    [SerializeField] Effect _prefab = null;
    [SerializeField] Transform _root = null;
    [SerializeField, Tooltip("ç≈ëÂÉXÉ|Å[Éìêî")] int _prefabCapacity = 30;
    ObjectPool<Effect> _effPool = new ObjectPool<Effect>();
    void Start()
    {
        _effPool.SetBaseObj(_prefab, _root);
        _effPool.SetCapacity(_prefabCapacity);
    }

    public Effect Spawn()
    {
        var script = _effPool.Instantiate();
        
        return script;
    }
}
