using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ScoreThreshold
{
    [SerializeField] private int goal;
    [SerializeField] private float multiplier;
    public int Score => goal;
    public float Multiplier => multiplier;
}
