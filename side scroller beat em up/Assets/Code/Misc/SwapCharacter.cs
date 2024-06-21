using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacter : MonoBehaviour
{
    [SerializeField] Player playerSelecting;
    [SerializeField] Character selectedCharacter;
    private void OnEnable()
    {
        if (playerSelecting == Player.Player1)
            GameManager.Player1Character = selectedCharacter;
        else
            GameManager.Player2Character = selectedCharacter;
    }
}
