using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] GameObject _weap;
    Skillbase skill;
    float _timer = 0;

    private void Start()
    {
        skill = _weap.GetComponent<Skillbase>();
    }
    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > skill._cooldown)
        {
            skill.ActiveSkill();
            _timer -= skill._cooldown;
        }

    }
}
