using MyPokedex.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace MyPokedex.Tests.Data
{
    public static class FunTranslationsClientData
    {
        public const string jsonData_Valid_Shakespeare = @"{
                                         'success': {
                                                     'total': 1
                                                     },
                                         'contents': {
                                                     'translated': '\'t can freely recombine its own cellular structure totransform into other life-forms.',
                                                     'text': 'It can freely recombine its own cellular structure totransform into other life-forms.',
                                                     'translation': 'shakespeare'
                                                     }}";
        
        public const string jsonData_Invalid = @"{
                         'has_gender_differences': false,
                         'hatch_counter': 120,
                         'id': 150,
                         'is_baby': false}";
        
        public const string jsonData_MissingProperty = @"{
                                                'success': {
                                                            'total': 1
                                                            },
                                                'contents': {
                                                            'text': 'It can freely recombine its own cellular structure totransform into other life-forms.',
                                                            'translation': 'shakespeare'
                                                            }}";

        public const string jsonData_Valid_Yoda = @"{
                                         'success': {
                                                     'total': 1
                                                     },
                                         'contents': {
                                                     'translated': 'freely recombining on its own cellular structure, \nit can transform into other life-forms.',
                                                     'text': 'It can freely recombine its own cellular structure totransform into other life-forms.',
                                                     'translation': 'shakespeare'
                                                     }}";
    }
}
