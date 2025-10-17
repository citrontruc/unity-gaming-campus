/*
A class to handle collisions between the player and other entities.
Collision events are triggered by the player when he touches another entity.
*/

using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : Singleton<CollisionHandler>
{
    [SerializeField]
    private CollectableEventChannelSO collectableEventChannelSO;

    void OnEnable()
    {
        collectableEventChannelSO.onEventRaised += Printlog;
    }

    void OnDisable()
    {
        collectableEventChannelSO.onEventRaised -= Printlog;
    }

    private void Printlog(int score)
    {
        Debug.Log($"We got hit! {score}");
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
