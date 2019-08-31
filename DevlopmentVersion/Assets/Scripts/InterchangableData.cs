/*
 * InterchangableDate class
 * 
 * Saves info about chosen difficulty setting from the user in the start new game screen.
 * 
 * Author: Martin Schuster
 */

public static class InterchangableData
{
    public static Difficulty DifficultySetting;

    public static void SetDifficulty(string diff)
    {
        switch (diff)
        {
            case "Einfach":
                DifficultySetting = Difficulty.Easy;
                break;
            case "Mittel":
                DifficultySetting = Difficulty.Medium;
                break;
            case "Schwer":
                DifficultySetting = Difficulty.Hard;
                break;
            default:
                DifficultySetting = Difficulty.Easy;
                break; 
        }
    }
}
