using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeeder : MonoBehaviour
{
    // Start is called before the first frame update
    public string seedString = "level 1";

    public static void SetSeed(int seed)
    {
        Random.InitState(seed);
    }

    public static void SetSeedBasedOnLevel(int level)
    {
        string newlevel = "level 000" + level;
        int seed = newlevel.GetHashCode();
        Random.InitState(seed);
    }
}
