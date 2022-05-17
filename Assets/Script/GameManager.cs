using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Slider _expSlider;
    int _expValue;

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
        if (_expSlider.maxValue == _expValue)
        {
            _expSlider.value = 0;
            _expValue = +_expValue;
            _expSlider.maxValue = _expValue;
        }
    }
    public void AddExp()
    {
        _expSlider.value++;
    }
}
