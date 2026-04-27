using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private bool _isJump;
    private bool _isAttack;
    private bool _isVampirism;

    public event Action<float> Moving;
    public event Action Attacking;
    public event Action GainingHealth;

    public float Direction {  get; private set; }

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();

        _inputActions.Enable();
        _inputActions.Player.Jump.performed += JumpPerformed;
        _inputActions.Player.Attack.performed += AttackPerformed;
        _inputActions.Player.Sprint.performed += VampirismPerformed;
    }

    private void Update()
    {
        Vector2 move = _inputActions.Player.Move.ReadValue<Vector2>();
        Direction = move.x;

        Moving?.Invoke(Direction);
    }    

    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= JumpPerformed;
        _inputActions.Player.Attack.performed -= AttackPerformed;
        _inputActions.Player.Sprint.performed -= VampirismPerformed;
        _inputActions.Disable();        
    }

    public bool GetIsJump()
    {
        return _isJump;
    }

    public void ResetJumpFlag()
    {
        _isJump = false;
    }

    private void JumpPerformed(InputAction.CallbackContext context)
    {
        _isJump = true;
    }

    public bool GetIsAttack()
    {
        bool result = _isAttack;
        _isAttack = false;
        return result;
    }

    private void AttackPerformed(InputAction.CallbackContext context)
    {
        Attacking?.Invoke();
        _isAttack = true;
    }

    public bool GetIsVampirism()
    {
        bool result = _isVampirism;
        _isVampirism = false;
        return result;
    }

    private void VampirismPerformed(InputAction.CallbackContext context)
    {
        GainingHealth?.Invoke();
        _isVampirism = true;
    }
}