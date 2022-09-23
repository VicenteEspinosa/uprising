using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPLayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 6, true);
    }
}
