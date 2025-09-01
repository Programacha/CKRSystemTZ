using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MVP.Clicker
{
    public class ClickerView : MonoBehaviour
    {
        private const string MONEY = "Валюта :";
        private const string ENERGY = "Энергия :";
        
        [SerializeField]private Button _clickButton;
        [SerializeField]private TextMeshProUGUI _moneyCount;
        [SerializeField]private TextMeshProUGUI _energyCount;
        [SerializeField]private ParticleSystem _particles;
        [SerializeField]private RectTransform _moneyPopUp;
        [SerializeField]private AudioSource _moneySfxSource;
    
        private ClickerPresenter _presenter;

        public void Init(ClickerPresenter presenter)
        {
            _presenter = presenter;
        
            _clickButton.OnClickAsObservable()
                .Subscribe(_ => OnClickButton())
                .AddTo(this);
        }

        private void OnClickButton()
        {
            _presenter.OnTap();
        }
    
        public void UpdateCurrency(int value)
        {
            _moneyCount.text = MONEY + value; 
            _particles.Play();
            _moneySfxSource.Play();
            _moneyPopUp.gameObject.SetActive(true);
            _moneyPopUp.transform.DOLocalMoveY(1000, 0.2f).OnComplete(() =>
            {
                _moneyPopUp.gameObject.SetActive(false);
                _moneyPopUp.transform.localPosition = Vector3.zero;
            });
        }

        public void UpdateEnergy(int value)
        {
            _energyCount.text = ENERGY + value;
        }
    }
}