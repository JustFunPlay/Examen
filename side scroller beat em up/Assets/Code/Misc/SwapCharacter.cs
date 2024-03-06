using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacter : MonoBehaviour
{
    [SerializeField] Player playerSelecting;
    [SerializeField] Character[] characters;
    private void Start()
    {
        if (playerSelecting == Player.player1)
            GameManager.Player1Character = characters[0];
        else
            GameManager.Player2Character = characters[0];
    }
    public void SwapToNextCharacter()
    {
        if (playerSelecting == Player.player1)
        {
            int i = 0;
            Character selectedCharacter = null;
            while (selectedCharacter == null)
            {
                if (characters[i] == GameManager.Player1Character)
                {
                    selectedCharacter = characters[i];
                }
                i++;
            }
            if (i == characters.Length) i = 0;
            GameManager.Player1Character = characters[i];
        }
        else
        {
            int i = 0;
            Character selectedCharacter = null;
            while (selectedCharacter == null)
            {
                if (characters[i] == GameManager.Player2Character)
                {
                    selectedCharacter = characters[i];
                }
                i++;
            }
            if (i == characters.Length) i = 0;
            GameManager.Player2Character = characters[i];
        }
    }
}
