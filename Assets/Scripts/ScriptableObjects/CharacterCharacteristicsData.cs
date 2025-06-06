using Core;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "Character Characteristics Data", menuName = "Scriptable Objects/Entity data")]
    public class CharacterCharacteristicsData : ScriptableObject
    {
        public CharacterCharacteristics characteristics;
    }
}