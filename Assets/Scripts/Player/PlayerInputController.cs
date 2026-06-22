using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private MovementControllerAnimation movement;
    private Vector2 input;

    private void Awake()
    {
        movement = GetComponent<MovementControllerAnimation>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        movement.SetMovement(input);
    }
}