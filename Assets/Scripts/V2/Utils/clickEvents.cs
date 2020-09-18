using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickEvents : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void setAsLastSibling(GameObject go)
    {
        go.transform.SetAsLastSibling();
    }
}
