using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSight : MonoBehaviour
{
    public int visionRange = 10;
    public LayerMask playerLayer;

    public void DetectPlayer(Vector2 rayDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, visionRange, playerLayer);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            // Player is within the line of sight
            Debug.Log("Player detected!");
            // You can add additional actions here, like attacking the player.
        }
    }
}
