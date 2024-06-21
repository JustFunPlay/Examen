using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSecondPlayer : MonoBehaviour
{
    public void EnableSecondPlayer(bool enable)
    {
        GameManager.Player2Active = enable;
    }
}
