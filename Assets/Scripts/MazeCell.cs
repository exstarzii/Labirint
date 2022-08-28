using System.Collections;
using System.Collections.Generic;

public class MazeCell
{
    public bool topBorder;
    public bool bottomBorder;
    public bool leftBorder;
    public bool rightBorder;

    // номер области
    public int value;

    public MazeCell()
    {
        topBorder = false;
        bottomBorder = false;
        leftBorder = false;
        rightBorder = false;
        value = 0;
    }
}
