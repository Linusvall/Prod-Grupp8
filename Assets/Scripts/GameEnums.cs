public class GameEnums 
{
    public enum Directions
    {
        Natural  = 0,
        Right   = 1,
        Left = 2,
        Down = 3,
        Up = 4
    }

    public static readonly Directions[] ListOfDirections = { Directions.Right, Directions.Up, Directions.Left };
}
