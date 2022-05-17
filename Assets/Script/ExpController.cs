using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    [SerializeField] int _expvalue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var gamemanager = FindObjectOfType<GameManager>();
            gamemanager.AddExp(_expvalue);
            gameObject.SetActive(false);
        }
    }
}
