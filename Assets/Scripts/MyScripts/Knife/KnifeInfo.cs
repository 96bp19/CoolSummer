using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Knife",menuName ="Knifeinfo/knife")]
public class KnifeInfo : ScriptableObject
{
    public GameObject knifePrefab;
    public Texture2D knifetexture;

}

[CreateAssetMenu(fileName ="usedKnifes",menuName ="Knifeinfo/usedKnife")]
public class  UsedKnife: ScriptableObject
{
    public List<KnifeInfo> usedKnifes;
}


