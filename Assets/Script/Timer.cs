using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float _second = 0;
    public int _minute { get; private set; }
    float _resetTime = 0;
    [SerializeField] TextMeshProUGUI _text;

    // Update is called once per frame
    void Update()
    {
        _second += Time.deltaTime;
        if (_second >= 60f)
        {
            _minute++;
            _second = 0;
        }

        _text.text = _minute.ToString("D2") + ":" + ((int)_second).ToString("D2");
    }
}
