using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPoint : MonoBehaviour
{
    Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = GameManager.Instance._result_timeminute.ToString("D2") + ":" + ((int)GameManager.Instance._result_timesecond).ToString("D2");
    }
}
