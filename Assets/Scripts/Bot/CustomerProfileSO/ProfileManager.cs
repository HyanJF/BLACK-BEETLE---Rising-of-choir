using System.Collections.Generic;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [SerializeField]
    private CustomerProfileSO[] profiles;


    private List<CustomerProfileSO> availableProfiles =
        new();


    private void Awake()
    {
        ResetPool();
    }


    public CustomerProfileSO GetRandomProfile()
    {
        if (availableProfiles.Count == 0)
        {
            ResetPool();
        }

        int index =
            Random.Range(
                0,
                availableProfiles.Count);

        CustomerProfileSO profile =
            availableProfiles[index];

        availableProfiles.RemoveAt(index);

        return profile;
    }


    private void ResetPool()
    {
        availableProfiles =
            new List<CustomerProfileSO>(
                profiles);
    }
}