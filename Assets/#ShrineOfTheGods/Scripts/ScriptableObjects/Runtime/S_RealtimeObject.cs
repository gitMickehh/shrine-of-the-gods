using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_RealtimeObject<T> : ScriptableObject
{
    private T value = default(T);

    public T Value { get => value; set { this.value = value; onValueChanged?.Invoke(); } }

    public Action onValueChanged;
}
