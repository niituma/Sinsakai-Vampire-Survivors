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
    [SerializeField] GameObject _FinishPanel;
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

    public void FinishGame()
    {
        var timer = FindObjectOfType<Timer>();
        _FinishPanel.SetActive(true);
        if (timer._minute >= 2)
        {
            _FinishPanel.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            _FinishPanel.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// �o���l�擾
    /// </summary>
    public void AddExp(int addexp)
    {
        _expSlider.value += addexp;

        //���x���A�b�v
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
