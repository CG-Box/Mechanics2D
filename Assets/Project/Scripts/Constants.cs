public class Constants
{
    public static readonly string PlayerTag = "Player";

    


    #if UNITY_EDITOR
    public static readonly bool InUnityEditor = true;
    #else       
    public static readonly bool InUnityEditor = false;
    #endif
}
