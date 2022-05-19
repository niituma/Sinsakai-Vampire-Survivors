using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] Slider _expSlider;
    [SerializeField] int _expValue = 5;
    [SerializeField] TextMeshProUGUI _levelText;
    int _level = 0;

    public Slider ExpSlider { get; set; }
    GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _expSlider.maxValue = _expValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 経験値取得
    /// </summary>
    public void AddExp(int addexp)
    {
        _expSlider.value += addexp;

        //レベルアップ
        if (_expSlider.value == _expValue)
        {
            ++_level;
            _levelText.text = "Lv." + _level.ToString("D2");
            _expSlider.value = 0;
            _expValue += _expValue;
            _expSlider.maxValue = _expValue;
        }
    }
}
