using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public Character Player1;
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
                wasdMoveInteraction.performed += P1Move;
                wasdMoveInteraction.canceled += P1Move;
                wasdMoveInteraction.Enable();
                wasdJumpInteraction.performed += P1Jump;
                wasdJumpInteraction.Enable();
                wasdLightAttackInteraction.performed += P1LightAttack;
                wasdLightAttackInteraction.Enable();
                wasdHeavyAttackInteraction.performed += P1HeavyAttack;
                wasdHeavyAttackInteraction.Enable();
                break;
            case InputType.ArrowKeys:
                arrowKeyMoveInteraction.performed += P1Move;
                arrowKeyMoveInteraction.canceled += P1Move;
                arrowKeyMoveInteraction.Enable();
                arrowKeyJumpInteraction.performed += P1Jump;
                arrowKeyJumpInteraction.Enable();
                arrowKeyLightAttackInteraction.performed += P1LightAttack;
                arrowKeyLightAttackInteraction.Enable();
                arrowKeyHeavyAttackInteraction.performed += P1HeavyAttack;
                arrowKeyHeavyAttackInteraction.Enable();
                break;
            case InputType.Controller:
                controllerMoveInteraction.performed += P1Move;
                controllerMoveInteraction.canceled += P1Move;
                controllerMoveInteraction.Enable();
                controllerJumpInteraction.performed += P1Jump;
                controllerJumpInteraction.Enable();
                controllerLightAttackInteraction.performed += P1LightAttack;
                controllerLightAttackInteraction.Enable();
                controllerHeavyAttackInteraction.performed += P1HeavyAttack;
                controllerHeavyAttackInteraction.Enable();
                break;
        }
    }
    private void UnbindPlayer1()
    {
        switch (player1Input)
        {
            case InputType.WASD:
                wasdMoveInteraction.performed -= P1Move;
                wasdMoveInteraction.canceled -= P1Move;
                wasdMoveInteraction.Disable();
                wasdJumpInteraction.performed -= P1Jump;
                wasdJumpInteraction.Disable();
                wasdLightAttackInteraction.performed -= P1LightAttack;
                wasdLightAttackInteraction.Disable();
                wasdHeavyAttackInteraction.performed -= P1HeavyAttack;
                wasdHeavyAttackInteraction.Disable();
                break;
            case InputType.ArrowKeys:
                arrowKeyMoveInteraction.performed -= P1Move;
                arrowKeyMoveInteraction.canceled -= P1Move;
                arrowKeyMoveInteraction.Disable();
                arrowKeyJumpInteraction.performed -= P1Jump;
                arrowKeyJumpInteraction.Disable();
                arrowKeyLightAttackInteraction.performed -= P1LightAttack;
                arrowKeyLightAttackInteraction.Disable();
                arrowKeyHeavyAttackInteraction.performed -= P1HeavyAttack;
                arrowKeyHeavyAttackInteraction.Disable();
                break;
            case InputType.Controller:
                controllerMoveInteraction.performed -= P1Move;
                controllerMoveInteraction.canceled -= P1Move;
                controllerMoveInteraction.Disable();
                controllerJumpInteraction.performed -= P1Jump;
                controllerJumpInteraction.Disable();
                controllerLightAttackInteraction.performed -= P1LightAttack;
                controllerLightAttackInteraction.Disable();
                controllerHeavyAttackInteraction.performed -= P1HeavyAttack;
                controllerHeavyAttackInteraction.Disable();
                break;
        }
    }

    private void P1Move(InputAction.CallbackContext callbackContext)
    {
        if (Player1 != null)
            Player1.Move(callbackContext.ReadValue<Vector2>());
    }
    private void P1Jump(InputAction.CallbackContext callbackContext)
    {
        if (Player1 != null)
            Player1.Jump();
    }
    private void P1LightAttack(InputAction.CallbackContext callbackContext)
    {
        if (Player1 != null)
            Player1.Attack(AttackType.Light);
    }
    private void P1HeavyAttack(InputAction.CallbackContext callbackContext)
    {
        if (Player1 != null)
            Player1.Attack(AttackType.Heavy);
    }

}

public enum InputType
{
    WASD,
    ArrowKeys,
    Controller
}
