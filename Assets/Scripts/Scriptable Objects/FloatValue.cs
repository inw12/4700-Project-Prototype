using System;
using UnityEngine;

// • A script INDEPENDENT from objects in the scene
// • Basically a custom class

[CreateAssetMenu]   // Creates this script as an object
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float value;

    [NonSerialized] public float runtimeValue;

    public void OnAfterDeserialize()    // what values to retain/reset when game ends
    {
        runtimeValue = value;
    } 

    public void OnBeforeSerialize() {}
}
