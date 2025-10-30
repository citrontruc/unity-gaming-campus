using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject playerModel;

    public void MoveModel(Vector3 position)
    {
        playerModel.transform.position = position;
    }
}