using UnityEngine;

[CreateAssetMenu(
    menuName = "Input/Icon Database"
)]
public class InputIconDatabase : ScriptableObject
{
    public Sprite keyboardInteract;
    public Sprite xboxInteract;

    public Sprite keyboardPrevious;
    public Sprite xboxPrevious;

    public Sprite keyboardNext;
    public Sprite xboxNext;
}