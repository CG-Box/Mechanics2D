public class Constants
{
    public static readonly string PlayerTag = "Player";

    //public static bool GameManagerReady = false;


    #if UNITY_EDITOR
    public static readonly bool InUnityEditor = true;
    #else       
    public static readonly bool InUnityEditor = false;
    #endif
}
