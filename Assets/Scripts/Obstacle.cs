using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int damage = 10; // El daño que el cometa inflige al jugador al atravesarlo.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Ship ship = other.GetComponent<Ship>();
            if (ship != null)
            {
                ship.TakeDamage(damage);
            }
        }
    }
}
