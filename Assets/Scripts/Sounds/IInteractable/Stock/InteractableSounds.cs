using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class InteractableSounds : MonoBehaviour
{
    #region ReferenceStock

    [SerializeField]
    private AudioClip takeDrink;

    [SerializeField]
    private AudioClip restoredStock;

    #endregion

    #region ReferenceCustomer

    [SerializeField] 
    private AudioClip arrive;

    [SerializeField]
    private AudioClip dialogue;

    [SerializeField]
    private AudioClip incorrect;

    [SerializeField]
    private AudioClip correct;

    [SerializeField]
    private AudioClip empty;

    [SerializeField]
    private AudioClip happy;

    #endregion

    #region Unity

    public static InteractableSounds instance;

    public void Awake()
    {
        instance = this;
    }

    #endregion

    #region Stock

    public void TakedrinkPlaySound()
    {
        GameDataBase.Instance.soundController.PlaySoundInteracion(takeDrink);
    }

    public void RestoredPlaySound()
    {
        GameDataBase.Instance.soundController.PlaySoundInteracion(restoredStock);
    }

    #endregion

    #region Customer

    public void Play(InteractionSoundType type)
    {
        switch (type)
        {
            case InteractionSoundType.Greeting:
                GameDataBase.Instance.soundController
                    .PlaySoundInteracion(arrive);
                break;

            case InteractionSoundType.Ordering:
                GameDataBase.Instance.soundController
                    .PlaySoundInteracion(dialogue);
                break;

            case InteractionSoundType.Served:
                GameDataBase.Instance.soundController
                    .PlaySoundInteracion(correct);
                break;

            case InteractionSoundType.EmptyHands:
                GameDataBase.Instance.soundController
                    .PlaySoundInteracion(empty);
                break;

            case InteractionSoundType.WrongDrink:
                GameDataBase.Instance.soundController
                    .PlaySoundInteracion(incorrect);
                break;

            case InteractionSoundType.Leaving:
                GameDataBase.Instance.soundController
                    .PlaySoundInteracion(happy);
                break;

            case InteractionSoundType.LeavingAngry:
                GameDataBase.Instance.soundController
                    .PlaySoundInteracion(incorrect);
                break;
        }
    }

    #endregion
}
