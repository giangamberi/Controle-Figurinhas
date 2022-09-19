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
            Colecao colecao = new Colecao();
            //colecao.addFigurinha("FWC", 7);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            controleFigurinhas form = new controleFigurinhas(colecao);
            form.Width = 2500;
            form.Height = 700;
            Application.Run(form);
        }

    }



    public class Colecao
    {
        public Dictionary<string, short[]> repetidas = new Dictionary<string, short[]>();
        public Dictionary<string, bool[]>[] Albums; 
        public short QTDAlbums;
        public short QTDFigurinhas = 0;
        public short QTDFigurinhasRepetidas = 0;
        public short[] QTDFigurinhasAlbum;
        public short[] QTDFigurinhasFaltantesAlbum;

        public Colecao ()
        {
            string[] arquivo = System.IO.File.ReadAllLines($@"{Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName}\dataDic.txt");
            string[] linha;
            short[] figurinhasR;
            bool[][] figurinhasA;
            short auxFig;
            int i,j,k;

            linha = arquivo[0].Split(' ');
            if (linha[0] == "albums")
            {
                QTDAlbums = short.Parse(linha[1]);
            }

            Albums  = new Dictionary<string, bool[]>[QTDAlbums];
            for (i = 0; i < QTDAlbums; i++)
                Albums[i] = new Dictionary<string, bool[]>();
            QTDFigurinhasAlbum = new short[QTDAlbums];
            QTDFigurinhasFaltantesAlbum = new short[QTDAlbums];
            figurinhasA = new bool[QTDAlbums][];

            for (i = 1; i < arquivo.Length; i++)
            {
                linha = arquivo[i].Split(' ');
                figurinhasR = new short[linha.Length-1];
                for (k = 0; k < QTDAlbums; k++)
                    figurinhasA[k] = new bool[linha.Length - 1];

                for (j = 0; j < figurinhasR.Length;j++)
                {
                    auxFig = short.Parse(linha[j+1]);
                    QTDFigurinhas += auxFig;
                    for (k = 0; k < QTDAlbums; k++)
                    {
                        if (auxFig > 0)
                        {
                            figurinhasA[k][j] = true;
                            QTDFigurinhasAlbum[k]++;
                            auxFig--;
                        }
                        else
                        {
                            figurinhasA[k][j] = false;
                            QTDFigurinhasFaltantesAlbum[k]++;
                        }
                    }
                    figurinhasR[j] = auxFig;

                    QTDFigurinhasRepetidas += auxFig;
                }
                for (k = 0; k < QTDAlbums; k++)
                    Albums[k].Add(linha[0], figurinhasA[k]);
                repetidas.Add(linha[0],figurinhasR);
            }
        }

        public void save()
        {
            string[] file = new string[35];
            int i,j,k,aux;
            file[0] = $"albums {QTDAlbums}";
            i = 0;
            foreach (string time in repetidas.Keys)
            {
                file[++i] = time;
                for (j = 0; j < repetidas[time].Length;j++)
                {
                    aux = 0;
                    for (k = 0; k < QTDAlbums; k++)
                        aux += Albums[k][time][j] ? 1 : 0 ;
                    file[i] += $" {repetidas[time][j] + aux}";
                }
            }
            File.WriteAllLines(@"C:\Users\giang\OneDrive\RandomProjects\Controle Figurinhas\Controle Figurinhas\dataDic.txt",file);
        }

        public void addFigurinha(string time, int numero)
        {
            int i;
            if (repetidas.Keys.Contains(time))
            {
                numero -= (time == "FWC" ? 0 : 1);
                if (numero < repetidas[time].Length)
                {
                    QTDFigurinhas++;
                    for (i = 0; i <= QTDAlbums; i++)
                    {
                        if (i == QTDAlbums)
                        {
                            repetidas[time][numero]++;
                            QTDFigurinhasRepetidas++;
                        }
                        else if (!Albums[i][time][numero])
                        {
                            Albums[i][time][numero] = true;
                            QTDFigurinhasAlbum[i]++;
                            QTDFigurinhasFaltantesAlbum[i]--;
                            break;
                        }
                    }
                }
            }
        }

        public int maxRow()
        {
            return Albums[0]["FWC"].Length;
        }

        public int maxColumn()
        {
            return Albums[0].Keys.Count;
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
            foreach (string chave in repetidas.Keys)
            {
                aux += (chave + ':' + repetidas[chave] + '\n');
            }

            return aux;
        }

        public string ToString(int album)
        {
            string aux = "";
            int numero;

            if (album > QTDAlbums || album <0)
                return aux;

            if (album == QTDAlbums)
            {
                foreach (string time in repetidas.Keys)
                {
                    aux+= time + ": ";
                    for (numero = 0; numero < repetidas[time].Length; numero++)
                    {
                        if (repetidas[time][numero] != 0)
                            aux += $"{((time == "FWC") ? numero : numero + 1)} : {repetidas[time][numero]}, ";
                    }

                    aux += '\n';
                }
                return aux;
            }

            foreach (string time in Albums[album].Keys)
            {
                aux += time + ": ";
                for (numero = 0; numero < Albums[album][time].Length; numero++)
                {
                    if (!Albums[album][time][numero])
                        aux += $"{((time == "FWC") ? numero : numero+1)}, ";
                }

                aux += '\n';
            }
            return aux;
        }



    }
}