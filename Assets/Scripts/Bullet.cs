using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f; // Aumenta la velocidad aquí para probar

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Aplicar una fuerza constante hacia arriba al Rigidbody2D
        rb.velocity = Vector2.up * speed;
        // Aplicar un gran impulso hacia arriba para asegurar que la bala se mueva rápidamente
        rb.AddForce(Vector2.up * speed * 10f, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Destruir la bala al colisionar
        Destroy(gameObject);

        // Si colisiona con un bloque, llama al método Hit()
        if (collision.gameObject.CompareTag("Brick"))
        {
            Brick brick = collision.gameObject.GetComponent<Brick>();
            if (brick != null)
            {
                brick.Hit();
            }
        }
    }
}
