using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Skillbase, ISkill
{
    public SkillDef SkillId => SkillDef.MeleeWeapon;
    List<GameObject> _weaps = new List<GameObject>();
    public void Setup()
    {
        var skill = Instantiate(Resources.Load<GameObject>("Skills/Whip"), transform.position, Quaternion.identity);
        skill.transform.SetParent(transform);
        skill.GetComponent<WhipAttack>()._maxdamage = _maxdamage;
        skill.GetComponent<WhipAttack>()._mindamage = _mindamage;
        _weaps.Add(skill);
    }
    public void SkillUpdate()
    {
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
                _weaps.Add(Instantiate(Resources.Load<GameObject>("Skills/Whip"), transform.position, Quaternion.identity));
                break;
            case 3:
                _maxdamage += (50 / 100) * _maxdamage;
                _mindamage += 1;
                foreach (var a in _weaps)
                {
                    a.GetComponent<WhipAttack>()._maxdamage = _maxdamage;
                    a.GetComponent<WhipAttack>()._mindamage = _mindamage;
                }
                break;
            case 4:
                _cooldown -= 1;
                break;
            case 5:
                _maxdamage += (50 / 100) * _maxdamage;
                _mindamage += 1;
                foreach (var a in _weaps)
                {
                    a.GetComponent<WhipAttack>()._maxdamage = _maxdamage;
                    a.GetComponent<WhipAttack>()._mindamage = _mindamage;
                }
                break;
            default:
                break;
        }
    }


    public override void ActiveSkill()
    {
        PlayerController _moveController = FindObjectOfType<PlayerController>();

        var rote = transform.rotation;
        var firstRote = 0f;
        var secondRote = 0f;

        if (_moveController.Lastdir.y == 1)
        {
            firstRote = 90f;
        }
        else if (_moveController.Lastdir.y == -1)
        {
            firstRote = 270f;
        }
        else if (_moveController.Lastdir.x == 1)
        {
            firstRote = 0f;
        }
        else if (_moveController.Lastdir.x == -1)
        {
            firstRote = 180f;
        }

        foreach (var a in _weaps)
        {
            rote = Quaternion.Euler(0.0f, 0.0f, firstRote + secondRote);
            a.transform.position = _moveController.gameObject.transform.position;
            a.transform.rotation = rote;
            a.SetActive(true);
            StartCoroutine(Method.DelayMethod(0.15f, () => a.SetActive(false)));
            secondRote += 180;
        }
    }
}