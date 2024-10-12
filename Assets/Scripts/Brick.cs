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

    private void Start()
    {
        ResetBrick();
    }

    public void SetAnswer(string answer, bool esCorrecto)
    {
        this.answer = answer;
        this.esCorrecto = esCorrecto; // Asigna si este bloque contiene la respuesta correcta
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
            spriteRenderer.color = Color.white; // Reiniciar el color al blanco
        }
    }

    public void Hit()
    {
        if (unbreakable) return;

        health--;
        if (health <= 0)
        {
            // Solo llamar a MoveToStack si la escena actual es "Level2"
            if (SceneManager.GetActiveScene().name == "Level2")
            {
                MoveToStack();
            }
        }
        else
        {
            spriteRenderer.sprite = states[health - 1];
        }

        GameManager.Instance.OnBrickHit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {      
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                // Verificar si la respuesta es correcta
                bool isCorrect = bullet.IsCorrectAnswer(answer); // Comparar la respuesta del bloque con la respuesta de la bala
                HighlightBlock(isCorrect); // Aquí cambia el color basado en la respuesta
            }

            Destroy(collision.gameObject); // Destruir la bala después de la colisión
        }
    }


   public void HighlightBlock(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("Color verde para respuesta correcta"); // Depuración para ver si está entrando en la condición correcta
            spriteRenderer.color = Color.green; // Resaltar en verde si la respuesta es correcta
        }
        else
        {
            Debug.Log("Color rojo para respuesta incorrecta"); // Depuración para ver si está entrando en la condición incorrecta
            spriteRenderer.color = Color.red; // Resaltar en rojo si es incorrecta
        }
    }

    private void MoveToStack()
    {
        transform.position = stackPosition;
        boxCollider.enabled = false; // Deshabilitar el collider para evitar nuevas colisiones
        stackPosition.y += stackOffsetY;
    }
}
