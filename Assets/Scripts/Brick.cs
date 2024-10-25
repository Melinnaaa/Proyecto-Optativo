using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public Sprite[] states = new Sprite[0];
    public int points = 100;
    public bool unbreakable;
    public TextMesh textMesh; // TextMesh asignado desde el Inspector
    public bool esCorrecto; // Indica si este bloque tiene la respuesta correcta
    private SpriteRenderer spriteRenderer;
    private int health;
    private BoxCollider2D boxCollider;
    private string answer; // Respuesta almacenada en este bloque

    // Variables para el apilamiento
    public static Vector3 stackPosition = new Vector3(-19, -8, 0); // Posición inicial en la esquina inferior izquierda
    public static float stackOffsetY = 1f; // Desplazamiento en el eje Y para apilar hacia arriba

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>(); // Obtener el BoxCollider2D
    }

    // Método para resetear el color del bloque
    public void ResetColor()
    {
        spriteRenderer.color = Color.white; // Cambia esto al color original del bloque
    }

    public void SetAnswer(string respuesta, bool correcto)
    {
        answer = respuesta;
        esCorrecto = correcto;
    }

    public void ResetBrick()
    {
        gameObject.SetActive(true);  // Asegurarse de que el bloque esté activo

        // Si el bloque no es inquebrantable, restaurar sus propiedades de salud, sprite, etc.
        if (!unbreakable)
        {
            health = states.Length;
            spriteRenderer.sprite = states[health - 1];
            boxCollider.enabled = true;  // Habilitar el colisionador
            spriteRenderer.color = Color.white;  // Reiniciar el color al blanco
        }
    }

    public void Hit()
    {}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                // Verificar si la respuesta del bloque es correcta
                HighlightBlock(); // Cambia el color en función de si es correcto
            }
            Destroy(collision.gameObject); // Destruir la bala después de la colisión
        }
    }


    public void HighlightBlock()
    {
        if (GameManager.Instance.OnHitBlock(answer) == true)
        {
            spriteRenderer.color = Color.green; // Resaltar en verde si la respuesta es correcta
            if(SceneManager.GetActiveScene().name == "Level2")
            {
                MoveToStack();
            }
        }
        else
        {
            spriteRenderer.color = Color.red; // Resaltar en rojo si es incorrecta
            GameManager.Instance.RegistrarError(); // Registrar el error
        }
    }

    private void MoveToStack()
    {
        transform.position = stackPosition;
        boxCollider.enabled = false; // Deshabilitar el collider para evitar nuevas colisiones
        stackPosition.y += stackOffsetY;
    }
}
