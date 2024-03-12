using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static Character Player1Character;
    public static int Player1Lives;
    public static int Player1Score;

    public static bool Player2Active;
    public static Character Player2Character;
    public static int Player2Lives;
    public static int Player2Score;

    public static bool CheckForRevive(Player player)
    {
        if (player == Player.Player1)
        {
            if (Player1Lives > 0)
            {
                Player1Lives--;
                return true;
            }
        }
        else if (player == Player.Player2)
        {
            if (Player2Lives > 0)
            {
                Player2Lives--;
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
            return Player1Score;
        }
        else if (player == Player.Player2)
        {
            Player2Score += scoreToAdd;
            return Player2Score;
        }
        return -1;
    }
}
