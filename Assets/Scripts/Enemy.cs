using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private int damage = 10;  // Da�o que inflige el enemigo al jugador
    [SerializeField] private int pointsValue = 0;  // Puntos que otorga al ser destruido

    private float topBound;  // L�mite superior de movimiento
    private float bottomBound;  // L�mite inferior de movimiento

    [Header("Enemy Movement")]
    private SpriteRenderer spriteRenderer;  // Renderer del sprite para obtener dimensiones
    private Camera mainCamera;  // C�mara principal para establecer l�mites

    [System.Serializable]
    public struct DropItem
    {
        public GameObject itemPrefab;
        public float dropProbability;
    }
    [Header("Drop Items")]
    public DropItem[] dropItems;  // Lista de �tems que el enemigo puede soltar

    private void Awake()
    {
        // Inicializaciones
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        SetBounds();
    }

    private void SetBounds()
    {
        // Establece los l�mites de movimiento basados en la c�mara y el tama�o del sprite
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
        // Mantiene al enemigo dentro de los l�mites establecidos
        Vector3 position = transform.position;
        position.y = Mathf.Clamp(position.y, bottomBound, topBound);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica la colisi�n con el jugador y aplica da�o
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
        // Intenta soltar un �tem basado en la probabilidad establecida
        foreach (DropItem dropItem in dropItems)
        {
            if (Random.value <= dropItem.dropProbability)
            {
                Instantiate(dropItem.itemPrefab, transform.position, Quaternion.identity);
                Debug.Log($"El enemigo {gameObject.name} ha soltado un �tem: {dropItem.itemPrefab.name} en la posici�n: {transform.position}");
                break;
            }
        }
    }

    // M�todo para obtener el valor en puntos del enemigo
    public int GetPointsValue()
    {
        return pointsValue;
    }

    // M�todo para obtener el da�o del enemigo
    public int GetDamage()
    {
        return damage;
    }
}