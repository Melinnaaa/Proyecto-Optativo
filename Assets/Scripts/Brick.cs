using UnityEngine;
using UnityEngine.SceneManagement; // Esta es la línea correcta

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour
{
    public int points = 100;
    public TextMesh textMesh; // TextMesh asignado desde el Inspector
    public bool esCorrecto; // Indica si este bloque tiene la respuesta correcta
    private SpriteRenderer spriteRenderer;
    private int health;
    private BoxCollider2D boxCollider;
    private string answer; // Respuesta almacenada en este bloque

    public bool isMovable = true; // Indica si el brick debería moverse
    public float velocidadMovimiento; // Velocidad de movimiento en el eje x
    private int direccionMovimiento = 1; // Direccion inicial
    
    // Rango de velocidad 
    public float velocidadMinima = 3f;
    public float velocidadMaxima = 9f;

    // Variables para el apilamiento
    public static Vector3 stackPosition = new Vector3(-19, -8, 0); // Posición inicial en la esquina inferior izquierda
    public static float stackOffsetY = 1f; // Desplazamiento en el eje Y para apilar hacia arriba
    private Vector3 posicionOriginal; // Variable para almacenar la posición original del bloque

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>(); // Obtener el BoxCollider2D
        posicionOriginal = transform.position; 
    }

    private void Start()
    {
        // Asigna una velocidad aleatoria dentro del rango especificado
        velocidadMovimiento = Random.Range(velocidadMinima, velocidadMaxima);
    }

    public void Update()
    {
        // Solo mueve el brick si isMovable es verdadero
        if (isMovable)
        {
            transform.Translate(Vector2.right * velocidadMovimiento * direccionMovimiento * Time.deltaTime);
        }
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
        gameObject.SetActive(true);
        velocidadMovimiento = Random.Range(velocidadMinima, velocidadMaxima);

        // Restaurar la posición original
        transform.position = posicionOriginal;  
        boxCollider.enabled = true; // Asegurarse de que el collider esté habilitado al resetear
        spriteRenderer.color = Color.white; // Reiniciar el color al blanco
    }

    public void Hit()
    {}
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta si colisiona con una pared
        if (collision.gameObject.CompareTag("LeftWall"))
        {
            direccionMovimiento = 1; // Cambia de dirección hacia la derecha
            velocidadMovimiento = Random.Range(velocidadMinima, velocidadMaxima);
        }
        else if (collision.gameObject.CompareTag("RightWall"))
        {
            direccionMovimiento = -1; // Cambia de dirección hacia la izquierda
            velocidadMovimiento = Random.Range(velocidadMinima, velocidadMaxima);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                HighlightBlock(); // Cambia el color en función de si es correcto
            }
            Destroy(collision.gameObject); // Destruir la bala después de la colisión
        }
    }

    public void HighlightBlock()
    {
        if (GameManager.Instance.OnHitBlock(answer) == true)
        {
            if (SceneManager.GetActiveScene().name == "Level2")
            {
                MoveToStack();
            }
            spriteRenderer.color = Color.green; // Resaltar en verde si la respuesta es correcta
        }
        else
        {
            spriteRenderer.color = Color.red; // Resaltar en rojo si es incorrecta

            GameManager.Instance.RegistrarError();
        }
    }


    private void MoveToStack()
    {
        transform.position = stackPosition;
        boxCollider.enabled = false; // Deshabilitar el collider para evitar nuevas colisiones
        stackPosition.y += stackOffsetY;
        velocidadMovimiento = 0; // Detener el movimiento en el stack
    }
}
