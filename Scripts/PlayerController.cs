using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 3f;
    public float jumpSpeed = 500f;
    public int maxHealth = 3;
    public float timeInvincible = 2f;
    public int health { get { return currentHealth; } }

    int currentHealth;
    bool isInvincible;
    float invincibleTimer;
    bool isGrounded = true;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    TextMeshProUGUI healthText;
    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;
        GameObject stig = GameObject.Find("Stig");
        if (stig != null)
        {
            healthText = stig.GetComponent<TextMeshProUGUI>();
        }

        UpdateHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        // höndla hreyfingu leikmanns
        if (isGrounded)
        {
            rb.gravityScale = 2.1f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0)
        {
            Move(horizontal);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        // höndla leikmann að hoppa
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }

        // gerir leikmanninum ósigrandi í stuttan tíma áður en hann meiðist
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        // tökum á leikmanni að skjóta
        if (Input.GetKeyDown(KeyCode.S))
        {
            Launch();
        }
    }

    // færir leikmann ​​lárétt
    void Move(float horizontal)
    {
        Vector3 movement = new Vector3(horizontal * movementSpeed * Time.deltaTime, 0f, 0f);
        transform.position += movement;

        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }

        animator.SetBool("isRunning", true);
    }

    // lætur leikmann ​​hoppa
    void Jump()
    {
        rb.AddForce(Vector2.up * jumpSpeed);
        isGrounded = false;
    }

    // athugar hvenær leikmaður lendir á jörðina
    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    // breytir heilsu leikmanns
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UpdateHealthText();

        // endurræsir leikinn ef heilsa leikmanns er á þrotum
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }

    // uppfærir heilsutexta á UI
    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth;
        }
    }

    // hleypir af stað skotfæri
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();

        Vector2 launchDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;
        projectile.Launch(launchDirection, 300);
    }
}
