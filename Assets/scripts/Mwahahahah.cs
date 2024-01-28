using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public string[] keys = new string[] { "w", "a", "s", "d" };
    public bool AnyKeyDown(IEnumerable<string> keys)
    {
        foreach (string key in keys)
        {
            if (Input.GetKeyDown(key)) return true;
        }
        return false;
    }
    void Update()
    {
        
    }
}
