using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShotMagic : Skillbase, ISkill
{
    public SkillDef SkillId => SkillDef.ShotMagic;
    [SerializeField] Bullet _bullet = null;
    [SerializeField] AudioClip _attackSE = null;
    int __bulletnum = 1;
    int _prefabCapacity = 30;
    ObjectPool<Bullet> _bulletPool = new ObjectPool<Bullet>();
    public void Setup()
    {
        _cooldown = 0.8f;
        _bulletPool.SetBaseObj(_bullet, gameObject.transform);
        _bulletPool.SetCapacity(_prefabCapacity);
    }
    public void SkillUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer >= _cooldown)
        {
            StartCoroutine(DelayCoroutine());

            _timer -= _cooldown;
        }
    }
    public void Levelup()
    {
        _skillLevel++;
        switch (_skillLevel)
        {
            case 2:
                __bulletnum++;
                break;
            case 3:
                _bullet._maxdamage += _maxdamage + (int)(50f / 100f * _maxdamage);
                _bullet._mindamage = _mindamage + 1;
                break;
            case 4:
                _cooldown -= 0.1f;
                break;
            case 5:
                _bullet._maxdamage = _maxdamage + (int)(50f / 100f * _maxdamage);
                _bullet._mindamage = _mindamage + 1;
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
        GameManager.Instance._playerAttack.Audio.PlayOneShot(_attackSE);
        var script = _bulletPool.Instantiate();

        if (!script)
        {
            return;
        }
        var list = GameManager.Instance._enemies;
        Enemybase target = list.Where(e => e.gameObject.activeSelf)
            .OrderBy(e => Vector3.Distance(e.transform.position, GameManager.Instance._player.gameObject.transform.position)).FirstOrDefault();

        script.transform.position = GameManager.Instance._player.gameObject.transform.position;
        script.Shoot(target);
    }
    private IEnumerator DelayCoroutine()
    {
        for (int i = 0; i < __bulletnum; i++)
        {
            ActiveSkill();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
