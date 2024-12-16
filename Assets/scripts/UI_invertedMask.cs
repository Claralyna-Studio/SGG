using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UI_invertedMask : Image
{
    public override Material material
    {
        get
        {
            Material materials = new Material(base.material);
            material.SetInt("_StencilComp",(int)CompareFunction.NotEqual);
            return materials;
        }
    }
}
