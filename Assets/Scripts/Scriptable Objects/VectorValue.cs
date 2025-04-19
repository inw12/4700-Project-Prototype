using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 value;
    public Vector2 defaultValue;

    public void OnAfterDeserialize() {
        value = defaultValue;
    }
    public void OnBeforeSerialize() {}

}
