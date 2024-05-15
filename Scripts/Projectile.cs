using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;

    // awake er kalla� �egar veri� er a� hla�a skriftutilvikinu
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // hleypir skotinu af sta� me� �kve�inni stefnu og krafti
    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    // Update is called once per frame
    void Update()
    {
        // ey�ileggur skoti� ef �a� f�rist of langt fr� upprunanum
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // ef �a� rekst � �vin ey�ir hann �vininum
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            Destroy(e.gameObject);
        }

        // ey�ileggur skoti� �h�� �rekstri
        Destroy(gameObject);
    }
}
