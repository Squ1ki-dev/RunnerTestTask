using UnityEngine;

public static class TargetAppFramerate
{
    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {
        Application.targetFrameRate = 60;
    }
}
