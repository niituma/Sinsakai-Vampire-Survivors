using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Slider _expSlider;
    [SerializeField] int _expValue = 5;

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
            _expSlider.value = 0;
            _expValue += _expValue;
            _expSlider.maxValue = _expValue;
        }
    }
}
