using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    public float aliveTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyAfterTime", aliveTime);
    }

    void DestroyAfterTime()
    {
        Destroy(this.gameObject);
    }

  
}
