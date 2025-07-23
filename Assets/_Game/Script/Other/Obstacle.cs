using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : GameUnit
{
    [SerializeField] private float moveSpeed = 3f;

    private void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if (transform.position.y <= -9f)
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        SimplePool.Despawn(this);
    }
}
