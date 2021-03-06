using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHPController : MonoBehaviour
{
    [SerializeField, Tooltip("無敵モード")] bool _godMode;
    [SerializeField, Tooltip("最大HP")] float _maxhp  = 200;
    float _currenthp = 0f;
    float _damage = 5f;
    [SerializeField, Tooltip("ダメージを受けたら一定時間無敵にする時間")] float _notdamageTime = 0.1f;
    bool _isdamaging = false;
    AddOrignalMethod Method = new AddOrignalMethod();

    [SerializeField] Slider _slider;

    public float Maxhp { get => _maxhp; set => _maxhp = value; }

    // Start is called before the first frame update
    void Start()
    {
        _slider.value = 1;
        _currenthp = _maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currenthp <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.FinishGame();
        }
    }
    public void Damege()
    {
        if (!_godMode && !_isdamaging)
        {
            _isdamaging = true;
            var damage = _damage;
            _currenthp = _currenthp - damage;
            _slider.value = _currenthp / _maxhp;
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(Method.DelayMethod(_notdamageTime, () =>
            {
                _isdamaging = false;
                GetComponent<SpriteRenderer>().color = Color.white;
            }
            ));
        }
    }
    public void Heel(float value)
    {
        var heel = value;
        _currenthp = Mathf.Min(_currenthp + heel, _maxhp);
        _slider.value = _currenthp / _maxhp;
    }

    public void MaxHPUp(float addhp)
    {
        _maxhp += addhp;
        _slider.value = _currenthp / (int)_maxhp;
    }
}
