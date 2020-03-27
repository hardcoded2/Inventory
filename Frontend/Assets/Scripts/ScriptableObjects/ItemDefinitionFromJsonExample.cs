using System;
using UnityEngine;

namespace ScriptableObjects
{
    public class ItemDefinitionFromJsonExample : ISerializationCallbackReceiver
    {
        [Serializable]
        public class ExampleDataToSet
        {
            public float Awesome;
        }

        [SerializeField] private ExampleDataToSet m_Data;

        [SerializeField] private string m_ExampleJsonToDeserialize;
        //an example of a scriptable object being fed by json data if we want another transport/etc
        //odin inspector does a more in-depth version of this under the hood
        public void OnBeforeSerialize()
        {
            //relies on the unity serializer for simplicity's sake
            //can use deeper serialized property setter/etc to avoid much of the thrashing here
            m_Data = JsonUtility.FromJson<ExampleDataToSet>(m_ExampleJsonToDeserialize);
        }

        public void OnAfterDeserialize()
        {
            m_ExampleJsonToDeserialize = JsonUtility.ToJson(m_Data);
        }
    }
}
