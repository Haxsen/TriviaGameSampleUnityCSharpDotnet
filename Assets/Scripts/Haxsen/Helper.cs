using UnityEngine;

namespace Haxsen
{
    /// <summary>
    /// A basic helper class.
    /// </summary>
    public class Helper : MonoBehaviour
    {
        /// <summary>
        /// Destroys a transform's children from given index.
        /// </summary>
        /// <param name="rootTransform">The parent transform</param>
        /// <param name="startingIndex">The index to start from</param>
        public static void DestroyChildrenOfList(Transform rootTransform, int startingIndex = 0)
        {
            for (int i = startingIndex; i < rootTransform.childCount; i++)
            {
                Destroy(rootTransform.GetChild(i).gameObject);
            }
        }
    }
}