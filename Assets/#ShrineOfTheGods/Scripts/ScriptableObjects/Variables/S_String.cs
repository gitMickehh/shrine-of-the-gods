using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New String", menuName = "Basic/Variables/String")]
public class S_String : ScriptableObject
{
    [SerializeField] private string value;

    public string Value { get => value; set => this.value = value; }
}