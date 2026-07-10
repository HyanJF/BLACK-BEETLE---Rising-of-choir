using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [SerializeField]
    private CustomerProfileSO[] profiles;

    public CustomerProfileSO GetRandomProfile()
    {
        if (profiles == null ||
            profiles.Length == 0)
            return null;

        return profiles[
            Random.Range(0, profiles.Length)];
    }
}