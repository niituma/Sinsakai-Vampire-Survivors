using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    AddOrignalMethod Method = new AddOrignalMethod();
    public int _currenthp { get; set; } = 5;

    public void Damege(int n)
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        StartCoroutine(Method.DelayMethod(0.3f, () => GetComponent<SpriteRenderer>().color = Color.white));
        _currenthp -= n;
    }
}
