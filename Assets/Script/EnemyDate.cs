using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyDate : ScriptableObject
{
	public string _enemyName;
	public Sprite _model;
	public int _maxHP;
}
