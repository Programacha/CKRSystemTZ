using System.Collections.Generic;
using UniRx;

namespace _Scripts.MVP.DogBreed
{
    public class DogFactsModel
    {
        public ReactiveProperty<List<DogBreedParsing.BreedData>> BreedsList { get; private set; } = new (new List<DogBreedParsing.BreedData>());
        public ReactiveProperty<DogBreedParsing.BreedFactsResponse> CurrentBreedFacts { get; private set; } = new (null);
        public ReactiveProperty<bool> IsLoading { get; private set; } = new (false);
    }
}