using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        Destroy(transform.parent.gameObject);
    }
}