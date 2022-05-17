using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HPController : MonoBehaviour
{
    [SerializeField, Tooltip("ñ≥ìGÉÇÅ[Éh")] bool _godMode;
    [SerializeField, Tooltip("ç≈ëÂHP")] float _maxhp = 200;
    float _currenthp;
    float _damage = 10;

    [SerializeField] Slider _slider;

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
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Damege();
        }
    }
    void Damege()
    {
        if (!_godMode)
        {
            var damage = _damage;
            _currenthp = _currenthp - damage;
            float value = _currenthp / _maxhp;
            DOTween.To(() => _slider.value, x => _slider.value = x, value, 0.5f);
        }
    }
}
