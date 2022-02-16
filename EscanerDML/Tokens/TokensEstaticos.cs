using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscanerDML
{
    class TokensEstaticos
    {
        string cadena;
        int valor;
        string simbolo;
        string tipo;
        TokensEstaticos() { }
        public TokensEstaticos(string c,int v, string t)
        {
            Cadena = c;
            Valor = v;
            tipo = t;
        }
        public TokensEstaticos(string c, int v,string s,string t)
        {
            Cadena = c;
            Valor = v;
            simbolo = s;
            tipo = t;
        }

        public string Cadena { get => cadena; set => cadena = value; }
        public int Valor { get => valor; set => valor = value; }
        public string Simbolo { get => simbolo; set => simbolo = value; }
        public string Tipo { get => tipo; set => tipo = value; }
    }
}
