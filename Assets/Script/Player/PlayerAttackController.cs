using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] List<GameObject> _skills = new List<GameObject>();

    private void Awake()
    {
        AddSkill(2);
    }
    // Update is called once per frame
    void Update()
    {
        foreach(var skill in _skills)
        {
            skill.GetComponent<ISkill>().SkillUpdate();
        }
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
                    newskill = Instantiate(Resources.Load<GameObject>("Skills/Whips"), transform.position, Quaternion.identity);
                    break;

                case SkillDef.ShotBullet:
                    newskill = Instantiate(Resources.Load<GameObject>("Skills/ShotBullet"), transform.position, Quaternion.identity);
                    break;

                case SkillDef.AreaAttack:
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
