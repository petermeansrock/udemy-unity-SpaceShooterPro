using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 15.0f;
    [SerializeField]
    private GameObject explosionPrefab;

    private SpawnManager spawnManager;

    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            spawnManager.StartSpawning();
            Destroy(gameObject, 0.15f);
        }
    }
}
