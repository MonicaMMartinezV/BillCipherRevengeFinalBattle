using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBullet : MonoBehaviour
{
    public float bulletLife = 1f;
    public float speed = 1f;
    public string objectiveTag = "Player";
    

    private float timer = 0f;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(objectiveTag))
        {
            Destroy(this.gameObject);
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
