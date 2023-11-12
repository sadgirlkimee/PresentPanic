using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace YourNamespace
{
    [CreateAssetMenu(fileName = "CharacterDatabase", menuName = "Custom/Character Database", order = 1)]
    public class CharacterDatabase : ScriptableObject
    {
        public Character[] character;

        public int CharacterCount
        {
            get { return character.Length; }
        }

        public Character GetCharacter(int index)
        {
            return character[index];
        }
    }
}
#endif
