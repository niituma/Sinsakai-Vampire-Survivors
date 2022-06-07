using UnityEngine;
using UnityEditor;

public interface IObjectPool
{
    bool IsActive { get; }
    void InactiveInstantiate();
    void Create();
    void Destroy();
}
