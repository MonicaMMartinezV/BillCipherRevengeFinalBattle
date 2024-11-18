using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public BulletSpawner[] bulletSpawners; // Array de spawners de balas
    private int currentPattern = 0;

    void Start()
    {
        StartCoroutine(ChangePatterns());
    }

    IEnumerator ChangePatterns()
    {
        while (true)
        {
            // Cambia el patrón actual
            currentPattern = (currentPattern + 1) % bulletSpawners.Length;

            // Activa el spawner correspondiente
            for (int i = 0; i < bulletSpawners.Length; i++)
            {
                bulletSpawners[i].enabled = (i == currentPattern);
            }

            yield return new WaitForSeconds(15f); // Cambia de patrón cada 10 segundos
        }
    }
}