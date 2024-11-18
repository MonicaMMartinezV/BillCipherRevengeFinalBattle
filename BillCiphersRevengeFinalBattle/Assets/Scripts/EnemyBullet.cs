using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float speed = 1f;
    public string objectiveTag = "Player";
    private float timer = 0f;
    private const string MY_BULLET_TAG    = "Bullet";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider objectCollider)
    {
        Debug.Log("Colision con " + objectCollider.tag);
        // Detectar si colisiona con una bala
        if (objectCollider.CompareTag(MY_BULLET_TAG))
        {
            //Debug.Log("Colisión con una bala: " + objectCollider.gameObject.name);
            // Destruir la bala después de la colisión
            Destroy(objectCollider.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bulletLife)
        {
            Destroy(this.gameObject);
        }

        // Mueve la bala en la dirección en que está rotada
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
