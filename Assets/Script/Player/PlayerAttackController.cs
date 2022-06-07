using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] GameObject _weap;
    Skillbase skill;
    float _timer = 0;
    List<GameObject> _skills = new List<GameObject>();

    private void Awake()
    {
        AddSkill(1);
    }
    //private void Start()
    //{
    //    skill = _weap.GetComponent<Skillbase>();
    //}
    // Update is called once per frame
    void Update()
    {
        foreach(var skill in _skills)
        {
            skill.GetComponent<ISkill>().SkillUpdate();
        }
        //_timer += Time.deltaTime;
        //if (_timer > skill._cooldown)
        //{
        //    skill.ActiveSkill();
        //    _timer -= skill._cooldown;
        //}
    }
    public void AddSkill(int skillId)
    {
        var having = _skills.Where(s => s.GetComponent<ISkill>().SkillId == (SkillDef)skillId);
        if (having.Count() > 0)
        {
            having.Single().GetComponent<ISkill>().Levelup();
        }
        else
        {
            GameObject newskill = null;
            switch ((SkillDef)skillId)
            {
                case SkillDef.MeleeWeapon:
                    //newskill = new MeleeWeapon();
                    GameObject skill = new GameObject();
                    skill.AddComponent<MeleeWeapon>();
                    break;

                case SkillDef.ShotBullet:
                    //newskill = new ShotBullet();
                    break;

                case SkillDef.AreaAttack:
                    //newskill = new AreaAttack();
                    break;
            }

            if (newskill != null)
            {
                newskill.GetComponent<ISkill>().Setup();
                _skills.Add(newskill);
            }
        }
    }
}
