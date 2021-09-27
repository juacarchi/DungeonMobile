using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleGrow : MonoBehaviour
{
    private void Update()
    {
        this.transform.localScale += new Vector3(Time.deltaTime*0.5f, Time.deltaTime*0.4f);
    }
}
