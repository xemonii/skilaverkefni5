using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;

    // awake er kallað þegar verið er að hlaða skriftutilvikinu
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // hleypir skotinu af stað með ákveðinni stefnu og krafti
    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    // Update is called once per frame
    void Update()
    {
        // eyðileggur skotið ef það færist of langt frá upprunanum
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // ef það rekst á óvin eyðir hann óvininum
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            Destroy(e.gameObject);
        }

        // eyðileggur skotið óháð árekstri
        Destroy(gameObject);
    }
}
