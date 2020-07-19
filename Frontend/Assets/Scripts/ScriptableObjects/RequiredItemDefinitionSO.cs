using UnityEngine;

namespace ScriptableObjects
{
    //Items required by the editor, and likely ship with the game
    [CreateAssetMenu]
    public class RequiredItemDefinitionSO<T> : ScriptableObject
    {
        //could do fakes or something, least effort being the using/switch, but that's only compile time
        //however, code immediately using this class should know that this is "baked" data, and be aware that maybe this isn't desired before passing T around to other classes
        [SerializeField] private T Data; //one example of how to nest structured data
    }
}
