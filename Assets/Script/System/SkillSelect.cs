using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillSelect : MonoBehaviour
{
    [SerializeField] List<GameObject> _selectList;

    List<SkillSelectTable> _selectTable = new List<SkillSelectTable>();
    List<UnityEngine.UI.Text> _selectText = new List<UnityEngine.UI.Text>();
    int _maxLevel = 5;
    CanvasGroup _canvas;

    public bool _isSelect { get; private set; } = false;

    bool _startEvent = false;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    void Start()
    {
        for (int i = 0; i < _selectList.Count; ++i)
        {
            _selectTable.Add(null);
            _selectText.Add(_selectList[i].GetComponentInChildren<UnityEngine.UI.Text>());
            {
                var index = i;
                var btn = _selectList[i].GetComponentInChildren<UnityEngine.UI.Button>();
                btn.onClick.AddListener(() =>
                {
                    if (_canvas.alpha == 0) return;
                    OnClick(index);
                });
            }
        }
    }

    private void Update()
    {
        if (_startEvent)
        {
            SelectStart();
            _startEvent = false;
        }
    }

    public void SelectStartDelay()
    {
        _startEvent = true;
    }

    public void SelectStart()
    {
        _isSelect = true;
        _canvas.alpha = 1;

        List<SkillSelectTable> table = new List<SkillSelectTable>();
        var list = GameData.SkillSelectTable.Where(s => _maxLevel != s.Level);
        int totalProb = list.Sum(s => s.Probability);

        for (int i = 0; i < _selectList.Count; ++i)
        {
            _selectList[i].SetActive(true);
            _selectTable[i] = null;
            _selectText[i].text = "";
        }
        int rand = Random.Range(0, totalProb);
        var selectnum = 0;
        if (list.Count() >= 3)
        {
            selectnum = 3;
        }
        else if (list.Count() == 2)
        {
            selectnum = 2;
        }
        else
        {
            selectnum = 1;
        }
        for (int i = 0; i < _selectList.Count; ++i)
        {
            while (_selectTable[i] == null && selectnum > 0)
            {
                foreach (var s in list)
                {
                    if (rand < s.Probability)
                    {
                        _selectTable[i] = s;
                        if (s.Type != SelectType.Execute)
                        {
                            _selectText[i].text = $" {s.Name}  Lv.{s.Level + 1}";
                        }
                        else
                        {
                            _selectText[i].text = s.Name;
                        }
                        list = list.Where(ls => !(ls.Type == s.Type && ls.TargetId == s.TargetId));
                        rand -= s.Probability;
                        selectnum--;
                        break;
                    }
                }
                if (_selectTable[i] != null)
                {
                    break;
                }
                rand = Random.Range(0, totalProb);
            }

            if (_selectTable[i] == null)
            {
                _selectList[i].SetActive(false);
            }
        }
    }

    public void OnClick(int index)
    {
        if (_selectTable[index] == null)
        {
            return;
        }
        if (_selectTable[index].Type == SelectType.Skill || _selectTable[index].Type == SelectType.Passive)
        {
            _selectTable[index].Level++;
        }
        GameManager.Instance.LevelUpSelect(_selectTable[index]);
        _isSelect = false;
        GameManager.Instance.IsPause();
        _canvas.alpha = 0;
    }
}
