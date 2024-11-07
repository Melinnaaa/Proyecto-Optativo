using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    public ParticleSystem motorParticulas;

    public float speed = 10f;

    // Variables para disparar
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.7f;
    private float nextFireTime = 0f;

    // Límites para restringir el movimiento
    public float leftBoundary = -20f; // Límite izquierdo por defecto
    public float rightBoundary = 16f; // Límite derecho por defecto
    public AudioSource audioSource; // Referencia al AudioSource

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Detectar si estamos en el nivel 2 para ajustar los límites
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            leftBoundary = -9f;
            rightBoundary = 17.5f;
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            leftBoundary = -9f;
            rightBoundary = 17f;
        }
    }

    private void Start()
    {
        ResetPaddle();
        motorParticulas.Stop();
    }

    public void ResetPaddle()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);
    }

    private void Update()
    {
        // Movimiento de la plataforma
        float moveInput = Input.GetAxis("Horizontal");
        direction = new Vector2(moveInput, 0);

        // Control de las partículas
        if (moveInput != 0)
        {
            if (!motorParticulas.isEmitting)
            {
                motorParticulas.Play();
            }
        }
        else
        {
            if (motorParticulas.isEmitting)
            {
                motorParticulas.Stop();
            }
        }

        // Disparar con la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            if (bulletPrefab != null && bulletSpawnPoint != null)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.LogError("bulletPrefab o bulletSpawnPoint no están asignados.");
            }
        }
    }

    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            rb.velocity = direction * speed;
        }

        // Limitar la posición dentro de los límites
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, leftBoundary, rightBoundary);
        transform.position = clampedPosition;

        // Si alcanzamos un límite, detenemos el movimiento en esa dirección
        if (clampedPosition.x <= leftBoundary && direction.x < 0)
        {
            rb.velocity = Vector2.zero;
        }
        else if (clampedPosition.x >= rightBoundary && direction.x > 0)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource no está asignado.");
        }
    }

}
