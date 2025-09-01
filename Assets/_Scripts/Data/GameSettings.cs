using UnityEngine;

namespace _Scripts
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public int TapCurrencyAmount => _tapCurrencyAmount;
        public int TapEnergyCost => _tapEnergyCost;
        public float AutoCollectInterval => _autoCollectInterval;
        public float EnergyRegenInterval => _energyRegenInterval;
        public int EnergyRegenAmount => _energyRegenAmount;
        public int MaxEnergy => _maxEnergy;

        [SerializeField] private int _tapCurrencyAmount = 1;
        [SerializeField] private int _tapEnergyCost = 1;
        [SerializeField] private float _autoCollectInterval = 3f;
        [SerializeField] private float _energyRegenInterval = 10f;
        [SerializeField] private int _energyRegenAmount = 10;
        [SerializeField] private int _maxEnergy = 1000;
    }
}