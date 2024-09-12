public class GameEnums 
{
    public enum Direction
    {
        Netrual = 0,
        Right   = 1,
        Left = 2,
        Down = 3,
        Up = 4
    }

    public static readonly Direction[] ListOfDirections = { Direction.Right, Direction.Down, Direction.Left};
}
