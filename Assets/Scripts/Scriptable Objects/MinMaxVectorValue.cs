using UnityEngine;

[CreateAssetMenu]
public class MinMaxVectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 max;
    public Vector2 min;
    public Vector2 defaultMax;
    public Vector2 defaultMin;

    public void OnAfterDeserialize() {
        max = defaultMax;
        min = defaultMin;
    }
    public void OnBeforeSerialize() {}

}
