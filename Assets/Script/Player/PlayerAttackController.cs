using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] GameObject _weap;
    float _timer = 0;
    AddOrignalMethod Method = new AddOrignalMethod();

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3f)
        {
            WeapActive();
            _timer -= 3f;
        }

    }
    void WeapActive()
    {
        _weap.SetActive(true);
        StartCoroutine(Method.DelayMethod(0.5f, () => _weap.SetActive(false)));
    }

}
