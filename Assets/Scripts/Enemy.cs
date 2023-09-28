using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private int damage = 10;  // Daño que inflige el enemigo al jugador
    [SerializeField] private int pointsValue = 0;  // Puntos que otorga al ser destruido

    private float topBound;  // Límite superior de movimiento
    private float bottomBound;  // Límite inferior de movimiento

    [Header("Enemy Movement")]
    private SpriteRenderer spriteRenderer;  // Renderer del sprite para obtener dimensiones
    private Camera mainCamera;  // Cámara principal para establecer límites

    [System.Serializable]
    public struct DropItem
    {
        public GameObject itemPrefab;
        public float dropProbability;
    }
    [Header("Drop Items")]
    public DropItem[] dropItems;  // Lista de ítems que el enemigo puede soltar

    private void Awake()
    {
        // Inicializaciones
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        SetBounds();
    }

    private void SetBounds()
    {
        // Establece los límites de movimiento basados en la cámara y el tamaño del sprite
        if (mainCamera == null) return;

        float camHalfHeight = mainCamera.orthographicSize;
        float spriteHalfHeight = spriteRenderer.bounds.extents.y;

        topBound = mainCamera.transform.position.y + camHalfHeight - spriteHalfHeight;
        bottomBound = mainCamera.transform.position.y - camHalfHeight + spriteHalfHeight;
    }

    private void LateUpdate()
    {
        ClampPosition();
    }

    private void ClampPosition()
    {
        // Mantiene al enemigo dentro de los límites establecidos
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, bottomBound, topBound);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica la colisión con el jugador y aplica daño
        if (collision.gameObject.CompareTag("Player"))
        {
            Ship ship = collision.gameObject.GetComponent<Ship>();
            if (ship != null)
            {
                ship.TakeDamage(damage);
            }
        }
    }

    public void TryDropItem()
    {
        // Intenta soltar un ítem basado en la probabilidad establecida
        foreach (DropItem dropItem in dropItems)
        {
            if (Random.value <= dropItem.dropProbability)
            {
                Instantiate(dropItem.itemPrefab, transform.position, Quaternion.identity);
                Debug.Log($"El enemigo {gameObject.name} ha soltado un ítem: {dropItem.itemPrefab.name} en la posición: {transform.position}");
                break;
            }
        }
    }

    // Método para obtener el valor en puntos del enemigo
    public int GetPointsValue()
    {
        return pointsValue;
    }

    // Método para obtener el daño del enemigo
    public int GetDamage()
    {
        return damage;
    }
}