using UnityEngine;

public class VisitGoals
{
    public int DrinksConsumed = 0;
    public int SocialActivities = 0;

    public int RequiredDrinks = 2;
    public int RequiredSocialActivities = 2;

    public bool VisitCompleted =>
        DrinksConsumed >= RequiredDrinks &&
        SocialActivities >= RequiredSocialActivities;
}
