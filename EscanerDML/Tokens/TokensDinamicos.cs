using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscanerDML
{
    class TokensDinamicos
    {
        int num;
        string cadena;
        int valor;
        int tipo;
        //string tipo;
        TokensDinamicos() { }
        //public TokensDinamicos(int n, string c, int v, string t)
        //{
        //    Num = n;
        //    Cadena = c;
        //    Valor = v;
        //    tipo = t;
        //}
        public TokensDinamicos(int n, string c, int v, int t)
        {
            Num = n;
            Cadena = c;
            Valor = v;
            Tipo = t;
        }

        public string Cadena { get => cadena; set => cadena = value; }
        public int Valor { get => valor; set => valor = value; }
        //public string Tipo { get => tipo; set => tipo = value; }
        public int Num { get => num; set => num = value; }
        public int Tipo { get => tipo; set => tipo = value; }
    }
}
