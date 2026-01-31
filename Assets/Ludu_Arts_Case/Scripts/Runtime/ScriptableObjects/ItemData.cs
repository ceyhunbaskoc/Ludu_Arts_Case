using UnityEngine;

namespace LuduArtsCase.Runtime.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "LuduArts/Inventory/ItemData")]
    
    public class ItemData : ScriptableObject
    {
        #region Fields
        
        [SerializeField] private GameObject m_Prefab;
        
        [SerializeField] private string m_ID;

        [SerializeField] private string m_DisplayName;

        [SerializeField] private Sprite m_Icon;

        [SerializeField] private bool m_IsStackable = true;

        #endregion
        
        #region Properties

        public GameObject Prefab => m_Prefab;
        public string ID => m_ID;
        public string DisplayName => m_DisplayName;
        public Sprite Icon => m_Icon;
        public bool IsStackable => m_IsStackable;

        #endregion
    }
}
