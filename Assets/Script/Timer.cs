using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    float _second = 0;
    int _minute = 0;
    float _resetTime = 0;
    TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

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
