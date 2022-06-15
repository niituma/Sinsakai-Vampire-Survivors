using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GameManager
{
    static private GameManager _instance = new GameManager();
    static public GameManager Instance => _instance;
    private GameManager() { }
    static public int Level => _instance._level;
    public List<Enemybase> _enemies { get; private set; } = new List<Enemybase>();

    int _stackLevelup = 0;
    int _level = 0;
    int _expMaxValue = 1;
    float _expValue = 1;
    float _passiveUpValue = 0;
    public Slider _expSlider { get; set; }
    public TextMeshProUGUI _levelText { get; set; }
    public GameObject _finishPanel { get; set; }
    public GameObject _pausePanal { get; set; }
    public bool _isPause { get; private set; } = false;
    public int _result_timeminute { get; set; }
    public int _result_timesecond { get; set; }

    public PlayerController _player { get; private set; } = null;
    public void SetPlayer(PlayerController p) { _player = p; }
    PlayerAttackController _playerAttack = null;
    SkillSelect _sklSelect = null;

    // Start is called before the first frame update
    public void Setup()
    {
        _enemies = GameObject.FindObjectsOfType<Enemybase>(true).ToList();
        _playerAttack = _player.GetComponent<PlayerAttackController>();
        _sklSelect = GameObject.FindObjectOfType<SkillSelect>();
        _expSlider.maxValue = _expMaxValue;
        _isPause = false;
    }

    // Update is called once per frame
    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _playerAttack && !_sklSelect._isSelect)
        {
            IsPause();
        }
    }

    public void FinishGame()
    {
        _isPause = true;
        var timer = GameObject.FindObjectOfType<Timer>();
        _result_timeminute = timer._minute;
        _result_timesecond = (int)timer._second;
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
                _playerAttack.AddSkill(table.TargetId);
                break;

            case SelectType.Passive:
                AddPassive(table.TargetId);
                break;

            case SelectType.Execute:
                _player.GetComponent<PlayerHPController>().Heel(30);
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
    void AddPassive(int PassiveId)
    {
        switch ((PassiveDef)PassiveId)
        {
            case PassiveDef.MaxHPUp:
                var UpHP = (10f / 100f) * _player.gameObject.GetComponent<PlayerHPController>().Maxhp;
                _player.GetComponent<PlayerHPController>().MaxHPUp(UpHP);
                break;
            case PassiveDef.SpeedUp:
                var speed = _player.gameObject.GetComponent<PlayerController>().Speed;
                _player.gameObject.GetComponent<PlayerController>()._addSpeed += (20f / 100f) * speed;
                break;
            case PassiveDef.ExpUp:
                _passiveUpValue ++;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 経験値取得
    /// </summary>
    public void AddExp()
    {
        _expSlider.value += _expValue + _passiveUpValue;

        //レベルアップ
        if (_expSlider.value == _expSlider.maxValue)
        {
            ++_level;
            _sklSelect.SelectStart();
            _levelText.text = "Lv." + _level.ToString("D2");
            _expSlider.value = 0;
            _expSlider.maxValue += 0;

            _isPause = true;
        }
    }
}
