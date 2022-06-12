using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bomber : Skillbase, ISkill
{
    public SkillDef SkillId => SkillDef.Bomber;
    GameObject _player = null;
    [SerializeField] Bomb _Bomb = null;
    int _ballnum = 3;
    int _prefabCapacity = 30;
    ObjectPool<Bomb> _BombPool = new ObjectPool<Bomb>();
    public void Setup()
    {
        _cooldown = 5f;
        _player = GameObject.FindGameObjectWithTag("Player");
        _BombPool.SetBaseObj(_Bomb, gameObject.transform);
        _BombPool.SetCapacity(_prefabCapacity);
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
        if (!_player)
        {
            return;
        }
        for (int i = 0; i < _ballnum; ++i)
        {
            var script = _BombPool.Instantiate();
            if (!script)
            {
                break;
            }
            script.transform.position = _player.transform.position;
        }


    }
}
