using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollectible : MonoBehaviour
{
    // Update is called once per frame
    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
