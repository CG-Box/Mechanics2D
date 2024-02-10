public class Constants
{

    static readonly string PlayerStr = "Player";
    public static readonly string PlayerTag = PlayerStr;
    public static readonly string PlayerLayer = PlayerStr;
    public static readonly string DefaultLayer = "Everything";
    public static readonly string NewGameScene = "L00";
    public static readonly string MainMenuScene = "MainMenu";
    

    //public static bool GameManagerReady = false;


    #if UNITY_EDITOR
    public static readonly bool InUnityEditor = true;
    #else       
    public static readonly bool InUnityEditor = false;
    #endif
}
