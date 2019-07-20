using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectInformation
{
    public int ID;
    public float[] Position = new float[3];
    public float[] Quaternion = new float[4];
    public float[] Scale = new float[3];

    public ObjectPrefab objectsPrefab;

    public List<int> childrenObjectsID = new List<int>();

    public ObjectInformation()
    {

    }
}
