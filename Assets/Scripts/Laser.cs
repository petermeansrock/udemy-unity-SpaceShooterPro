using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float speed = 8.0f;

    private float maxY = 8.0f;

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);
        ConditionallyDespawn();
    }

    void ConditionallyDespawn()
    {
        if (transform.position.y > maxY)
        {
            Transform parent = transform.parent;
            if (parent != null)
            {
                Destroy(parent.gameObject);
            } else
            {
                Destroy(gameObject);
            }
        }
    }
}
