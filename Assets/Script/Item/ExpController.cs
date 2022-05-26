using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : Itembase
{
    [SerializeField] int _expvalue = 1;
    protected override void GetItem()
    {
        var gamemanager = FindObjectOfType<GameManager>();
        gamemanager.AddExp(_expvalue);
    }
}