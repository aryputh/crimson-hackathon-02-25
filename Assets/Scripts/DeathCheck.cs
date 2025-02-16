using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>();

            collision.gameObject.transform.position = controller.SpawnPoint;
        }
    }
}
