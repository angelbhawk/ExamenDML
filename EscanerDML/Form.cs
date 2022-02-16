using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EscanerDML
{
    public partial class FrmPrincipal : Form
    {
        List<Token> Tokens;
        List<Token>Dtokens;
        Clasificador ListaDI = new Clasificador();
        Clasificador ListaDC = new Clasificador();
        Clasificador Lista = new Clasificador();
        List<TokensDinamicos> TdinamicosIde;
        List<TokensDinamicos> Tconstantes;
        public FrmPrincipal()
        {
            InitializeComponent();
            //fcbxText.Language = FastColoredTextBoxNS.Language.SQL;
        }

        private void TsbEjecutar_Click(object sender, EventArgs e)
        {
            if (dgv.Visible == true)
            {
                dgv.Visible = false;
            }
            else
            {
                Tokens = Lista.getList(Tokenizador.getTokens(fcbxText.Text));
                
                if(buscarErrores())
                {
                    Errores();
                }
                else
                {
                    Resultados();
                }
                dgv.Visible = true;
            }
        }

        private void Resultados()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("clmNum", "Numero");
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("clmCad", "Cadena");
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("clmTip", "Tipo");
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("clmPLin", "Linea");
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("clmPos", "Posicion");
            dgv.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("clmCod", "Código");
            dgv.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Color c = Color.FromArgb(32,32,32);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.BackColor = c;
            dgv.DefaultCellStyle.SelectionBackColor =c;

            int i = 0;
            foreach (Token t in Tokens)
            {
                i++;
                this.dgv.Rows.Add(i, t.Cadena, t.Tipo, t.Linea, t.Posicion,t.Valor);
            }
        }
        private void ResultadosDinamicos()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("DclmNum", "Numero");
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("DclmCad", "Cadena");
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("DclmTip", "Tipo");
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("DclmCod", "Codigo");
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Color c = Color.FromArgb(32, 32, 32);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.BackColor = c;
            dgv.DefaultCellStyle.SelectionBackColor = c;

            int i = 0;
            TdinamicosIde = ListaDI.Dinamicos;
            foreach (TokensDinamicos t in TdinamicosIde)
            {
                i++;
                this.dgv.Rows.Add(i, t.Cadena, t.Tipo,t.Valor);
            }
        }
        private void ResultadosDinamicosConstantes()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            Tconstantes=new List<TokensDinamicos>();
            Tconstantes.Clear();

            dgv.Columns.Add("DclmNum", "Numero");
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("DclmCad", "Cadena");
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("DclmTip", "Tipo");
            dgv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("DclmCod", "Codigo");
            dgv.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Color c = Color.FromArgb(32, 32, 32);
            dgv.DefaultCellStyle.ForeColor = Color.White;
            dgv.DefaultCellStyle.BackColor = c;
            dgv.DefaultCellStyle.SelectionBackColor = c;

            int i = 0;
            Tconstantes = ListaDC.Dconstantes;
            foreach (TokensDinamicos t in Tconstantes)
            {
                i++;
                this.dgv.Rows.Add(i, t.Cadena, t.Tipo, t.Valor);
            }
        }

        private void Errores()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            dgv.Columns.Add("clmErr", "Error");
            dgv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgv.Columns.Add("clmDes", "Descripcion");
            dgv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (Token t in Tokens)
            {
                if(t.Tipo == "Simbolo desconocido")
                {
                    this.dgv.Rows.Add("1:101 Error en linea " + t.Linea + " posicion "+ t.Posicion, t.Tipo,t.Valor);
                }
                if (t.Tipo == "Elemento invalido")
                {
                    this.dgv.Rows.Add("1:102 Error en linea " + t.Linea + " posicion " + t.Posicion, t.Tipo,t.Valor);
                }
            }
        }

        private bool buscarErrores()
        {
            bool error = false;
            foreach (Token t in Tokens)
            {
                if (t.Tipo == "Simbolo desconocido")
                {
                    error = true;
                    return error;
                }
                if(t.Tipo == "Elemento invalido")
                {
                    error = true;
                    return error;
                }
            }
            return error;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (dgv.Visible == true)
            {
                dgv.Visible = false;
            }
            else
            {
                Tokens = Lista.getList(Tokenizador.getTokens(fcbxText.Text));
                Dtokens = ListaDI.getList(Tokenizador.getTokens(fcbxText.Text));

                if (buscarErrores())
                {
                    Errores();
                }
                else
                {
                    Resultados();
                    ResultadosDinamicos();
                }
                dgv.Visible = true;
            }
        }

        private void ConstantT_Click(object sender, EventArgs e)
        {
            if (dgv.Visible == true)
            {
                dgv.Visible = false;
            }
            else
            {
                //Dtokens.Clear();
                Tokens = Lista.getList(Tokenizador.getTokens(fcbxText.Text));
                Dtokens = ListaDC.getList(Tokenizador.getTokens(fcbxText.Text));

                if (buscarErrores())
                {
                    Errores();
                }
                else
                {
                    Resultados();
                    ResultadosDinamicosConstantes();
                }
                dgv.Visible = true;
            }
        }
    }
}