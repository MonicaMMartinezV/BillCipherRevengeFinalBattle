using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillCiphers : MonoBehaviour
{
    private const string MY_BULLET_TAG    = "Bullet";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider objectCollider)
    {
        // Detectar si colisiona con una bala
        if (objectCollider.CompareTag(MY_BULLET_TAG))
        {
            //Debug.Log("Colisión con una bala: " + objectCollider.gameObject.name);
            // Destruir la bala después de la colisión
            Destroy(objectCollider.gameObject);
        }
    }
}
