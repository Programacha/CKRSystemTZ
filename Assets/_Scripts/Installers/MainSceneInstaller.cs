using _Scripts.MVP.Clicker;
using _Scripts.MVP.DogBreed;
using _Scripts.MVP.Weather;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameSettings _gameSettings;
        [SerializeField] private ClickerView _clickerView;
        [SerializeField] private DogFactsView _dogFactsView;
        [SerializeField] private PopupView _popupView;
        public override void InstallBindings()
        {
            Container
                .Bind<GameSettings>()
                .FromInstance(_gameSettings)
                .AsSingle();
            
            Container
                .Bind<NetworkRequestQueue>()
                .AsSingle();
            
            Container
                .Bind<ClickerModel>()
                .AsSingle();
            
            Container
                .Bind<ClickerView>()
                .FromInstance(_clickerView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ClickerPresenter>()
                .AsSingle();
            
            Container
                .Bind<WeatherApiService>()
                .AsSingle();
            
            Container
                .Bind<WeatherModel>()
                .AsSingle();
            
            Container
                .Bind<WeatherView>()
                .FromComponentInHierarchy()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<WeatherPresenter>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<DogApiService>()
                .AsSingle();
            
            Container
                .Bind<DogFactsModel>()
                .AsSingle();
            
            Container
                .Bind<DogFactsView>()
                .FromComponentInHierarchy(_dogFactsView)
                .AsSingle();
            
            Container
                .Bind<PopupView>()
                .FromComponentInHierarchy(_popupView)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<DogFactsPresenter>()
                .AsSingle()
                .NonLazy();

        }
    }
}