using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : Itembase
{
    protected override void GetItem()
    {
        GameManager.Instance.AddExp();
    }
}
