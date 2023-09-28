using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    bool canBeDestroyed = false;

    private Enemy enemyScript;  // Referencia al script Enemy

    void Start()
    {
        enemyScript = GetComponent<Enemy>();  // Obtenemos la referencia al script Enemy al iniciar
    }

    void Update()
    {
        if (transform.position.x < 18.63f)
        {
            canBeDestroyed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDestroyed)
        {
            return;
        }
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                // Antes de destruir el enemigo, verifica si debe soltar un ítem.
                if (enemyScript != null)
                {
                    enemyScript.TryDropItem();

                    // Añade puntos cuando destruyes el enemigo
                    int points = enemyScript.GetPointsValue();
                    ScoreManager.AddPoints(points);
                }

                Destroy(gameObject);
                Destroy(bullet.gameObject);
            }
        }
    }
}