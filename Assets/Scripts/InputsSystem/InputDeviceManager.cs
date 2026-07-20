using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class InputDeviceManager : MonoBehaviour
{
    public static InputDeviceManager Instance;

    public InputDeviceType CurrentDevice
    {
        get;
        private set;
    } = InputDeviceType.KeyboardMouse;

    public event Action<InputDeviceType> OnDeviceChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        InputSystem.onEvent += OnInputEvent;
    }

    private void OnDisable()
    {
        InputSystem.onEvent -= OnInputEvent;
    }

    private void OnInputEvent(
        InputEventPtr eventPtr,
        InputDevice device)
    {
        if (!eventPtr.IsA<StateEvent>() &&
            !eventPtr.IsA<DeltaStateEvent>())
            return;

        if (device is Gamepad)
        {
            SetDevice(InputDeviceType.Xbox);
        }
        else if (device is Keyboard ||
                 device is Mouse)
        {
            SetDevice(InputDeviceType.KeyboardMouse);
        }
    }

    private void SetDevice(
        InputDeviceType device)
    {
        if (CurrentDevice == device)
            return;

        CurrentDevice = device;

        OnDeviceChanged?.Invoke(device);
    }
}

[System.Serializable]
public class InputIcon
{
    public InputActionType action;
    public Image image;
}