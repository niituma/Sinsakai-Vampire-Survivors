using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelItem : Itembase
{
    [SerializeField] int _heelvalue = 1;
    protected override void GetItem()
    {
        var HP = FindObjectOfType<PlayerHPController>();
        HP.Heel(_heelvalue);
    }
}
