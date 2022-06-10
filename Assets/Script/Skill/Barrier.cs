using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Barrier : Skillbase, ISkill
{
    public SkillDef SkillId => SkillDef.Barrier;
    [SerializeField] float _sizeRadius = 3f;
    Vector3 _center;
    GameObject _player = null;
    public void Setup()
    {
        _maxdamage = 3;
        _cooldown = 0.1f;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public void SkillUpdate()
    {
        transform.position = _player.transform.position;
        _timer += Time.deltaTime;
        if (_timer >= _cooldown)
        {
            ActiveSkill();
            _timer -= _cooldown;
        }
    }
    public void Levelup()
    {
        _skillLevel++;
        switch (_skillLevel)
        {
            case 2:
                _maxdamage = _maxdamage *= 2;
                _mindamage = _mindamage *= 2;
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
    }
    public override void ActiveSkill()
    {
        var targets = Physics2D.OverlapCircleAll(GetTargetsRangeCenter(), _sizeRadius).Where(e => e.tag == "Enemy").ToList();
        foreach (var target in targets)
        {
            var damge = Random.Range(_mindamage, _maxdamage);
            target.GetComponent<EnemyHPController>().Damege(damge);
        }
    }
    void OnDrawGizmosSelected()
    {
        // 攻撃範囲を赤い線でシーンビューに表示する
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(GetTargetsRangeCenter(), _sizeRadius);
    }
    Vector3 GetTargetsRangeCenter()
    {
        Vector3 center = this.transform.position + this.transform.forward * _center.z
            + this.transform.up * _center.y
            + this.transform.right * _center.x;
        return center;
    }
}
