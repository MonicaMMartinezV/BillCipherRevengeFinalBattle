using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lifePoints = 5; // Puntos de vida del jugador
    public float moveSpeed = 5f; // Velocidad de movimiento del jugador
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform bulletSpawnPoint; // Punto de generación de la bala
    void Start()
    {
        // Calcula los límites de la cámara en el espacio del mundo
    }

    void Update()
    {
        // Obtiene la entrada del teclado
        float moveHorizontal = Input.GetAxis("Horizontal"); // A y D o flechas izquierda y derecha
        float moveVertical = Input.GetAxis("Vertical"); // W y S o flechas arriba y abajo

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);

        // Normaliza el vector de movimiento para evitar que sea más rápido en diagonal
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Verifica si la tecla Shift está presionada
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetMouseButton(1))
        {
            movement /= 3;
        }

        // Aplica el movimiento al jugador
        Vector3 newPosition = transform.position + movement * moveSpeed * Time.deltaTime;

        // Genera una bala si se presiona el botón de disparo
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
        transform.position = newPosition;
    }
}