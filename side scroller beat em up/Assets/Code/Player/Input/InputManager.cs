using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public Character Player1;
    public Character Player2;
    InputType player1Input;
    InputType player2Input = InputType.ArrowKeys;
    [Header("WASD input")]
    [SerializeField] private InputAction wasdMoveInteraction;
    [SerializeField] private InputAction wasdJumpInteraction;
    [SerializeField] private InputAction wasdLightAttackInteraction;
    [SerializeField] private InputAction wasdHeavyAttackInteraction;
    [Header("Controller input")]
    [SerializeField] private InputAction controllerMoveInteraction;
    [SerializeField] private InputAction controllerJumpInteraction;
    [SerializeField] private InputAction controllerLightAttackInteraction;
    [SerializeField] private InputAction controllerHeavyAttackInteraction;
    [Header("Arrow Key input")]
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
        BindPlayer2();
    }
    private void Update()
    {
        if (ChangePlayer1Input)
        {
            SetPlayer1NextInput();
            ChangePlayer1Input = false;
        }
    }

    public void SetPlayer1(Character player1Character)
    {
        Player1 = player1Character;
    }
    public void SetPlayer2(Character player2Character)
    {
        Player2 = player2Character;
    }

    #region Player 1
    public InputType SetPlayer1NextInput()
    {
        UnbindPlayer1();
        switch (player1Input)
        {
            case InputType.WASD:
                if (GameManager.Player2Active && player2Input == InputType.ArrowKeys)
                    player1Input = InputType.Controller;
                else
                    player1Input = InputType.ArrowKeys;
                break;
            case InputType.ArrowKeys:
                if (GameManager.Player2Active && player2Input == InputType.Controller)
                    player1Input = InputType.WASD;
                else
                    player1Input = InputType.Controller;
                break;
            case InputType.Controller:
                if (GameManager.Player2Active && player2Input == InputType.WASD)
                    player1Input = InputType.ArrowKeys;
                else
                    player1Input = InputType.WASD;
                break;
        }
        BindPlayer1();
        return player1Input;
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
                wasdJumpInteraction.performed -= P1Jump;
                wasdLightAttackInteraction.performed -= P1LightAttack;
                wasdHeavyAttackInteraction.performed -= P1HeavyAttack;
                break;
            case InputType.ArrowKeys:
                arrowKeyMoveInteraction.performed -= P1Move;
                arrowKeyMoveInteraction.canceled -= P1Move;
                arrowKeyJumpInteraction.performed -= P1Jump;
                arrowKeyLightAttackInteraction.performed -= P1LightAttack;
                arrowKeyHeavyAttackInteraction.performed -= P1HeavyAttack;
                break;
            case InputType.Controller:
                controllerMoveInteraction.performed -= P1Move;
                controllerMoveInteraction.canceled -= P1Move;
                controllerJumpInteraction.performed -= P1Jump;
                controllerLightAttackInteraction.performed -= P1LightAttack;
                controllerHeavyAttackInteraction.performed -= P1HeavyAttack;
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
    #endregion

    #region Player 2
    public InputType SetPlayer2NextInput()
    {
        UnbindPlayer2();
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
        BindPlayer2();
        return player2Input;
    }

    private void BindPlayer2()
    {
        switch (player2Input)
        {
            case InputType.WASD:
                wasdMoveInteraction.performed += P2Move;
                wasdMoveInteraction.canceled += P2Move;
                wasdMoveInteraction.Enable();
                wasdJumpInteraction.performed += P2Jump;
                wasdJumpInteraction.Enable();
                wasdLightAttackInteraction.performed += P2LightAttack;
                wasdLightAttackInteraction.Enable();
                wasdHeavyAttackInteraction.performed += P2HeavyAttack;
                wasdHeavyAttackInteraction.Enable();
                break;
            case InputType.ArrowKeys:
                arrowKeyMoveInteraction.performed += P2Move;
                arrowKeyMoveInteraction.canceled += P2Move;
                arrowKeyMoveInteraction.Enable();
                arrowKeyJumpInteraction.performed += P2Jump;
                arrowKeyJumpInteraction.Enable();
                arrowKeyLightAttackInteraction.performed += P2LightAttack;
                arrowKeyLightAttackInteraction.Enable();
                arrowKeyHeavyAttackInteraction.performed += P2HeavyAttack;
                arrowKeyHeavyAttackInteraction.Enable();
                break;
            case InputType.Controller:
                controllerMoveInteraction.performed += P2Move;
                controllerMoveInteraction.canceled += P2Move;
                controllerMoveInteraction.Enable();
                controllerJumpInteraction.performed += P2Jump;
                controllerJumpInteraction.Enable();
                controllerLightAttackInteraction.performed += P2LightAttack;
                controllerLightAttackInteraction.Enable();
                controllerHeavyAttackInteraction.performed += P2HeavyAttack;
                controllerHeavyAttackInteraction.Enable();
                break;
        }
    }
    private void UnbindPlayer2()
    {
        switch (player2Input)
        {
            case InputType.WASD:
                wasdMoveInteraction.performed -= P2Move;
                wasdMoveInteraction.canceled -= P2Move;
                wasdJumpInteraction.performed -= P2Jump;
                wasdLightAttackInteraction.performed -= P2LightAttack;
                wasdHeavyAttackInteraction.performed -= P2HeavyAttack;
                break;
            case InputType.ArrowKeys:
                arrowKeyMoveInteraction.performed -= P2Move;
                arrowKeyMoveInteraction.canceled -= P2Move;
                arrowKeyJumpInteraction.performed -= P2Jump;
                arrowKeyLightAttackInteraction.performed -= P2LightAttack;
                arrowKeyHeavyAttackInteraction.performed -= P2HeavyAttack;
                break;
            case InputType.Controller:
                controllerMoveInteraction.performed -= P2Move;
                controllerMoveInteraction.canceled -= P2Move;
                controllerJumpInteraction.performed -= P2Jump;
                controllerLightAttackInteraction.performed -= P2LightAttack;
                controllerHeavyAttackInteraction.performed -= P2HeavyAttack;
                break;
        }
    }

    private void P2Move(InputAction.CallbackContext callbackContext)
    {
        if (Player2 != null)
            Player2.Move(callbackContext.ReadValue<Vector2>());
    }
    private void P2Jump(InputAction.CallbackContext callbackContext)
    {
        if (Player2 != null)
            Player2.Jump();
    }
    private void P2LightAttack(InputAction.CallbackContext callbackContext)
    {
        if (Player2 != null)
            Player2.Attack(AttackType.Light);
    }
    private void P2HeavyAttack(InputAction.CallbackContext callbackContext)
    {
        if (Player2 != null)
            Player2.Attack(AttackType.Heavy);
    }
    #endregion
}

public enum InputType
{
    WASD,
    ArrowKeys,
    Controller
}
