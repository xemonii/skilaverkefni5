using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // athugar hvort hluturinn sem rekst á sé með PlayerController
        PlayerController controller = other.GetComponent<PlayerController>();

        // ef leikmaður rekst á og heilsa leikmannsins er minni en maxHealth
        if (controller != null && controller.health < controller.maxHealth)
        {
            // aukar heilsu leikmannsins um 1 og eyðileggur game object
            controller.ChangeHealth(1);
            Destroy(gameObject);
        }
    }
}
