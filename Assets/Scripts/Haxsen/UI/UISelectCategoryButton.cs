using System.Web;
using Haxsen.DataObjects;
using TMPro;
using UnityEngine;

namespace Haxsen.UI
{
    public class UISelectCategoryButton : MonoBehaviour
    {
        [SerializeField] private UICategoryManager uICategoryManager;
        
        [SerializeField] private TextMeshProUGUI textMesh;
        
        public CategoryStructure Category { get; set; }

        public void SetLabel(string label)
        {
            textMesh.text = HttpUtility.HtmlDecode(label);
        }

        public void UpdateSelectedCategory()
        {
            uICategoryManager.SetCategory(Category);
        }
    }
}