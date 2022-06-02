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
    [SerializeField] GameObject _finishPanel;
    [SerializeField] GameObject _pausePanal;
    [SerializeField] GameObject _skillSelectPanal;
    int _level = 0;
    public bool _isPause { get; private set; } = false;

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
        if (Input.GetKeyDown(KeyCode.Escape) && _player && !_skillSelectPanal)
        {
            IsPause();
        }
    }

    public void FinishGame()
    {
        _isPause = true;
        var timer = FindObjectOfType<Timer>();
        _finishPanel.SetActive(true);
        if (timer._minute >= 2)
        {
            _finishPanel.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            _finishPanel.transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void IsPause()
    {
        _isPause = !_isPause;
        _pausePanal.SetActive(_isPause);
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

            _skillSelectPanal.SetActive(true);
            _isPause = true;
        }
    }
}
