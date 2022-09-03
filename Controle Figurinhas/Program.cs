namespace Controle_Figurinhas
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            int i, j;
            int auxLocStart = 5;
            Album album = new Album();
            album.startUp();
            album.addFigurinha("FWC", 7);
            album.addFigurinha("FWC", 7);
            album.addFigurinha("FWC", 19);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            controleFigurinhas form = new controleFigurinhas();
            form.Width = 1700;
            form.Height = 650;

            Label qtdAlbumsLabel = new Label();
            qtdAlbumsLabel.Text = "Albums: " + album.QTDAlbums;
            qtdAlbumsLabel.AutoSize = true;
            qtdAlbumsLabel.BackColor = Color.PaleVioletRed;
            qtdAlbumsLabel.Location = new Point(auxLocStart, 5);
            form.Controls.Add(qtdAlbumsLabel);
            auxLocStart += qtdAlbumsLabel.Width + 5;


            Label qtdFigurinhasTotalLabel = new Label();
            qtdFigurinhasTotalLabel.Text = "Qtd de figurinhas: " + album.QTDFigurinhas;
            qtdFigurinhasTotalLabel.AutoSize = true;
            qtdFigurinhasTotalLabel.BackColor = Color.PaleVioletRed;
            qtdFigurinhasTotalLabel.Location = new Point(auxLocStart, 5);
            form.Controls.Add(qtdFigurinhasTotalLabel);
            auxLocStart += qtdFigurinhasTotalLabel.Width + 5;

            Label qtdFigurinhasRepetidasLabel = new Label();
            qtdFigurinhasRepetidasLabel.Text = "Figurinhas Repetidas: " + album.QTDFigurinhasRepetidas;
            qtdFigurinhasRepetidasLabel.AutoSize = true;
            qtdFigurinhasRepetidasLabel.BackColor = Color.PaleVioletRed;
            qtdFigurinhasRepetidasLabel.Location = new Point(auxLocStart, 5);
            form.Controls.Add(qtdFigurinhasRepetidasLabel);
            auxLocStart += qtdFigurinhasRepetidasLabel.Width + 5;
            
            for (i = 0; i < album.QTDAlbums; i++)
            {
                Label qtdFigurinhasLabel = new Label();
                qtdFigurinhasLabel.Text = $"Figurinhas no Album {i+1}: {album.QTDFigurinhasAlbum[i]}";
                qtdFigurinhasLabel.AutoSize = true;
                qtdFigurinhasLabel.BackColor = Color.PaleVioletRed;
                qtdFigurinhasLabel.Location = new Point(auxLocStart, 5);
                form.Controls.Add(qtdFigurinhasLabel);

                Label qtdFigurinhasFaltandoLabel = new Label();
                qtdFigurinhasFaltandoLabel.Text = $"Figurinhas Faltantes no Album {i+1}: {album.QTDFigurinhasFaltantesAlbum[i]}";
                qtdFigurinhasFaltandoLabel.AutoSize = true;
                qtdFigurinhasFaltandoLabel.BackColor = Color.PaleVioletRed;
                qtdFigurinhasFaltandoLabel.Location = new Point(auxLocStart, qtdFigurinhasLabel.Location.Y + qtdFigurinhasLabel.Height + 3);
                form.Controls.Add(qtdFigurinhasFaltandoLabel);
                auxLocStart += qtdFigurinhasFaltandoLabel.Width + 5;
            }

            TableLayoutPanel figurinhas = new TableLayoutPanel();
            figurinhas.Controls.Clear();
            figurinhas.ColumnStyles.Clear();
            figurinhas.RowStyles.Clear();
            figurinhas.Name = "figurinhas";
            figurinhas.Location = new Point(45,40);
            figurinhas.RowCount = album.maxRow();
            figurinhas.ColumnCount = album.maxColumn();
            figurinhas.AutoSize = true;
            figurinhas.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            i = 0;
            foreach (string time in album.album.Keys)
            {
                for (j = 0; j < album.album[time].Length;)
                {
                    Label figurinha = new Label();
                    figurinha.Text = time + ' ' + album.album[time][j];
                    figurinha.BackColor = album.corTime(time);
                    figurinha.AutoSize = true;
                    //figurinha.BorderStyle = BorderStyle.FixedSingle;
                    figurinhas.Controls.Add(figurinha,i,j++);
                }
                i++;
            }

            form.Controls.Add(figurinhas);
            
            Application.Run(form);
        }
    }

    public class Album
    {
        public Dictionary<string, short[]> album = new Dictionary<string, short[]>();
        public short QTDAlbums;
        public short QTDFigurinhas = 0;
        public short QTDFigurinhasRepetidas = 0;
        public short[] QTDFigurinhasAlbum;
        public short[] QTDFigurinhasFaltantesAlbum;

        public void startUp()
        {
            string[] arquivo = System.IO.File.ReadAllLines(@"C:\Users\giang\OneDrive\RandomProjects\Controle Figurinhas\Controle Figurinhas\dataDic.txt");
            string[] aux;
            short[] aux1;
            short auxFig;
            int i,j,k;
            aux = arquivo[0].Split(' ');
            if (aux[0] == "albums")
            {
                QTDAlbums = short.Parse(aux[1]);
            }

            QTDFigurinhasAlbum = new short[QTDAlbums];
            QTDFigurinhasFaltantesAlbum = new short[QTDAlbums];

            for (i = 1; i < arquivo.Length; i++)
            {
                aux = arquivo[i].Split(' ');
                aux1 = new short[aux.Length-1];
                for (j = 1; j < aux.Length;j++)
                {
                    auxFig = short.Parse(aux[j]);
                    aux1[j-1] = auxFig;
                    QTDFigurinhas += auxFig;
                    for (k = 0; k<QTDAlbums; k++)
                    {
                        if (auxFig > 0)
                        {
                            QTDFigurinhasAlbum[k] += 1;
                            auxFig--;
                        }
                        else
                        {
                            QTDFigurinhasFaltantesAlbum[k] += 1;
                        }
                    }
                    QTDFigurinhasRepetidas += auxFig;
                }
                album.Add(aux[0],aux1);    
            }
            try
            {
            }
            catch
            {
                Console.WriteLine("Erro ao abrir/ler o arquivo");
            }
        }

        public void addFigurinha(string time, int numero)
        {
            if (album.Keys.Contains(time))
            {
                if (numero-1 < album[time].Length)
                {
                    QTDFigurinhas += 1;
                    //if (album[time][numero - 1] > )
                    album[time][numero-1] += 1;
                    
                }
            }
        }

        public int maxRow()
        {
            return album["FWC"].Length;
        }

        public int maxColumn()
        {
            return album.Keys.Count;
        }

        public Color corTime(string Time)
        {
            switch (Time)
            {
                case "FWC":
                    return Color.Gold;
                case "QAT":
                    return Color.DarkRed;
                case "ECU":
                    return Color.Yellow;
                case "SEN":
                    return Color.LightGreen;
                case "NED":
                    return Color.Orange;
                case "ENG":
                    return Color.White;
                case "IRN":
                    return Color.DarkOliveGreen;
                case "USA":
                    return Color.LightGray;
                case "WAL":
                    return Color.ForestGreen;
                case "ARG":
                    return Color.LightSkyBlue;
                case "KSA":
                    return Color.LawnGreen;
                case "MEX":
                    return Color.DarkGreen;
                case "POL":
                    return Color.Red;
                case "FRA":
                    return Color.CadetBlue;
                case "AUS":
                    return Color.LightGoldenrodYellow;
                case "DEN":
                    return Color.IndianRed;
                case "TUN":
                    return Color.OrangeRed;
                case "ESP":
                    return Color.OrangeRed;
                case "CRC":
                    return Color.Red;
                case "GER":
                    return Color.DarkGray;
                case "JPN":
                    return Color.AliceBlue;
                case "BEL":
                    return Color.Red;
                case "CAN":
                    return Color.Red;
                case "MAR":
                    return Color.Red;
                case "CRO":
                    return Color.Red;
                case "BRA":
                    return Color.Green;
                case "SRB":
                    return Color.Red;
                case "SUI":
                    return Color.Red;
                case "CMR":
                    return Color.DarkSeaGreen;
                case "POR":
                    return Color.DarkGreen;
                case "GHA":
                    return Color.DarkSlateGray;
                case "URU":
                    return Color.LightSteelBlue;
                case "KOR":
                    return Color.DodgerBlue;
                case "C":
                    return Color.Red;
                default:
                    return Color.Gold;
            }
        }

        public override string ToString()
        {
            string aux = "Albums: " + QTDAlbums + '\n';
            foreach (string chave in album.Keys)
            {
                aux += (chave + ':' + album[chave] + '\n');
            }

            return aux;
        }



    }
}