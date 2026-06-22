
// Estados del bot, lugares
public enum PlaceType
{
    None,
    Bar,
    Table,
    Bathroom,
    Exit
}

// Deciones del bot
public enum BotDecision
{
    None,

    Wander,

    GoToBar,

    GoToTable,

    GoToBathroom,

    Exit
}

// Rutas del bot
public enum RouteType
{
    Shorter,
    AlittleLonger,
    Medium,
    Long,
    VeryLong
}