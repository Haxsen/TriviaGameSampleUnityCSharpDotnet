using System.Web;
using Haxsen.DataObjects;
using TMPro;
using UnityEngine;

namespace Haxsen.UI
{
    /// <summary>
    /// Performs operation when the user selects a category.
    /// </summary>
    public class UISelectCategoryButton : MonoBehaviour
    {
        [Header("Functional component references")]
        [SerializeField] private UICategoryManager uICategoryManager;
        
        [Header("Self component references")]
        [SerializeField] private TextMeshProUGUI textMesh;
        
        public CategoryStructure Category { get; set; }

        /// <summary>
        /// Sets the label on this button.
        /// </summary>
        /// <param name="label">The string to set on this button label</param>
        public void SetLabel(string label)
        {
            textMesh.text = HttpUtility.HtmlDecode(label);
        }

        /// <summary>
        /// Update the category selection to this one when the user selects it.
        /// </summary>
        public void UpdateSelectedCategory()
        {
            uICategoryManager.SetCategory(Category);
        }
    }
}