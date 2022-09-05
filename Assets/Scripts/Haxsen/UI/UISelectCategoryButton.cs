using System.Web;
using TMPro;
using UnityEngine;

namespace Haxsen.UI
{
    public class UISelectCategoryButton : MonoBehaviour
    {
        [SerializeField] private UICategoryManager uICategoryManager;
        
        [SerializeField] private TextMeshProUGUI textMesh;
        
        public int CategoryIndex { get; set; }

        public void SetLabel(string label)
        {
            textMesh.text = HttpUtility.HtmlDecode(label);
        }

        public void UpdateSelectedCategory()
        {
            uICategoryManager.SetCategory(CategoryIndex);
        }
    }
}