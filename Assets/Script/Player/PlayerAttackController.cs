using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] GameObject _weap;
    float _timer = 0;
    AddOrignalMethod Method = new AddOrignalMethod();
    PlayerController _moveController;
    private void Start()
    {
        _moveController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 3f)
        {
            WeapActive();
            _timer -= 3f;
        }

    }
    void WeapActive()
    {
        var rote = _weap.transform.rotation;
        _weap.transform.position = transform.position;
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
        _weap.transform.rotation = rote;
        _weap.SetActive(true);
        _weap.GetComponentInChildren<BoxCollider2D>().enabled = true;
        StartCoroutine(Method.DelayMethod(0.1f, () => _weap.GetComponentInChildren<BoxCollider2D>().enabled = false));
        StartCoroutine(Method.DelayMethod(0.15f, () => _weap.SetActive(false)));
    }

}
