using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField]
    private InputIconDatabase icons;

    public event Action OnInputDeviceChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputDeviceManager.Instance.OnDeviceChanged +=
            HandleDeviceChanged;
    }

    private void OnDisable()
    {
        if (InputDeviceManager.Instance != null)
        {
            InputDeviceManager.Instance.OnDeviceChanged -=
                HandleDeviceChanged;
        }
    }

    private void HandleDeviceChanged(
    InputDeviceType device)
    {

        OnInputDeviceChanged?.Invoke();
    }

    public Sprite GetSprite(
        InputActionType action)
    {
        bool keyboard =
            InputDeviceManager.Instance.CurrentDevice ==
            InputDeviceType.KeyboardMouse;

        switch (action)
        {
            case InputActionType.Interact:

                return keyboard
                    ? icons.keyboardInteract
                    : icons.xboxInteract;

            case InputActionType.PreviousDrink:

                return keyboard
                    ? icons.keyboardPrevious
                    : icons.xboxPrevious;

            case InputActionType.NextDrink:

                return keyboard
                    ? icons.keyboardNext
                    : icons.xboxNext;
        }

        return null;
    }
}