using System.Collections;
using System.Collections.Generic;
using Code.Data.GameDataTypes;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class PlayerUi : MonoBehaviour, IPlayerUi
    {
        private const int StatsRootIndex = 0;
        private const float DisabledTextAlpha = 0.3f;
        private const float EnabledTextAlpha = 1;
        
        public int Id { get; set; }
        public Transform StatsRootTransform { get; private set; }
        public Button AttackBtn { get; private set; }
        public RectTransform RectTransform { get; private set; }
        public Dictionary<StatType, PlayerCharacteristicUi> PlayerUiStats { get;} = new Dictionary<StatType, PlayerCharacteristicUi>();
        public Dictionary<BuffType, List<PlayerCharacteristicUi>> PlayerBuffs { get;}  = new Dictionary<BuffType, List<PlayerCharacteristicUi>>();
        
        private Text _attackText;

        public void DisableAttackBtnForTime(float delay)
        {
            StartCoroutine(DisableAttackBtnDelay(delay));
        }

        public void EnableAttackBtn(bool isEnabled)
        {
            var color = _attackText.color;
            if (isEnabled)
            {
                color.a = EnabledTextAlpha;
                AttackBtn.interactable = true;
            }
            else
            {
                color.a = DisabledTextAlpha;
                AttackBtn.interactable = false;
            }
            
            _attackText.color = color;
        }

        private void OnEnable()
        {
            AttackBtn = GetComponentInChildren<Button>();
            StatsRootTransform = transform.GetChild(StatsRootIndex);
            RectTransform = GetComponent<RectTransform>();
            _attackText = GetComponentInChildren<Text>();
        }

        private IEnumerator DisableAttackBtnDelay(float delay)
        {
            EnableAttackBtn(false);
            yield return new WaitForSeconds(delay);
            EnableAttackBtn(true);
        }
    }
}