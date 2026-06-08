using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Gameplay
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        public void Set(ItemStack stack)
        {
            if (stack == null || stack.Item == null)
            {
                _icon.enabled = false;
                _count.text = "";
                return;
            }

            var item = stack.Item;

            _icon.enabled = true;
            _icon.sprite = item.Icon;

            _count.text = stack.Item.IsStackable ? stack.Count.ToString() : "";
        }
    }
}