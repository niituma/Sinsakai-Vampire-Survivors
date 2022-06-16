using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoop : MonoBehaviour
{
    [SerializeField] bool _verticalWall = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "MainCamera")
        {
            if (_verticalWall)
            {
                var pos = collision.transform.position;
                pos.x *= -1;
                var offset = pos.x < 0 ? 1 : -1;
                pos.x += offset;
                collision.transform.position = pos;
            }
            else
            {
                var pos = collision.transform.position;
                pos.y *= -1;
                var offset = pos.y < 0 ? 1 : -1;
                pos.y += offset;
                collision.transform.position = pos;
            }
        }
    }
}
