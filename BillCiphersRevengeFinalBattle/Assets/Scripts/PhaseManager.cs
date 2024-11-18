using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    public GameObject billCipher;       // El enemigo principal inicial
    public GameObject[] midEnemies;    // Los dos enemigos que aparecerán después
    public GameObject boss;            // El jefe final

    private void Start()
    {
        StartCoroutine(ManagePhases());
    }

    IEnumerator ManagePhases()
    {
        // Fase 1: Mostrar Bill Cipher
        ActivateEnemy(billCipher);
        yield return new WaitForSeconds(20f); // Espera 20 segundos

        // Fase 2: Desactivar Bill Cipher, activar los dos enemigos
        DeactivateEnemy(billCipher);
        foreach (GameObject enemy in midEnemies)
        {
            ActivateEnemy(enemy);
        }
        yield return new WaitForSeconds(20f); // Espera 10 segundos

        // Fase 3: Desactivar los dos enemigos, activar el jefe final
        foreach (GameObject enemy in midEnemies)
        {
            DeactivateEnemy(enemy);
        }
        ActivateEnemy(boss);
        yield return new WaitForSeconds(75f);

        DeactivateEnemy(boss);

        // Aquí termina la lógica de las fases (opcionalmente puedes añadir más)
    }

    private void ActivateEnemy(GameObject enemy)
    {
        if (enemy != null)
        {
            enemy.SetActive(true); // Activa el enemigo
        }
    }

    private void DeactivateEnemy(GameObject enemy)
    {
        if (enemy != null)
        {
            enemy.SetActive(false); // Desactiva el enemigo
        }
    }
}