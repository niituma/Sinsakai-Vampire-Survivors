using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyDate : ScriptableObject
{
	public string _enemyName;
	public Sprite _model;
	public UnityEditor.Animations.AnimatorController _animator;
	public int _maxHp;
	public int _gold;
}
