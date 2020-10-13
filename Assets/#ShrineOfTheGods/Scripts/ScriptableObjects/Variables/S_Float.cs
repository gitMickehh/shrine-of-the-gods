using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Float", menuName = "Basic/Variables/Float")]
public class S_Float : ScriptableObject
{
    [SerializeField] private float value;

    public float Value { get => value; set => this.value = value; }
}