using MyPokedex.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MyPokedex.Tests.Data
{
    public static class PokeApiClientData
    {
        public const string jsonData = @"{
                        'flavor_text_entries': 
                            [{
                                'flavor_text': 'It was created by\na scientist after\nyears of horrific\fgene splicing and\nDNA engineering\nexperiments.',
                                'language': {
                                            'name': 'en',
                                            'url': 'https://pokeapi.co/api/v2/language/9/'
                                            },
                                'version': {
                                            'name': 'red',
                                            'url': 'https://pokeapi.co/api/v2/version/1/'
                                            }
                            }],
                        'is_legendary': true,
                        'name': 'mewtwo',
                        'habitat': {
                                    'name': 'rare',
                                    'url': 'https://pokeapi.co/api/v2/pokemon-habitat/5/'
                                   }}";

        public const string jsonData_Invalid = @"{
                                'has_gender_differences': false,
                                'hatch_counter': 120,
                                'id': 150,
                                'is_baby': false}";

        public const string jsonData_MissingDescription = @"{
                        'flavor_text_entries': [
                            {
                                'language': {
                                            'name': 'en',
                                            'url': 'https://pokeapi.co/api/v2/language/9/'
                                            },
                                'version': {
                                            'name': 'red',
                                            'url': 'https://pokeapi.co/api/v2/version/1/'
                                            }
                            }],
                        'is_legendary': true,
                        'name': 'mewtwo',
                        'habitat': {
                                    'name': 'rare',
                                    'url': 'https://pokeapi.co/api/v2/pokemon-habitat/5/'
                                   }}";

        public const string jsonData_MissingProperty = @"{
                        'is_legendary': true,
                        'name': 'mewtwo',
                        'habitat': {
                                    'name': 'rare',
                                    'url': 'https://pokeapi.co/api/v2/pokemon-habitat/5/'
                                   }}";

    }
}
