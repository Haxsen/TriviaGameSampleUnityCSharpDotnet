using UnityEngine;

namespace Haxsen
{
    public class Helper : MonoBehaviour
    {
        public static void DestroyChildrenOfList(Transform rootTransform, int startingIndex = 0)
        {
            for (int i = startingIndex; i < rootTransform.childCount; i++)
            {
                Destroy(rootTransform.GetChild(i).gameObject);
            }
        }
    }
}