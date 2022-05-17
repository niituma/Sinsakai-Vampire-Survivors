using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybase : MonoBehaviour
{
    [SerializeField] float _speed = 4f;
    GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(_player.transform.position.x, _player.transform.position.y), _speed * Time.deltaTime);
    }
}
