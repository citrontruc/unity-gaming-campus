/*
A class to handle collisions between the player and other entities.
Collision events are triggered by the player when he touches another entity.
*/

using UnityEngine;
using UnityEngine.Events;

public class CollisionHandler : Singleton<CollisionHandler>
{
    [SerializeField]
    private VoidEventChannelSO<int> collectableEventChannelSO;
    private PlayerScore _playerScore => PlayerScore.Instance;

    void OnEnable()
    {
        collectableEventChannelSO.onEventRaised += HandleCollectible;
    }

    void OnDisable()
    {
        collectableEventChannelSO.onEventRaised -= HandleCollectible;
    }

    /*
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
        */

    private void HandleObstacle(GameObject obstacle)
    {
        Debug.Log("Player hit obstacle: " + obstacle.name);
        // Example: notify GameManager, reduce HP, etc.
    }

    private void HandleCollectible(int score)
    {
        _playerScore.IncrementScore(score);
        Debug.Log($"Collectible collected for {score}");
        // Example: collectible logic can also destroy itself
    }
}
