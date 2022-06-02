using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActive : Skillbase
{
    public override void ActiveSkill()
    {
        PlayerController _moveController = FindObjectOfType<PlayerController>();

        var rote = transform.parent.rotation;
        transform.parent.position = _moveController.gameObject.transform.position;
        if (_moveController.Lastdir.y == 1)
        {
            rote = Quaternion.Euler(0.0f, 0.0f, 90f);
        }
        else if (_moveController.Lastdir.y == -1)
        {
            rote = Quaternion.Euler(0.0f, 0.0f, 270f);
        }
        else if (_moveController.Lastdir.x == 1)
        {
            rote = Quaternion.Euler(0.0f, 0.0f, 0f);
        }
        else if (_moveController.Lastdir.x == -1)
        {
            rote = Quaternion.Euler(0.0f, 0.0f, 180f);
        }
        transform.parent.rotation = rote;
        transform.parent.gameObject.SetActive(true);
        GetComponentInParent<BoxCollider2D>().enabled = true;
        StartCoroutine(Method.DelayMethod(0.1f, () => GetComponentInParent<BoxCollider2D>().enabled = false));
        StartCoroutine(Method.DelayMethod(0.15f, () => transform.parent.gameObject.SetActive(false)));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var damge = UnityEngine.Random.Range(_mindamage, _maxdamage);
            collision.gameObject.GetComponent<EnemyHPController>().Damege(damge);
        }
    }
}
