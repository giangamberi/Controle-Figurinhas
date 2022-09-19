namespace Controle_Figurinhas
{
    public partial class controleFigurinhas : Form
    {

        public Colecao colecao;
        public int albumSelecionado = 0;

        public controleFigurinhas(Colecao colecao)
        {
            this.colecao = colecao;
            int auxLocStart = 5;
            int i, j;

            Label qtdAlbumsLabel = new Label();
            qtdAlbumsLabel.Text = "Albums: " + colecao.QTDAlbums;
            qtdAlbumsLabel.AutoSize = true;
            qtdAlbumsLabel.BackColor = Color.PaleVioletRed;
            qtdAlbumsLabel.Location = new Point(auxLocStart, 5);
            this.Controls.Add(qtdAlbumsLabel);
            auxLocStart += qtdAlbumsLabel.Width + 5;


            Label qtdFigurinhasTotalLabel = new Label();
            qtdFigurinhasTotalLabel.Text = "Qtd de figurinhas: " + colecao.QTDFigurinhas;
            qtdFigurinhasTotalLabel.Name = "qtdFigurinhasTotalLabel";
            qtdFigurinhasTotalLabel.AutoSize = true;
            qtdFigurinhasTotalLabel.BackColor = Color.PaleVioletRed;
            qtdFigurinhasTotalLabel.Location = new Point(auxLocStart, 5);
            this.Controls.Add(qtdFigurinhasTotalLabel);
            auxLocStart += qtdFigurinhasTotalLabel.Width + 5;

            Label qtdFigurinhasRepetidasLabel = new Label();
            qtdFigurinhasRepetidasLabel.Text = "Figurinhas Repetidas: " + colecao.QTDFigurinhasRepetidas;
            qtdFigurinhasRepetidasLabel.Name = "qtdFigurinhasRepetidasLabel";
            qtdFigurinhasRepetidasLabel.AutoSize = true;
            qtdFigurinhasRepetidasLabel.BackColor = Color.PaleVioletRed;
            qtdFigurinhasRepetidasLabel.Location = new Point(auxLocStart, 5);
            this.Controls.Add(qtdFigurinhasRepetidasLabel);
            auxLocStart += qtdFigurinhasRepetidasLabel.Width + 5;

            for (i = 0; i < colecao.QTDAlbums; i++)
            {
                Label qtdFigurinhasLabel = new Label();
                qtdFigurinhasLabel.Text = $"Figurinhas no Album {i + 1}: {colecao.QTDFigurinhasAlbum[i]}";
                qtdFigurinhasLabel.Name = $"qtdFigurinhasLabel{i}";
                qtdFigurinhasLabel.AutoSize = true;
                qtdFigurinhasLabel.BackColor = Color.PaleVioletRed;
                qtdFigurinhasLabel.Location = new Point(auxLocStart, 5);
                this.Controls.Add(qtdFigurinhasLabel);

                Label qtdFigurinhasFaltandoLabel = new Label();
                qtdFigurinhasFaltandoLabel.Text = $"Figurinhas Faltantes no Album {i + 1}: {colecao.QTDFigurinhasFaltantesAlbum[i]}";
                qtdFigurinhasFaltandoLabel.Name = $"qtdFigurinhasFaltandoLabel{i}";
                qtdFigurinhasFaltandoLabel.AutoSize = true;
                qtdFigurinhasFaltandoLabel.BackColor = Color.PaleVioletRed;
                qtdFigurinhasFaltandoLabel.Location = new Point(auxLocStart, qtdFigurinhasLabel.Location.Y + qtdFigurinhasLabel.Height + 3);
                this.Controls.Add(qtdFigurinhasFaltandoLabel);
                auxLocStart += qtdFigurinhasFaltandoLabel.Width + 5;
            }

            Button[] botaoAlbum = new Button[colecao.QTDAlbums];

            for (i = 0; i < colecao.QTDAlbums; i++)
            {
                botaoAlbum[i] = new Button();
                botaoAlbum[i].Text = $"Album {i + 1}";
                botaoAlbum[i].Name = $"{i}";
                botaoAlbum[i].AutoSize = true;
                botaoAlbum[i].BackColor = Color.Gray;
                botaoAlbum[i].Location = new Point(auxLocStart, 5);
                this.Controls.Add(botaoAlbum[i]);
                botaoAlbum[i].Click += new EventHandler(botaoAlbum_Click);
                auxLocStart += botaoAlbum[i].Width + 5;
            }

            Button botaoRepetidas = new Button();
            botaoRepetidas.Text = $"Repetidas";
            botaoRepetidas.AutoSize = true;
            botaoRepetidas.BackColor = Color.Gray;
            botaoRepetidas.Location = new Point(auxLocStart, 5);
            this.Controls.Add(botaoRepetidas);
            botaoRepetidas.Click += new EventHandler(botaoRepetidas_Click);
            auxLocStart += botaoRepetidas.Width + 5;

            TextBox inserirFigurinha = new TextBox();
            inserirFigurinha.Name = "inserirFigurinha";
            inserirFigurinha.Width = 200;
            inserirFigurinha.BorderStyle = BorderStyle.Fixed3D;
            inserirFigurinha.Location = new Point(auxLocStart, 5);
            this.Controls.Add(inserirFigurinha);
            auxLocStart += inserirFigurinha.Width;

            Button botaoInserir = new Button();
            botaoInserir.Text = $"Inserir";
            botaoInserir.AutoSize = true;
            botaoInserir.BackColor = Color.Gray;
            botaoInserir.Location = new Point(auxLocStart, 5);
            this.Controls.Add(botaoInserir);
            botaoInserir.Click += new EventHandler(botaoInserirFigurinha);
            auxLocStart += botaoInserir.Width + 5;

            Button botaoRemover = new Button();
            botaoRemover.Text = $"Remover";
            botaoRemover.AutoSize = true;
            botaoRemover.BackColor = Color.Gray;
            botaoRemover.Location = new Point(auxLocStart, 5);
            this.Controls.Add(botaoRemover);
            //botaoRemover.Click += new EventHandler(botaoRemoverFigurinha);
            auxLocStart += botaoRemover.Width + 5;

            Button botaoGerar = new Button();
            botaoGerar.Text = $"Gerar Lista";
            botaoGerar.AutoSize = true;
            botaoGerar.BackColor = Color.Gray;
            botaoGerar.Location = new Point(auxLocStart, 5);
            this.Controls.Add(botaoGerar);
            botaoGerar.Click += new EventHandler(botaoGerarLista);
            auxLocStart += botaoGerar.Width + 5;

            TableLayoutPanel figurinhas = new TableLayoutPanel();
            figurinhas.Controls.Clear();
            figurinhas.ColumnStyles.Clear();
            figurinhas.RowStyles.Clear();
            figurinhas.Name = "figurinhas";
            figurinhas.Location = new Point(45, 40);
            figurinhas.RowCount = colecao.maxRow();
            figurinhas.ColumnCount = colecao.maxColumn();
            figurinhas.AutoSize = true;
            figurinhas.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            i = 0;
            foreach (string time in colecao.repetidas.Keys)
            {
                for (j = 0; j < colecao.repetidas[time].Length;)
                {
                    Label figurinha = new Label();
                    figurinha.Text = time + (time == "FWC" ? j : j + 1) + ' ' + colecao.repetidas[time][j];
                    figurinha.BackColor = colecao.corTime(time);
                    figurinha.Width = 60;
                    figurinha.Height = 17;
                    figurinha.Name = $"{i} {j}";
                    //figurinha.BorderStyle = BorderStyle.FixedSingle;
                    figurinhas.Controls.Add(figurinha, i, j++);
                }
                i++;
            }
            this.Controls.Add(figurinhas);

            this.FormClosing += Form1_Closing;

            InitializeComponent();
        }
        public void botaoRepetidas_Click(object sender, System.EventArgs e)
        {
            albumSelecionado = colecao.QTDAlbums;
            int i = 0;
            int j;
            foreach (string time in colecao.repetidas.Keys)
            {
                for (j = 0; j < colecao.repetidas[time].Length;j++)
                {
                    var figurinha = this.Controls["figurinhas"].Controls[$"{i} {j}"] as Label;
                    if (figurinha == null) continue;
                    figurinha.Text = time + (time == "FWC" ? j : j + 1) + ' ' + this.colecao.repetidas[time][j];
                    figurinha.BackColor = colecao.corTime(time);
                }
                i++;
            }
        }

        public void botaoAlbum_Click(object sender, System.EventArgs e)
        {
            var button = sender as Button;
            albumSelecionado = int.Parse(button.Name);
            int i = 0;
            int j;
            foreach (string time in colecao.repetidas.Keys)
            {
                for (j = 0; j < colecao.repetidas[time].Length; j++)
                {

                    var figurinha = this.Controls["figurinhas"].Controls[$"{i} {j}"] as Label;
                    if (figurinha == null) continue;
                    figurinha.Text = time + (time == "FWC" ? j : j + 1);
                    figurinha.BackColor = (this.colecao.Albums[int.Parse(button.Name)][time][j] ? Color.Green : Color.Red);
                }
                i++;
            }
        }

        public void botaoInserirFigurinha(object sender, System.EventArgs e)
        {
            string texto = this.Controls["inserirFigurinha"].Text;
            int i;
            string[] textoAux = texto.Split(" ");
            this.Controls["inserirFigurinha"].Text = "";

            try
            {
                colecao.addFigurinha(textoAux[0], int.Parse(textoAux[1]));
                this.Controls["qtdFigurinhasTotalLabel"].Text = "Qtd de figurinhas: " + colecao.QTDFigurinhas;
                this.Controls["qtdFigurinhasRepetidasLabel"].Text = "Figurinhas Repetidas: " + colecao.QTDFigurinhasRepetidas;
                for (i = 0; i < colecao.QTDAlbums; i++)
                {
                    this.Controls[$"qtdFigurinhasLabel{i}"].Text = $"Figurinhas no Album {i + 1}: {colecao.QTDFigurinhasAlbum[i]}";
                    this.Controls[$"qtdFigurinhasFaltandoLabel{i}"].Text = $"Figurinhas Faltantes no Album {i + 1}: {colecao.QTDFigurinhasFaltantesAlbum[i]}";
                }
                
                return;
            }
            catch
            {
                return;
            }
        }

        public void botaoGerarLista(object sender, System.EventArgs e)
        {
            MessageBox.Show(colecao.ToString(albumSelecionado));
        }

        public void Form1_Closing(object sender, System.EventArgs e)
        {
            colecao.save();
        }

    }
}