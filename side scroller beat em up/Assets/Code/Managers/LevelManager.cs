using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Vector3[] playerSpawnPoints = new Vector3[2];
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] float worldEdgeFront, worldEdgeBack;
    private List<Character> characters = new List<Character>();

    [SerializeField] private EnemyWave[] enemyWaves;
    int currentWave = 0;

    private void Start()
    {
        characters.Add(Instantiate(GameManager.Player1Character, playerSpawnPoints[0], Quaternion.identity));
        InputManager.Instance.SetPlayer1(characters[0]);
        cameraManager.Characters.Add(characters[0]);
        characters[0].SetWorldEdges(worldEdgeFront, worldEdgeBack);
        if (GameManager.Player2Active)
        {
            characters.Add(Instantiate(GameManager.Player2Character, playerSpawnPoints[1], Quaternion.identity));
            InputManager.Instance.SetPlayer2(characters[1]);
            cameraManager.Characters.Add(characters[1]);
            characters[1].SetWorldEdges(worldEdgeFront, worldEdgeBack);
        }
    }

    private void FixedUpdate()
    {
        if (currentWave < enemyWaves.Length)
        {
            foreach  (Character character in characters)
            {
                if (character.transform.position.x >= enemyWaves[currentWave].TriggerDistance)
                {
                    ActivateEnemyWave();
                    break;
                }
            }
        }
    }
    private void ActivateEnemyWave()
    {
        for (int i = 0; i < enemyWaves[currentWave].enemies.Length; i++)
        {
            enemyWaves[currentWave].enemies[i].ActivateSelf();
        }
        if (enemyWaves[currentWave].CamLockPosition > 0) WaitToReleaseCamera(enemyWaves[currentWave].enemies, enemyWaves[currentWave].CamLockPosition);
        currentWave++;
    }

    private IEnumerator WaitToReleaseCamera(EnemyBase[] enemies, float positionToLockAt)
    {
        List<EnemyBase> livingEnemies = new List<EnemyBase>();
        for (int i = 0; i < enemies.Length; i++)
        {
            livingEnemies.Add(enemies[i]);
        }
        cameraManager.LockAtPosition(positionToLockAt);
        while (livingEnemies.Count > 0)
        {
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < livingEnemies.Count; i++)
            {
                if (!livingEnemies[i].IsAlive)
                {
                    livingEnemies.RemoveAt(i);
                    i--;
                }
            }
        }
        cameraManager.ReleaseFromPosition();
    }
}

[System.Serializable]
public class EnemyWave
{
    public float TriggerDistance;
    public EnemyBase[] enemies;
    /// <summary>
    /// Leave at -1 to not lock camera at position, otherwise unlocks once relevant enemies have been defeated.
    /// </summary>
    public float CamLockPosition = -1;
}