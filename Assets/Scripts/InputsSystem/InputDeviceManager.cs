using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceManager : MonoBehaviour
{
    public static InputDeviceManager Instance;

    [SerializeField]
    private InputIconDatabase icons;

    public InputDeviceType CurrentDevice
    {
        get;
        private set;
    }

    public event Action OnDeviceChanged;

    private void Awake()
    {
        Instance = this;

        CurrentDevice =
            InputDeviceType.KeyboardMouse;
    }

    private void OnEnable()
    {
        InputSystem.onActionChange +=
            OnActionChange;
    }

    private void OnDisable()
    {
        InputSystem.onActionChange -=
            OnActionChange;
    }

    private void OnActionChange(
        object obj,
        InputActionChange change)
    {
        if (change !=
            InputActionChange.ActionPerformed)
            return;

        if (Keyboard.current != null &&
            Keyboard.current.anyKey.wasPressedThisFrame)
        {
            SetDevice(
                InputDeviceType.KeyboardMouse);

            return;
        }

        if (Gamepad.current != null &&
            Gamepad.current.wasUpdatedThisFrame)
        {
            SetDevice(
                InputDeviceType.Xbox);
        }
    }

    private void SetDevice(
        InputDeviceType device)
    {
        if (CurrentDevice == device)
            return;

        CurrentDevice = device;

        OnDeviceChanged?.Invoke();
    }

    public Sprite GetSprite(
        InputActionType action)
    {
        switch (action)
        {
            case InputActionType.Interact:

                return CurrentDevice ==
                    InputDeviceType.KeyboardMouse
                    ? icons.keyboardInteract
                    : icons.xboxInteract;

            case InputActionType.PreviousDrink:

                return CurrentDevice ==
                    InputDeviceType.KeyboardMouse
                    ? icons.keyboardPrevious
                    : icons.xboxPrevious;

            case InputActionType.NextDrink:

                return CurrentDevice ==
                    InputDeviceType.KeyboardMouse
                    ? icons.keyboardNext
                    : icons.xboxNext;
        }

        return null;
    }
}