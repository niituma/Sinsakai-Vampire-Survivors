using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultPoint : MonoBehaviour
{
    TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = GameManager.Instance._result_timeminute.ToString("D2") + ":" + ((int)GameManager.Instance._result_timesecond).ToString("D2");
    }
}
