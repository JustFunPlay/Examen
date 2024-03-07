using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInput : MonoBehaviour
{
    [SerializeField] Player inputPlayer;

    [SerializeField] GameObject wASDVisual;
    [SerializeField] GameObject arrowVisual;
    [SerializeField] GameObject controllerVisual;

    public void SwitchPlayerInput()
    {
        wASDVisual.SetActive(false);
        arrowVisual.SetActive(false);
        controllerVisual.SetActive(false);

        InputType selectedInputtype = new InputType();
        if (inputPlayer == Player.player1)
            selectedInputtype = InputManager.Instance.SetPlayer1NextInput();
        else
            selectedInputtype = InputManager.Instance.SetPlayer2NextInput();

        switch (selectedInputtype)
        {
            case InputType.WASD:
                wASDVisual.SetActive(true);
                break;
            case InputType.ArrowKeys:
                arrowVisual.SetActive(true);
                break;
            case InputType.Controller:
                controllerVisual.SetActive(true);
                break;
        }
    }
}

    public enum Player { player1, player2 };