using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPController : MonoBehaviour
{
    public int _currenthp { get; set; } = 5;

    public void Damege(int n)
    {
        _currenthp -= n;
    }
}
