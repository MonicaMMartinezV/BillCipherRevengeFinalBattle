using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;       // Prefab de la bala
    public float spawnInterval = 0.5f;    // Intervalo de tiempo para generar balas
    public float spinSpeed = 45f;         // Velocidad de rotación en modo Spin
    public float bulletSpeed = 10f;       // Velocidad de la bala

    private float spawnTimer = 0f;

    // Define el tipo de spawner
    private enum SpawnerType
    {
        Spin,
        ReverseSpin,
        Regular,
        Ring,
        CurvedCross,
    }
    [SerializeField] private SpawnerType spawnerType; // Tipo de spawner

    void Start()
    {
        spawnTimer = spawnInterval; // Inicializa el temporizador
    }

    void FixedUpdate()
    {
        // Controla el temporizador para disparar balas
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnBullet();
            spawnTimer = 0f; // Reinicia el temporizador
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
    }

    private void SpawnBullet()
    {
        // Crea múltiples balas en diferentes ángulos si es el modo Spin
        if (spawnerType == SpawnerType.Spin)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.Rotate(Vector3.right, i * 45);

                // Agrega fuerza al Rigidbody de la bala para moverla en la dirección de la rotación
                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = bullet.transform.forward * bulletSpeed;
                }
            }
        }
        else if (spawnerType == SpawnerType.ReverseSpin)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.Rotate(Vector3.left, i * 45);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = bullet.transform.forward * bulletSpeed;
                }
            }
        }
        else if (spawnerType == SpawnerType.CurvedCross)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.Rotate(Vector3.right, i * 90);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = bullet.transform.forward * bulletSpeed;
                }
            };
        }
        else if (spawnerType == SpawnerType.Ring)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Vector3 offset = new Vector3(j * 0.5f, 0, 0); 
                    GameObject bullet = Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
                    bullet.transform.Rotate(Vector3.right, i * 36);

                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = bullet.transform.forward * bulletSpeed;
                    }
                }
            }
        }
        else // Modo Regular
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = bullet.transform.forward * bulletSpeed;
            }
        }
    }
}

