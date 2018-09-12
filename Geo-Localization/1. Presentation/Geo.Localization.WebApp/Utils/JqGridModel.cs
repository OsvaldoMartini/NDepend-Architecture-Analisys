using System.Collections.Generic;

namespace Geo.Localization.WebApp.Utils
{
    public class JqGridModel<T>
    {
        public JqGridModel()
        {
            Sucesso = true;
        }

        public string page { get; set; }
        public string records { get; set; }
        public string total { get; set; }

        public string RequestRows { get; set; }
        public List<T> rows { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }
}