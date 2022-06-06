using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    static private GameManager _instance = new GameManager();
    static public GameManager Instance => _instance;
    static public int Level => _instance._level;

    [SerializeField] Slider _expSlider;
    [SerializeField] int _expValue = 5;
    [SerializeField] TextMeshProUGUI _levelText;
    [SerializeField] GameObject _finishPanel;
    [SerializeField] GameObject _pausePanal;
    [SerializeField] GameObject _skillSelectPanal;
    int _stackLevelup = 0;
    int _level = 0;
    public bool _isPause { get; private set; } = false;

    List<int> _passive = new List<int>();

    public Slider ExpSlider { get; set; }
    PlayerController _player = null;
    SkillSelect _sklSelect = null;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        _sklSelect = FindObjectOfType<SkillSelect>();
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

    public void LevelUpSelect(SkillSelectTable table)
    {
        switch (table.Type)
        {
            case SelectType.Skill:
                _player.AddSkill(table.TargetId);
                break;

            case SelectType.Passive:
                _passive.Add(table.TargetId);
                break;

            case SelectType.Execute:
                //TODO:
                break;
        }

        if (_stackLevelup > 0)
        {
            _sklSelect.SelectStartDelay();
            _stackLevelup--;
        }
        else
        {
            Time.timeScale = 1;
        }
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
            _sklSelect.SelectStart();
            _levelText.text = "Lv." + _level.ToString("D2");
            _expSlider.value = 0;
            _expValue += _expValue;
            _expSlider.maxValue = _expValue;

            _skillSelectPanal.SetActive(true);
            _isPause = true;
        }
    }
}
