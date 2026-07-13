public enum DrinkType
{
    None = 0,
    Beer = 1,
    Wine = 2,
    Cocktail = 3,
    Soda = 4
}

public enum ServeResult
{
    Success,
    OrderFinished,

    EmptyHands,
    WrongDrink
}

public enum SeatAvailability
{
    Free,
    Reserved,
    Occupied
}

public enum ThoughtType
{
    None,

    Greeting,
    Ordering,
    EmptyHands,
    Waiting,
    Served,
    Angry,
    Leaving
}

public enum InteractionSoundType
{
    None,

    Greeting,
    Ordering,
    Served,
    EmptyHands,
    WrongDrink,
    Leaving,
    LeavingAngry
}