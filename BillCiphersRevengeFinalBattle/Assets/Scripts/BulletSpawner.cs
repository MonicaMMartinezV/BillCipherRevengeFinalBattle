using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de que esta línea esté incluida


public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;       // Prefab de la bala
    public float spawnInterval = 0.5f;    // Intervalo de tiempo para generar balas
    public float spinSpeed = 45f;         // Velocidad de rotación en modo Spin
    public float bulletSpeed = 10f;       // Velocidad de la bala

    private float spawnTimer = 0f;

    private GameIUManager gameIUManager; // Referencia al GameIUManager


    

    // Define el tipo de spawner
    private enum SpawnerType
    {
        Spin,
        ReverseSpin,
        Regular,
        Ring,
        CurvedCross,
        Ellipse,
        Flower
    }
    [SerializeField] private SpawnerType spawnerType; // Tipo de spawner

    void Start()
    {
        spawnTimer = spawnInterval; // Inicializa el temporizador
        gameIUManager = GameObject.FindObjectOfType<GameIUManager>(); // Encuentra el UI Manager
        gameIUManager.Start();
    }

    void FixedUpdate()
    {
        // Controla el temporizador para disparar balas
        
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnBullet();
            spawnTimer = 0f; // Reinicia el temporizador
            gameIUManager.UpdateBulletCount();
        }

        // Si es el modo Spin, rota el spawner continuamente
        if (spawnerType == SpawnerType.Spin)
        {
            transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
        }
        else if (spawnerType == SpawnerType.ReverseSpin)
        {
            transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
        }
        else if (spawnerType == SpawnerType.CurvedCross)
        {
            transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
        }
        else if (spawnerType == SpawnerType.Ring)
        {
            transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
        }
        else if (spawnerType == SpawnerType.Ellipse)
        {
            transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
        }
        else if (spawnerType == SpawnerType.Flower)
        {
            transform.Rotate(Vector3.left, spinSpeed * Time.deltaTime);
        }
        
    }

    private void SpawnBullet()
    {
        // Crea múltiples balas en diferentes ángulos si es el modo Spin
        if (spawnerType == SpawnerType.Spin)
        {
            SpinBullet();
        }
        else if (spawnerType == SpawnerType.ReverseSpin)
        {
            ReverseSpinBullet();
        }
        else if (spawnerType == SpawnerType.CurvedCross)
        {
            CurvedCrossBullet();
        }
        else if (spawnerType == SpawnerType.Ring)
        {
            RingBullet();
        }
        else if (spawnerType == SpawnerType.Ellipse) {
            EllipseBullet();
        }
        else if (spawnerType == SpawnerType.Flower) {
            FlowerBullet();
        }
        else // Modo Regular
        {
            RegularBullet();
        }
    }

    void RegularBullet() {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bullet.transform.forward * bulletSpeed;
        }
    }

    void SpinBullet() {
        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.Rotate(Vector3.right, i * 45);
            Bullet bulletScript = bullet.GetComponent<Bullet>();

            if (bulletScript != null)
            {
                bulletScript.objectiveTag = "Player"; // Las balas disparadas apuntan al jugador
            }

            // Agrega fuerza al Rigidbody de la bala para moverla en la dirección de la rotación
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.forward * bulletSpeed;
            }
        }
    }

    void ReverseSpinBullet()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.Rotate(Vector3.left, i * 45);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.objectiveTag = "Player"; // Las balas disparadas apuntan al jugador
            }
            
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.forward * bulletSpeed;
            }
        }
    }

    void CurvedCrossBullet()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.Rotate(Vector3.right, i * 90);

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.objectiveTag = "Player"; // Las balas disparadas apuntan al jugador
            }
            
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.forward * bulletSpeed;
            }
        }
    }

    void RingBullet() {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Vector3 offset = new Vector3(j * 0.5f, 0, 0); 
                GameObject bullet = Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
                bullet.transform.Rotate(Vector3.right, i * 36);

                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.objectiveTag = "Player"; // Las balas disparadas apuntan al jugador
                }
                
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = bullet.transform.forward * bulletSpeed;
                }
            }
        }
    }
    
    void EllipseBullet() {
        float t = Time.time * Mathf.PI * 2; // El tiempo afecta el movimiento en la elipse

        // Definir los semiejes de la elipse
        float a = 2f; // Semieje mayor
        float b = 1f; // Semieje menor

        // Calcular la posición de la bala con la fórmula de la elipse
        Vector3 offset = new Vector3(a * Mathf.Cos(t), b * Mathf.Sin(t), 0); // Movimiento en X y Y, Z fijo

        // Crear la bala en la posición calculada
        GameObject bullet = Instantiate(bulletPrefab, transform.position + offset, transform.rotation);

        // Configurar el objetivo de la bala
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.objectiveTag = "Player"; // Las balas disparadas apuntan al jugador
        }

        // Agregar velocidad a la bala
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = bullet.transform.forward * bulletSpeed;
        }
    }

    void FlowerBullet() {
        int numOfRays = 4;
        float angleStep = 360f / numOfRays; // Espaciado entre rayos

        for (int i = 0; i < numOfRays; i++) // Número de balas
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            
            // Calcula el ángulo actual
            float currentAngle = i * angleStep;
            
            // Rota la bala para distribuirla uniformemente en 360 grados
            bullet.transform.Rotate(Vector3.right, currentAngle);
            
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.objectiveTag = "Player"; // Las balas disparadas apuntan al jugador
            }
            // Agrega fuerza al Rigidbody de la bala para moverla en la dirección de la rotación
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.forward * bulletSpeed;
            }
        }
    }
}