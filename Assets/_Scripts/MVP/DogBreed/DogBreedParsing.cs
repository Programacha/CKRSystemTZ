using System;

namespace _Scripts.MVP.DogBreed
{
    public class DogBreedParsing
    {
        [Serializable]
        public class BreedsResponse
        {
            public BreedData[] data;
        }

        [Serializable]
        public class BreedData
        {
            public string id;
            public string type;
            public BreedAttributes attributes;
        }

        [Serializable]
        public class BreedAttributes
        {
            public string name;
            public string description;
        }
        
        [Serializable]
        public class BreedFactsResponse
        {
            public BreedData data;
        }
        
        [Serializable]
        public class BreedDescription
        {
            public string description;
        }
    }
}