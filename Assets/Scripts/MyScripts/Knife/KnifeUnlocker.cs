using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnifeUnlocker : MonoBehaviour
{
    public UsedKnife allknife;
    public UsedKnife unlockedKnife;
    public UsedKnife lockedKnife;

    public  int UnlockRandomKnife()
    {

        int randomval = Random.Range(0, lockedKnife.usedKnifes.Count);
        unlockedKnife.usedKnifes.Add(lockedKnife.usedKnifes[randomval]);
        return allknife.usedKnifes.IndexOf(lockedKnife.usedKnifes[randomval]);

    }
}
