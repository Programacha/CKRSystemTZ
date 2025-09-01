using _Scripts.MVP.Clicker;
using _Scripts.MVP.DogBreed;
using _Scripts.MVP.Weather;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.MVP
{
    public class TabController : MonoBehaviour
    {
        [SerializeField]private Button _clickerButton;
        [SerializeField]private Button _weatherButton;
        [SerializeField]private Button _dogFactsButton;
        [SerializeField]private GameObject _clickerTab;
        [SerializeField]private GameObject _weatherTab;
        [SerializeField]private GameObject _dogFactsTab;
    
        private DogFactsPresenter _dogFactsPresenter;
        private ClickerPresenter _clickerPresenter;
        private WeatherPresenter _weatherPresenter;

        [Inject]
        public void Construct(DogFactsPresenter dogFactsPresenter,ClickerPresenter clickerPresenter,WeatherPresenter weatherPresenter)
        {
            _dogFactsPresenter = dogFactsPresenter;
            _clickerPresenter = clickerPresenter;
            _weatherPresenter = weatherPresenter;
        }
    
        public void Start()
        {
            _clickerButton.onClick.AddListener(() => SwitchTab(_clickerTab));
            _weatherButton.onClick.AddListener(() => SwitchTab(_weatherTab));
            _dogFactsButton.onClick.AddListener(() => SwitchTab(_dogFactsTab));

            SwitchTab(_clickerTab);
        }
    
        private void SwitchTab(GameObject activeTab)
        {
            DeactivateTab(_dogFactsPresenter, _dogFactsTab);
            DeactivateTab(_weatherPresenter, _weatherTab);
            DeactivateTab(_clickerPresenter, _clickerTab);
        
            if (activeTab == _dogFactsTab)
            {
                ActivateTab(_dogFactsPresenter, _dogFactsTab);
            }
            if (activeTab == _weatherTab)
            {
                ActivateTab(_weatherPresenter, _weatherTab);
            }
            if (activeTab == _clickerTab)
            {
                ActivateTab(_clickerPresenter, _clickerTab);
            }
        }

        private void ActivateTab<T>(T presenter, GameObject tab) where T : class
        {
            tab.SetActive(true);

            if (presenter is ITabPresenter tabPresenter)
            {
                tabPresenter.OnTabActivated();
            }
        }

        private void DeactivateTab<T>(T presenter, GameObject tab) where T : class
        {
            if (tab.activeSelf)
            {
                tab.SetActive(false);
            
                if (presenter is ITabPresenter tabPresenter)
                {
                    tabPresenter.OnTabDeactivated();
                }
            }
        }
    }
}