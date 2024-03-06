using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Vector3[] playerSpawnPoints = new Vector3[2];
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] float worldEdgeFront, worldEdgeBack;

    private void Start()
    {
        Character player1Character = Instantiate(GameManager.Player1Character, playerSpawnPoints[0], Quaternion.identity);
        InputManager.Instance.SetPlayer1(player1Character);
        cameraManager.Characters.Add(player1Character);
        player1Character.SetWorldEdges(worldEdgeFront, worldEdgeBack);
        if (GameManager.Player2Active)
        {
            Character player2Character = Instantiate(GameManager.Player2Character, playerSpawnPoints[1], Quaternion.identity);
            InputManager.Instance.SetPlayer2(player2Character);
            cameraManager.Characters.Add(player2Character);
            player2Character.SetWorldEdges(worldEdgeFront, worldEdgeBack);
        }
    }
}
