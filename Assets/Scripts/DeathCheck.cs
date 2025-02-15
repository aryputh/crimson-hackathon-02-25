using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    [SerializeField] private float spawnX;
    [SerializeField] private float spawnY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = new Vector2(spawnX, spawnY);
        }
    }
}
