using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscanerDML
{
    class Clasificador
    {
        TokensEstaticos[] Estaticos=new TokensEstaticos[48];
        public List<TokensDinamicos> Dinamicos = new List<TokensDinamicos>();
        public List<TokensDinamicos> Dconstantes = new List<TokensDinamicos>();
        public Clasificador() 
        {
            //reservadas 
            Estaticos[0] = new TokensEstaticos("SELECT", 10,"s", "reservadas");
            Estaticos[1] = new TokensEstaticos("FROM", 11,"f", "reservadas");
            Estaticos[2] = new TokensEstaticos("WHERE", 12, "w", "reservadas");
            Estaticos[3] = new TokensEstaticos("IN", 13, "n", "reservadas");
            Estaticos[4] = new TokensEstaticos("AND", 14, "y", "reservadas");
            Estaticos[5] = new TokensEstaticos("OR", 15, "o", "reservadas");
            Estaticos[6] = new TokensEstaticos("CREATE", 16, "c", "reservadas");
            Estaticos[7] = new TokensEstaticos("TABLE", 17, "t", "reservadas");
            Estaticos[8] = new TokensEstaticos("CHAR", 18, "h", "reservadas");
            Estaticos[9] = new TokensEstaticos("NUMERIC", 19, "u", "reservadas");
            Estaticos[10] = new TokensEstaticos("NOT", 20, "e", "reservadas");
            Estaticos[11] = new TokensEstaticos("NULL", 21, "g", "reservadas");
            Estaticos[12] = new TokensEstaticos("CONSTRAINTS", 22, "b", "reservadas");
            Estaticos[13] = new TokensEstaticos("KEY", 23, "k", "reservadas");
            Estaticos[14] = new TokensEstaticos("PRIMARY", 24, "p", "reservadas");
            Estaticos[15] = new TokensEstaticos("FOREIGN", 25, "j", "reservadas");
            Estaticos[16] = new TokensEstaticos("REFERENCES", 26, "l", "reservadas");
            Estaticos[17] = new TokensEstaticos("INSERT", 27, "m", "reservadas");
            Estaticos[18] = new TokensEstaticos("INTO", 28, "q", "reservadas");
            Estaticos[19] = new TokensEstaticos("VALUES", 29, "v", "reservadas");
            Estaticos[20] = new TokensEstaticos("GO", 30, "a", "reservadas");
            Estaticos[21] = new TokensEstaticos("PROCEDURE", 31, "b", "reservadas");
            Estaticos[22] = new TokensEstaticos("VARCHAR", 32, "d", "reservadas");
            Estaticos[23] = new TokensEstaticos("AS", 33, "z", "reservadas");
            Estaticos[24] = new TokensEstaticos("IF", 34, "x", "reservadas");
            Estaticos[25] = new TokensEstaticos("BEGIN", 35, "h", "reservadas");
            Estaticos[26] = new TokensEstaticos("PRINT", 36, "r", "reservadas");
            Estaticos[27] = new TokensEstaticos("END", 37, "f", "reservadas");
            Estaticos[28] = new TokensEstaticos("ELSE", 38, "s", "reservadas");
            Estaticos[29] = new TokensEstaticos("EXISTS", 39, "s", "reservadas");
            //delimitadores
            Estaticos[30] = new TokensEstaticos(",", 50, "delimitador");
            Estaticos[31] = new TokensEstaticos(".", 51, "delimitador");
            Estaticos[32] = new TokensEstaticos("(", 52, "delimitador");
            Estaticos[33] = new TokensEstaticos(")", 53, "delimitador");
            Estaticos[34] = new TokensEstaticos("'", 54, "delimitador");
            Estaticos[35] = new TokensEstaticos("’", 54, "delimitador");
            Estaticos[36] = new TokensEstaticos("‘", 54, "delimitador");
            //Constante
            Estaticos[37] = new TokensEstaticos("d", 61, "constantes");
            Estaticos[38] = new TokensEstaticos("a", 62, "constantes");
            //Operadores
            Estaticos[39] = new TokensEstaticos("+", 70, "operadores");
            Estaticos[40] = new TokensEstaticos("-", 71, "operadores");
            Estaticos[41] = new TokensEstaticos("*", 72, "operadores");
            Estaticos[42] = new TokensEstaticos("/", 73, "operadores");
            //Relacionales
            Estaticos[43] = new TokensEstaticos(">", 81, "relacionales");
            Estaticos[44] = new TokensEstaticos("<", 82, "relacionales");
            Estaticos[45] = new TokensEstaticos("=", 83, "relacionales");
            Estaticos[46] = new TokensEstaticos(">=", 84, "relacionales");
            Estaticos[47] = new TokensEstaticos("<=", 85, "relacionales");
        }

        public bool buscar(TokensEstaticos[] Esta, Match cad, ref int val)
        {
            int i = 0;
            Regex Comp;
            
            while (i < Esta.Length)
            {
                if (char.IsLetter(cad.ToString()[0]))
                {
                    Comp = new Regex(cad.Value, RegexOptions.IgnoreCase);
                    if (Comp.IsMatch(Esta[i].Cadena))
                    {
                        val = Esta[i].Valor;
                        return true;
                    }
                }
                else
                {
                    if (cad.Value == Esta[i].Cadena)
                    {
                        val = Esta[i].Valor;
                        return true;
                    }
                }
                i++;
            }
            return false;
        }
        public bool buscarDinamico(List<TokensDinamicos> EstaD, Match match, ref int val)
        {
            int i = 0;
            while (i < EstaD.Count)
            {
                if (match.Value == EstaD[i].Cadena)
                {
                    val = EstaD[i].Valor;
                    return true;
                }
                i++;
            }
            return false;
        }
        public bool buscarReservada(TokensEstaticos[] EstaR, Match match)
        {
            int i = 0;
            Regex Comp;
            while (i <=29)
            {
                if (char.IsLetter(match.ToString()[0]))
                {
                    Comp = new Regex(match.Value, RegexOptions.IgnoreCase);
                    if (Comp.IsMatch(EstaR[i].Cadena))
                    {
                        return true;
                    }
                }
                
                i++;
            }
            return false;
        }
        public List<Token> getList(MatchCollection matches)
        {
            int linea = 1, errores = 0;
            List<Token> Tokens = new List<Token>();
            Dinamicos.Clear();
            Dconstantes.Clear();
            int val = 0;
            //int nDinamico = 1;
            int vIdentificador = 401;
            int vConstante =600;
            int vDinamico = 0;
            foreach (Match match in matches)
            {
                if (match.Groups[1].Value != "")
                {
                    linea++;
                }
                if (match.Value != "\r" && match.Value != "\r\n" && match.Value != "\n" && match.Value != " " && match.Value != "")
                {
                    //buscador que da el valor al token estatico
                    buscar(Estaticos, match, ref val);

                    if (match.Groups[2].Value != "")
                    {
                        //MessageBox.Show(match.Value + " si es Alfanumerico");
                        if (buscarDinamico(Dconstantes, match, ref vDinamico) == false)
                        {
                            vDinamico = vConstante;
                            Dconstantes.Add(new TokensDinamicos(match.Index, match.Value, vDinamico, 6));
                            vConstante++;
                        }
                        Tokens.Add(new Token(match.Value, "Constante", match.Index, linea,vDinamico));
                    }
                    else if (buscarReservada(Estaticos,match)) // verificar si es una palabra reservada
                    {
                        //MessageBox.Show(match.Value + " si es reservada");
                        Tokens.Add(new Token(match.Value, "Palabra reservada", match.Index, linea, val));
                    }
                    else if (Regex.IsMatch(match.Value, @"\A[@A-Za-z#]+[A-Za-z#@_0-9]*")) // verificar si es un identificador
                    {
                        //MessageBox.Show(match.Value + " si es identificador");
                        if (buscarDinamico(Dinamicos, match, ref vDinamico) == false)
                        {
                            vDinamico = vIdentificador;
                            Dinamicos.Add(new TokensDinamicos(match.Index, match.Value, vDinamico, 4));
                            vIdentificador++;
                        }
                        Tokens.Add(new Token(match.Value, "Identificador", match.Index, linea,vDinamico));
                    }
                    else if (Regex.IsMatch(match.Value, @"\b\d{1,3}(\,?\d{3})?(\.?\d{1,3})?\b")) // verificar si es un constante
                    {
                        //MessageBox.Show(match.Value + " si es constante");
                        if(buscarDinamico(Dconstantes, match, ref vDinamico) ==false)
                        {
                            vDinamico = vConstante;
                            Dconstantes.Add(new TokensDinamicos(match.Index, match.Value, vDinamico,6));
                            vConstante++;
                        }
                        Tokens.Add(new Token(match.Value, "Constante", match.Index, linea,vDinamico));
                    }
                    else if (Regex.IsMatch(match.Value, @"[+/*<>=]{1,2}")) // verificar si es una operador
                    {
                        //MessageBox.Show(match.Value + " si es operador");
                        Tokens.Add(new Token(match.Value, "Operador", match.Index, linea,val));
                    }
                    else if (Regex.IsMatch(match.Value, @"([](),.’‘{}[])")) // verificar si es una delimitador ’‘’' " "
                    {
                        //MessageBox.Show(match.Value + " si es delimitador");
                        Tokens.Add(new Token(match.Value, "Delimitador", match.Index, linea,val));
                    }
                    else if (Regex.IsMatch(match.Value, @"\S?")) // verificar si es un simbolo desconocido
                    {
                        //MessageBox.Show(match.Value + " si es simbolo desconocido");
                        Tokens.Add(new Token(match.Value, "Simbolo desconocido", match.Index, linea,val));
                    }
                    else if (Regex.IsMatch(match.Value, @"\S*")) // verificar si es una elemento invalido
                    {
                        //MessageBox.Show(match.Value + " si es elemento invalido");
                        Tokens.Add(new Token(match.Value, "Elemento invalido", match.Index, linea,val));
                    }
                }          
            }
            return Tokens;
        }   
    }
}