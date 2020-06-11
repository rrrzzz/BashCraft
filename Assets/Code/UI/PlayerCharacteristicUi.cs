using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class PlayerCharacteristicUi : MonoBehaviour
    {
        public Image Icon { get; private set; }
        public Text Text { get; private set; }
        
        private void OnEnable()
        {
            Text = GetComponentInChildren<Text>();
            Icon = GetComponentsInChildren<Image>()[1];
        }
    }
}