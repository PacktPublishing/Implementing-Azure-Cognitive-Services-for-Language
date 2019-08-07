using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using TwitterAnalytics.Pocos;

namespace TwitterAnalytics.Services
{
    public class TranslatorTextService
    {
        const string _subscriptionKey = "b4d009edae8f42398f2ea893a9f4017d";
        const string _host =
            "https://api.cognitive.microsofttranslator.com";
       

        public DetectRepsonse Detect(string sourceText)
        {

        }

        public TranslationResponse Translate(string sourceText,
                                      string targetLanguage)
        {

        }


    }
}
