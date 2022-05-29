using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    public int _currenthp { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (_currenthp <= 0)
        {
            GetComponent<Enemybase>().Destroy();
        }
    }

    public void Damege(int n)
    {
        _currenthp -= n;
    }
}
