using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.MVP.Clicker
{
    public class ClickerView : MonoBehaviour
    {
        [SerializeField]private Button _clickButton;
        [SerializeField]private TextMeshProUGUI _moneyCount;
        [SerializeField]private TextMeshProUGUI _energyCount;
        [SerializeField]private ParticleSystem _particles;
        [SerializeField]private RectTransform _currencyImage;
    
        private ClickerPresenter _presenter;

        public void Init(ClickerPresenter presenter)
        {
            this._presenter = presenter;
        
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
            _moneyCount.text = "Валюта: " + value;
          //  _particles.Play();
            _currencyImage.transform.DOLocalMoveY(200f, 0.5f).OnComplete(() =>
            {
                _currencyImage.transform.localPosition = Vector3.zero;
            });
        }

        public void UpdateEnergy(int value)
        {
            _energyCount.text = "Энергия: " + value;
        }
    }
}