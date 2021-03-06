using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : UnityEngine.Object, IObjectPool
{
    T BaseObj = null;
    Transform Parent = null;
    List<T> Pool = new List<T>();
    EnemyDate _date;
    int Index = 0;

    public List<T> GetPool { get => Pool; private set => Pool = value; }

    public void SetBaseObj(T obj, Transform parent)
    {
        BaseObj = obj;
        Parent = parent;
    }

    public void Pooling(T obj)
    {
        obj.InactiveInstantiate();
        Pool.Add(obj);
    }

    public void SetCapacity(int size)
    {
        //既にオブジェクトサイズが大きいときは更新しない
        if (size < Pool.Count) return;



        for (int i = 0; i < size; ++i)
        {
            T Obj = default(T);
            if (Parent)
            {
                Obj = GameObject.Instantiate(BaseObj, Parent);
            }
            else
            {
                Obj = GameObject.Instantiate(BaseObj);
            }
            Pooling(Obj);
        }
    }
    public void LoadDate(EnemyDate date)
    {
        _date = date;
    }
    public T Instantiate()
    {
        T ret = null;
        for (int i = 0; i < Pool.Count; ++i)
        {
            int index = i;

            if (Pool[index].IsActive) continue;
            if (Pool[index] is Enemybase)
            {
                (Pool[index] as Enemybase).GetComponent<EnemyHPController>()._currenthp = _date._maxHP;
                (Pool[index] as Enemybase).GetComponent<SpriteRenderer>().sprite = _date._model;
            }
            Pool[index].Create();
            ret = Pool[index];
            break;
        }

        return ret;
    }
}
