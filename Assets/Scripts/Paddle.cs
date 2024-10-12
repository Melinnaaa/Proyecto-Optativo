using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 direction;
    public ParticleSystem motorParticulas; // Asigna el Particle System en el Inspector

    public float speed = 30f;
    public float maxBounceAngle = 75f;

    // Variables para disparar
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform bulletSpawnPoint; // Punto desde donde se disparan las balas
    public float fireRate = 0.5f; // Tiempo entre disparos
    private float nextFireTime = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetPaddle();
        motorParticulas.Stop(); // Las partículas deben estar desactivadas al inicio
    }

    public void ResetPaddle()
    {
        rb.velocity = Vector2.zero;
        transform.position = new Vector2(0f, transform.position.y);
    }

    private void Update()
    {
        // Movimiento de la plataforma
        float moveInput = Input.GetAxis("Horizontal"); // Usa Input.GetAxis para detección de movimiento continuo
        direction = new Vector2(moveInput, 0);

        // Control de las partículas
        if (moveInput != 0)
        {
            if (!motorParticulas.isEmitting)
            {
                motorParticulas.Play(); // Iniciar las partículas cuando se mueva
            }
        }
        else
        {
            if (motorParticulas.isEmitting)
            {
                motorParticulas.Stop(); // Detener las partículas cuando no se mueva
            }
        }

        // Disparar con la barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            if (bulletPrefab != null && bulletSpawnPoint != null)
            {
                Shoot(); // Dispara una bala
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                Debug.LogError("bulletPrefab o bulletSpawnPoint no están asignados. Asegúrate de asignar ambos en el Inspector.");
            }
        }
    }


    private void FixedUpdate()
    {
        if (direction != Vector2.zero)
        {
            rb.velocity = direction * speed; // Ajustar la velocidad de la nave
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
        {
            return;
        }

        Rigidbody2D ball = collision.rigidbody;
        Collider2D paddle = collision.otherCollider;

        // Gather information about the collision
        Vector2 ballDirection = ball.velocity.normalized;
        Vector2 contactDistance = paddle.bounds.center - ball.transform.position;

        // Rotate the direction of the ball based on the contact distance
        float bounceAngle = (contactDistance.x / paddle.bounds.size.x) * maxBounceAngle;
        ballDirection = Quaternion.AngleAxis(bounceAngle, Vector3.forward) * ballDirection;

        // Re-apply the new direction to the ball
        ball.velocity = ballDirection * ball.velocity.magnitude;
    }

    private void Shoot()
    {
        // Instanciar la bala en el punto de disparo
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
    }
}