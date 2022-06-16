using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bomber : Skillbase, ISkill
{
    public SkillDef SkillId => SkillDef.Bomber;
    [SerializeField] Bomb _Bomb = null;
    [SerializeField] AudioClip _attackSE = null;
    int _ballnum = 3;
    int _prefabCapacity = 30;
    ObjectPool<Bomb> _BombPool = new ObjectPool<Bomb>();
    public void Setup()
    {
        _cooldown = 5f;
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
                _ballnum++;
                break;
            case 3:
                _Bomb._maxdamage = _maxdamage + (int)((50f / 100f) * _maxdamage);
                _Bomb._mindamage = _mindamage + 1;
                break;
            case 4:
                _Bomb._def++;
                break;
            case 5:
                _ballnum++;
                break;
            default:
                break;
        }
    }
    public override void ActiveSkill()
    {
        if (!GameManager.Instance._player.gameObject)
        {
            return;
        }
        for (int i = 0; i < _ballnum; ++i)
        {
            GameManager.Instance._playerAttack.Audio.PlayOneShot(_attackSE);
            var script = _BombPool.Instantiate();
            if (!script)
            {
                break;
            }
            script.transform.position = GameManager.Instance._player.gameObject.transform.position;
        }


    }
}
