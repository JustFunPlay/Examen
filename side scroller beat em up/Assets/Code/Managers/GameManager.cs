using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    public static Character Player1Character;
    public static int Player1Lives;
    public static int Player1Score;

    public static bool Player2Active;
    public static Character Player2Character;
    public static int Player2Lives;
    public static int Player2Score;

    public static Action OnChangeScore;
    public static Action OnChangeLives;

    public static bool CheckForRevive(Player player)
    {
        if (player == Player.Player1)
        {
            if (Player1Lives > 0)
            {
                Player1Lives--;
                OnChangeLives.Invoke();
                return true;
            }
        }
        else if (player == Player.Player2)
        {
            if (Player2Lives > 0)
            {
                Player2Lives--;
                OnChangeLives.Invoke();
                return true;
            }
        }
        return false;
    }
    public static int AddScoreToPlayer(Player player, int scoreToAdd)
    {
        if (player == Player.Player1)
        {
            Player1Score += scoreToAdd;
            OnChangeScore.Invoke();
            return Player1Score;
        }
        else if (player == Player.Player2)
        {
            Player2Score += scoreToAdd;
            OnChangeScore.Invoke();
            return Player2Score;
        }
        return -1;
    }
    public static void Reset()
    {
        Player1Lives = 3;
        Player2Lives = 3;
        Player1Score = 0;
        Player2Score = 0;
        Player2Active = false;
    }
}
