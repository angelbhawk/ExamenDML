using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscanerDML
{
    class Token
    {
        private string cadena, tipo;
        private int posicion, linea;
        private int valor;
        
        public Token(string c, string t, int p, int l,int v)
        {
            Cadena = c;
            Tipo = t;
            Posicion = p;
            Linea = l;
            valor = v;
        }

        public string Cadena { get => cadena; set => cadena = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public int Posicion { get => posicion; set => posicion = value; }
        public int Linea { get => linea; set => linea = value; }
        public int Valor { get => valor; set => valor = value; }
    }
}