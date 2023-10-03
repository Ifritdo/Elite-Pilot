using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int health = 100;
    public HealthBar healthBar;
    Gun[] guns;

    public float invulnerabilityDuration = 5f;
    private bool isInvulnerable = false;
    private float timeSinceDamage;
    float moveSpeed = 5;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool speedUp;
    bool shoot;

    void Start()
    {
        healthBar.SetHealth(health);
        guns = transform.GetComponentsInChildren<Gun>();
    }

    void Update()
    {
        if (isInvulnerable)
        {
            timeSinceDamage += Time.deltaTime;
            if (timeSinceDamage >= invulnerabilityDuration)
            {
                isInvulnerable = false;
                Debug.Log("La nave ya no es invulnerable");
            }
        }

        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        shoot = Input.GetKeyDown(KeyCode.Mouse0);

        if (shoot)
        {
            foreach (Gun gun in guns)
            {
                gun.Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        float moveAmount = moveSpeed * Time.fixedDeltaTime;

        if (speedUp)
        {
            moveAmount *= 3;
        }

        Vector2 move = Vector2.zero;
        if (moveUp) move.y += moveAmount;
        if (moveDown) move.y -= moveAmount;
        if (moveLeft) move.x -= moveAmount;
        if (moveRight) move.x += moveAmount;

        float moveMagnitude = move.magnitude;
        if (moveMagnitude > moveAmount)
        {
            move = move.normalized * moveAmount;
        }

        pos += move;

        float shipWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        float shipHeight = GetComponent<SpriteRenderer>().bounds.size.y;

        float minX = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x + shipWidth / 2;
        float rightLimitAdjustment = 5.0f;
        float maxX = Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x - shipWidth / 2 - rightLimitAdjustment;

        float minY = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y + shipHeight / 2;
        float maxY = Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y - shipHeight / 2;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"La nave ha colisionado con: {collision.gameObject.name} con el tag {collision.gameObject.tag}");

        // Ignoramos las balas del jugador
        if (collision.CompareTag("PlayerBullet"))
        {
            return;
        }

        if (collision.CompareTag("HealItem"))
        {
            Heal(10);
            Destroy(collision.gameObject);
        }
        else if (!isInvulnerable)
        {
            if (collision.CompareTag("EnemyBullet"))
            {
                Bullet bullet = collision.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    TakeDamage(bullet.damage);
                    return;
                }
            }
            else if (collision.CompareTag("EnemyShip"))
            {
                Enemy enemyShip = collision.gameObject.GetComponent<Enemy>();
                if (enemyShip != null)
                {
                    TakeDamage(enemyShip.GetDamage());
                    return;
                }
            }

            Debug.LogWarning($"Daño no reconocido al chocar con: {collision.gameObject.name} con el tag {collision.gameObject.tag}");
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Daño recibido: " + damage);
        Debug.Log($"Intento de infligir daño. Estado de invulnerabilidad: {isInvulnerable}");

        if (isInvulnerable)
            return;

        health -= Mathf.RoundToInt(damage);
        healthBar.SetHealth(health);

        if (health <= 0)
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("EnemyShip"))
            {
                enemy.SetActive(false);
            }
            foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
            {
                bullet.SetActive(false);
            }

            GameOverManager.instance.PlayerLost();

            Destroy(gameObject);
        }

        isInvulnerable = true;
        timeSinceDamage = 0;
    }

    public void Heal(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, 100);
        healthBar.SetHealth(health);
    }
}
