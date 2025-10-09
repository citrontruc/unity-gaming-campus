/*
A class to handle collisions between the player and other entities.
Collision events are triggered by the player when he touches another entity.
*/

using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;

    void OnEnable()
    {
        //player.OnCollisionEvent += HandleCollision;
    }

    void OnDisable()
    {
        //player.OnCollisionEvent -= HandleCollision;
    }

    private void HandleCollision(GameObject hitObject)
    {
        Debug.Log("We just hit something");
        if (hitObject.CompareTag("Obstacle"))
        {
            HandleObstacle(hitObject);
        }
        else if (hitObject.CompareTag("Collectible"))
        {
            HandleCollectible(hitObject);
        }
    }

    private void HandleObstacle(GameObject obstacle)
    {
        Debug.Log("Player hit obstacle: " + obstacle.name);
        // Example: notify GameManager, reduce HP, etc.
    }

    private void HandleCollectible(GameObject collectible)
    {
        Debug.Log("Player picked collectible: " + collectible.name);
        // Example: collectible logic can also destroy itself
    }
}
