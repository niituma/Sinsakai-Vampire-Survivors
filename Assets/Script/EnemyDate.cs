using UnityEngine;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyDate : ScriptableObject
{
	public string _enemyName;
	public Sprite _model;
	//public AnimatorController _animator;
	public int _maxHP;
}
