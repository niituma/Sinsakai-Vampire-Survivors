using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skillbase : MonoBehaviour
{
    public int _mindamage { get; set; } = 1;
    public int _maxdamage { get; set; } = 4;

    protected float _timer = 0;

    public float _cooldown { get; set; } = 2;
    protected AddOrignalMethod Method = new AddOrignalMethod();
    public virtual void ActiveSkill()
    {

    }
}
