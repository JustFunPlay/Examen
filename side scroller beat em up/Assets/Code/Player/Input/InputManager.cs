using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    //player 1 character
    //player 2 character
    InputType player1Input;
    InputType player2Input;
    bool player2Active;
    [SerializeField] private InputAction wasdMoveInteraction;
    [SerializeField] private InputAction wasdJumpInteraction;
    [SerializeField] private InputAction wasdLightAttackInteraction;
    [SerializeField] private InputAction wasdHeavyAttackInteraction;
    [SerializeField] private InputAction controllerMoveInteraction;
    [SerializeField] private InputAction controllerJumpInteraction;
    [SerializeField] private InputAction controllerLightAttackInteraction;
    [SerializeField] private InputAction controllerHeavyAttackInteraction;
    [SerializeField] private InputAction arrowKeyMoveInteraction;
    [SerializeField] private InputAction arrowKeyJumpInteraction;
    [SerializeField] private InputAction arrowKeyLightAttackInteraction;
    [SerializeField] private InputAction arrowKeyHeavyAttackInteraction;

    [Header("editor testing")]
    public bool ChangePlayer1Input;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        BindPlayer1();
    }
    private void Update()
    {
        if (ChangePlayer1Input)
        {
            SetPlayer1NextInput();
            ChangePlayer1Input = false;
        }
    }

    public InputType SetPlayer1NextInput()
    {
        UnbindPlayer1();
        switch (player1Input)
        {
            case InputType.WASD:
                if (player2Active && player2Input == InputType.ArrowKeys)
                    player1Input = InputType.Controller;
                else
                    player1Input = InputType.ArrowKeys;
                break;
            case InputType.ArrowKeys:
                if (player2Active && player2Input == InputType.Controller)
                    player1Input = InputType.WASD;
                else
                    player1Input = InputType.Controller;
                break;
            case InputType.Controller:
                if (player2Active && player2Input == InputType.WASD)
                    player1Input = InputType.ArrowKeys;
                else
                    player1Input = InputType.WASD;
                break;
        }
        BindPlayer1();
        return player1Input;
    }
    public InputType SetPlayer2NextInput()
    {
        switch (player2Input)
        {
            case InputType.WASD:
                if (player1Input == InputType.ArrowKeys)
                    player2Input = InputType.Controller;
                else
                    player2Input = InputType.ArrowKeys;
                break;
            case InputType.ArrowKeys:
                if (player1Input == InputType.Controller)
                    player2Input = InputType.WASD;
                else
                    player2Input = InputType.Controller;
                break;
            case InputType.Controller:
                if (player1Input == InputType.WASD)
                    player2Input = InputType.ArrowKeys;
                else
                    player2Input = InputType.WASD;
                break;
        }
        return player2Input;
    }

    private void BindPlayer1()
    {
        switch(player1Input)
        {
            case InputType.WASD:
                wasdMoveInteraction.started += P1Move;
                wasdMoveInteraction.canceled += P1Move;
                wasdMoveInteraction.performed += P1Move;
                wasdMoveInteraction.Enable();
                wasdJumpInteraction.started += P1Jump;
                wasdJumpInteraction.Enable();
                wasdLightAttackInteraction.started += P1LightAttack;
                wasdLightAttackInteraction.Enable();
                wasdHeavyAttackInteraction.started += P1HeavyAttack;
                wasdHeavyAttackInteraction.Enable();
                Debug.Log("WASD has been bound to player 1");
                break;
            case InputType.ArrowKeys:
                arrowKeyMoveInteraction.started += P1Move;
                arrowKeyMoveInteraction.canceled += P1Move;
                arrowKeyMoveInteraction.performed += P1Move;
                arrowKeyMoveInteraction.Enable();
                arrowKeyJumpInteraction.started += P1Jump;
                arrowKeyJumpInteraction.Enable();
                arrowKeyLightAttackInteraction.started += P1LightAttack;
                arrowKeyLightAttackInteraction.Enable();
                arrowKeyHeavyAttackInteraction.started += P1HeavyAttack;
                arrowKeyHeavyAttackInteraction.Enable();
                break;
            case InputType.Controller:
                controllerMoveInteraction.started += P1Move;
                controllerMoveInteraction.canceled += P1Move;
                controllerMoveInteraction.performed += P1Move;
                controllerMoveInteraction.Enable();
                controllerJumpInteraction.started += P1Jump;
                controllerJumpInteraction.Enable();
                controllerLightAttackInteraction.started += P1LightAttack;
                controllerLightAttackInteraction.Enable();
                controllerHeavyAttackInteraction.started += P1HeavyAttack;
                controllerHeavyAttackInteraction.Enable();
                break;
        }
        Debug.Log($"wasd move bindings: {wasdMoveInteraction.bindings}\narrowKey move bindings: {arrowKeyMoveInteraction.bindings}\ncontroller move bindings: {controllerMoveInteraction.bindings}");
    }
    private void UnbindPlayer1()
    {
        switch (player1Input)
        {
            case InputType.WASD:
                wasdMoveInteraction.started -= P1Move;
                wasdMoveInteraction.canceled -= P1Move;
                wasdMoveInteraction.performed -= P1Move;
                wasdMoveInteraction.Disable();
                wasdJumpInteraction.started -= P1Jump;
                wasdJumpInteraction.Disable();
                wasdLightAttackInteraction.started -= P1LightAttack;
                wasdLightAttackInteraction.Disable();
                wasdHeavyAttackInteraction.started -= P1HeavyAttack;
                wasdHeavyAttackInteraction.Disable();
                break;
            case InputType.ArrowKeys:
                arrowKeyMoveInteraction.started -= P1Move;
                arrowKeyMoveInteraction.canceled -= P1Move;
                arrowKeyMoveInteraction.performed -= P1Move;
                arrowKeyMoveInteraction.Disable();
                arrowKeyJumpInteraction.started -= P1Jump;
                arrowKeyJumpInteraction.Disable();
                arrowKeyLightAttackInteraction.started -= P1LightAttack;
                arrowKeyLightAttackInteraction.Disable();
                arrowKeyHeavyAttackInteraction.started -= P1HeavyAttack;
                arrowKeyHeavyAttackInteraction.Disable();
                break;
            case InputType.Controller:
                controllerMoveInteraction.started -= P1Move;
                controllerMoveInteraction.canceled -= P1Move;
                controllerMoveInteraction.performed -= P1Move;
                controllerMoveInteraction.Disable();
                controllerJumpInteraction.started -= P1Jump;
                controllerJumpInteraction.Disable();
                controllerLightAttackInteraction.started -= P1LightAttack;
                controllerLightAttackInteraction.Disable();
                controllerHeavyAttackInteraction.started -= P1HeavyAttack;
                controllerHeavyAttackInteraction.Disable();
                break;
        }
    }

    private void P1Move(InputAction.CallbackContext callbackContext)
    {
        Debug.Log($"Player 1 moveValue: {callbackContext.ReadValue<Vector2>()}");
    }
    private void P1Jump(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Player 1 jumped");
    }
    private void P1LightAttack(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Player 1 performed light attack");
    }
    private void P1HeavyAttack(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Player 1 performed heavy attack");

    }

}

public enum InputType
{
    WASD,
    ArrowKeys,
    Controller
}
