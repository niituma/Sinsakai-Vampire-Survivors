using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActive : MonoBehaviour
{
    public int _mindamage { get; set; } = 1;
    public int _maxdamage { get; set; } = 4;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var damge = UnityEngine.Random.Range(_mindamage, _maxdamage);
            collision.gameObject.GetComponent<EnemyHPController>().Damege(damge);
        }
    }
}
