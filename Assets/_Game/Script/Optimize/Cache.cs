using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider2D, Obstacle> obstacle = new Dictionary<Collider2D, Obstacle>();

    public static Obstacle GetObstacle(Collider2D collider)
    {
        if (!obstacle.ContainsKey(collider))
        {
            obstacle.Add(collider, collider.GetComponent<Obstacle>());
        }

        return obstacle[collider];
    }
}
