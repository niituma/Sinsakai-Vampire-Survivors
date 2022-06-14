using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    AddOrignalMethod Method = new AddOrignalMethod();
    EffectSpawn _effectSpawn;
    public int _currenthp { get; set; } = 5;
    private void Start()
    {
        _effectSpawn = FindObjectOfType<EffectSpawn>();
    }
    public void Damege(int n)
    {
        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(Method.DelayMethod(0.3f, () => GetComponent<SpriteRenderer>().color = Color.white));
        }
        else
        {
            var eff = _effectSpawn.Spawn();
            eff.transform.position = transform.position;
        }
        _currenthp -= n;
    }
}
