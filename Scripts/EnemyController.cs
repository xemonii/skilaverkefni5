using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public float changeTime = 0.5f;

    Rigidbody2D rb;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        // minnkar tejlarann
        timer -= Time.deltaTime;

        // breytir stefnu þegar teljarinn nær núlli
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    // FixedUpdate is called at fixed intervals and is used for physics operations
    void FixedUpdate()
    {
        // færir óvininn lóðrétt
        Vector2 position = rb.position;
        position.y = position.y + Time.deltaTime * speed * direction;
        rb.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Ef rekst á leikmanninn skal minnka heilsu leikmannsins
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
