using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public Sprite[] states = new Sprite[0];
    public int points = 100;
    public bool unbreakable;
    public TextMesh textMesh; // TextMesh asignado desde el Inspector

    private SpriteRenderer spriteRenderer;
    private int health;
    private BoxCollider2D boxCollider;

    // Variables para el apilamiento
    public static Vector3 stackPosition = new Vector3(-19, -8, 0); // Posición inicial en la esquina inferior izquierda
    public static float stackOffsetY = 1f; // Desplazamiento en el eje Y para apilar hacia arriba

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>(); // Obtener el BoxCollider2D
    }

    private void Start()
    {
        ResetBrick();
    }

    public void ResetBrick()
    {
        gameObject.SetActive(true);
        stackPosition = new Vector3(-19, -8, 0);

        if (!unbreakable)
        {
            health = states.Length;
            spriteRenderer.sprite = states[health - 1];
            boxCollider.enabled = true; // Asegurarse de que el collider esté habilitado al resetear
        }
    }

    public void Hit()
    {
        // Si es un bloque irrompible no se hace nada
        if (unbreakable) {
            return;
        }

        health--;

        if (health <= 0) {
            MoveToStack(); // Mover el bloque a la esquina antes de desactivarlo
        } else {
            spriteRenderer.sprite = states[health - 1];
        }

        GameManager.Instance.OnBrickHit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball") {
            Hit();
        }
    }

    private void MoveToStack()
    {
        // Mover el bloque a la posición de la pila
        transform.position = stackPosition;

        // Deshabilitar el collider para que no sea hiteado de nuevo
        boxCollider.enabled = false;

        // Actualizar la posición para el próximo bloque (hacia arriba)
        stackPosition.y += stackOffsetY;
    }

}
