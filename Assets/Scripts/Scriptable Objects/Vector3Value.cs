using UnityEngine;

[CreateAssetMenu]
public class Vector3Value : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector3 value;
    public Vector3 defaultValue;

    public void OnAfterDeserialize() {
        value = defaultValue;
    }
    public void OnBeforeSerialize() {}

}
