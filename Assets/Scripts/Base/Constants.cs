using UnityEngine;

public class Constants
{
    public class Vectors
    {
        public static readonly Vector3 Zero = new Vector3();
    }
}

public static class Extensions
{
}

public static class Helpers
{
    public static Vector3 GetRandomAndNormalizeVector3()
    {
        var result = new Vector3
        {
            x = Random.Range(-1f, 1f),
            y = Random.Range(-1f, 1f),
            z = Random.Range(-1f, 1f)
        };
        return result.normalized;
    }
}