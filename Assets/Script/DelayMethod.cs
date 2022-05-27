using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AddOrignalMethod : MonoBehaviour
{
    public IEnumerator DelayMethod(float seconds, Action action)
    {
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
    }
}
