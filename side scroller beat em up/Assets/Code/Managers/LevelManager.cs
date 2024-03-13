using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Vector3[] playerSpawnPoints = new Vector3[2];
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] float worldEdgeFront, worldEdgeBack;
    [SerializeField] float levelEndDistance;
    private List<Character> characters = new List<Character>();
    [SerializeField] private SceneTransition sceneTransition;
    [SerializeField] private PlayerUIManager player1UI, player2UI;

    [SerializeField] private EnemyWave[] enemyWaves;
    int currentWave = 0;
    int defeatedPlayers = 0;
    private void Start()
    {
        characters.Add(Instantiate(GameManager.Player1Character, playerSpawnPoints[0], Quaternion.identity));
        InputManager.Instance.SetPlayer1(characters[0]);
        cameraManager.Characters.Add(characters[0]);
        characters[0].InitializeCharacter(Player.Player1, worldEdgeFront, worldEdgeBack, this);
        player1UI.OnSetup(characters[0]);
        if (GameManager.Player2Active)
        {
            characters.Add(Instantiate(GameManager.Player2Character, playerSpawnPoints[1], Quaternion.identity));
            InputManager.Instance.SetPlayer2(characters[1]);
            cameraManager.Characters.Add(characters[1]);
            characters[1].InitializeCharacter(Player.Player2, worldEdgeFront, worldEdgeBack, this);
            player2UI.OnSetup(characters[1]);
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
        else
        {
            foreach (Character character in characters)
            {
                if (character.transform.position.x >= levelEndDistance)
                {
                    sceneTransition.LoadScene();
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
        if (enemyWaves[currentWave].CamLockPosition != -1)
            StartCoroutine(WaitToReleaseCamera(enemyWaves[currentWave].enemies, enemyWaves[currentWave].CamLockPosition));
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
            Debug.Log("Waiting to unlock camera");
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

    public void ConfirmDefeat()
    {
        defeatedPlayers++;
        if (defeatedPlayers >= characters.Count && sceneTransition != null)
        {
            bool retry = false;
            if (GameManager.CheckForRevive(Player.Player1)) retry = true;
            if (GameManager.CheckForRevive(Player.Player2)) retry = true;
            if (retry)
            {
                sceneTransition.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                sceneTransition.LoadScene(0);
            }
        }
    }
    public void DenyDefeat()
    {
        defeatedPlayers--;
    }
    public void RemoveFromCamera(Character characterToRemove)
    {
        cameraManager.Characters.Remove(characterToRemove);
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