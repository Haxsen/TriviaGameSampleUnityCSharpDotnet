using UnityEngine;

namespace Haxsen.ScriptableObjects
{
    [CreateAssetMenu(fileName = "UIColorOptionsSO", menuName = "ScriptableObjects/UIColorOptions", order = 1)]
    public class UIColorOptionsSO : ScriptableObject
    {
        [SerializeField] private Color correctColor = Color.green;
        [SerializeField] private Color incorrectColor = Color.red;

        public Color GetButtonColor(bool isCorrect) => isCorrect ? correctColor : incorrectColor;
    }
}