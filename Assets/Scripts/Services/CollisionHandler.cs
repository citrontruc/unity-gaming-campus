/*
A class to handle collisions between the player and other entities.
Collision events are triggered by the player when he touches another entity.
*/

using UnityEngine;

public class CollisionHandler : Singleton<CollisionHandler>
{
    [SerializeField]
    private VoidEventChannelSO<int> collectableEventChannelSO;

    [SerializeField]
    private VoidEventChannelSO<Obstacle.Resistance> ObstacleEventChannelSO;
    private PlayerScore _playerScore => PlayerScore.Instance;

    #region Subscribe to events
    void OnEnable()
    {
        collectableEventChannelSO.onEventRaised += HandleCollectible;
        ObstacleEventChannelSO.onEventRaised += HandleObstacle;
    }

    void OnDisable()
    {
        collectableEventChannelSO.onEventRaised -= HandleCollectible;
        ObstacleEventChannelSO.onEventRaised -= HandleObstacle;
    }
    #endregion

    #region Handle collisions with items
    private void HandleObstacle(Obstacle.Resistance _resistance)
    {
        Debug.Log($"Player hit obstacle: {_resistance}.");
        // Example: notify GameManager, reduce HP, etc.
    }

    private void HandleCollectible(int score)
    {
        _playerScore.IncrementScore(score);
        //Debug.Log($"Collectible collected for {score}");
    }
    #endregion
}
