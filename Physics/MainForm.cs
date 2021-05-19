using System;
using System.Drawing;
using System.Windows.Forms;

namespace Physics
{
    public partial class MainForm : Form
    {

        public int[,] map;
        public Label[,] labels;
        public PictureBox[,] pics;
        public PictureBox[,] mapPics;
        private int score = 0;
        bool isHard = false;
        int N = 4;
        bool isInfinity = false;

        public MainForm()
        {
            InitializeComponent();
           
            this.KeyDown += new KeyEventHandler(OnKeyboardPressed);
            простоToolStripMenuItem.Enabled = false;
            map = new int[4, 4];
            labels = new Label[4, 4];
            pics = new PictureBox[4, 4];
            mapPics = new PictureBox[4, 4];
            CreateMap(4);
            GenerateNewPic(4);
            GenerateNewPic(4);
            Activate();
        }
        private void randomize(int N)
        {
            ClearPlayField(N);
            CreateMap(N);
            Random rnd = new Random();
            string[] nominals = new string[8] { "2", "4", "8", "16", "32", "64", "128", "256" };
            int countOfFields = rnd.Next(6, N * N - 1);
            for (int i = 0; i < countOfFields; i++)
            {
                int index = rnd.Next(0, 7);
                GenerateNewPic(nominals[index]);
            }
        }

        private void ClearPlayField(int N) {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    map[i, j] = 0;
                    
                    Controls.Remove(pics[i, j]);
                    Controls.Remove(labels[i, j]);
                    Controls.Remove(mapPics[i, j]);
                    mapPics[i, j] = null;
                    pics[i, j] = null;
                    labels[i, j] = null;
                }
            }
        }

        private void refresh()
        {
            score = 0;
            ClearPlayField(4);
            Refresh();
            CreateMap(4);
            GenerateNewPic(4);
            GenerateNewPic(4);
        }
        private bool chekLoose()
        {
            bool loose = false;
            int fields = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (map[i, j] == 1)
                        fields++;
                }
            }

            if (fields == N * N)
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {

                        if (j + 1 < N)
                        {
                            int a = int.Parse(labels[i, j].Text);
                            int b = int.Parse(labels[i, j + 1].Text);
                            if (a != b)
                                loose = true;
                            else
                            {
                                loose = false;
                                return false;

                            }
                        }

                        if (j - 1 > 0)
                        {
                            int a = int.Parse(labels[i, j].Text);
                            int b = int.Parse(labels[i, j - 1].Text);
                            if (a != b)
                                loose = true;
                            else
                            {
                                loose = false;
                                return false;

                            }
                        }

                        if (i + 1 < N)
                        {
                            int a = int.Parse(labels[i, j].Text);
                            int b = int.Parse(labels[i + 1, j].Text);
                            if (a != b)
                                loose = true;
                            else
                            {
                                loose = false;
                                return false;

                            }
                        }

                        if (i - 1 > 0)
                        {
                            int a = int.Parse(labels[i, j].Text);
                            int b = int.Parse(labels[i - 1, j].Text);
                            if (a != b)
                                loose = true;
                            else
                            {
                                loose = false;
                                return false;

                            }
                        }
                    }
                }
            return loose;
        }
        private bool CheckWin() 
        {
            bool isWin = false;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if(labels[i,j]!=null)
                        if (labels[i, j].Text == "2048")
                        {
                            isWin = true;
                            return true;
                        }

                }
            }
            return isWin;
        }
        private void ShowAddResultForm(int score,bool isHardLevel)
        {
            DialogResult dialogResult = new DialogResult();
            dialogResult = MessageBox.Show("Хотите соханить свой результат?", "Сохранение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            if (dialogResult == DialogResult.Yes)
            {
                AddResultForm resultForm = new AddResultForm(score,isHardLevel);
                resultForm.Show();
                resultForm.Activate();
            }
        }

        private void CreateMap(int N)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Location = new Point(12 + 56 * j, 73 + 56 * i);
                    pic.Size = new Size(50, 50);
                    pic.BackColor = Color.Gray;
                    mapPics[i, j] = pic;
                    this.Controls.Add(mapPics[i, j]);
                }
            }
        }

        private void GenerateNewPic(int N)
        {
            Random rnd = new Random();
            int a = rnd.Next(0, N);
            int b = rnd.Next(0, N);
            while (pics[a, b] != null)
            {
                a = rnd.Next(0, N);
                b = rnd.Next(0, N);
            }
            map[a, b] = 1;
            pics[a, b] = new PictureBox();
            labels[a, b] = new Label();
            labels[a, b].Text = "2";
            labels[a, b].Size = new Size(50, 50);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[a, b].Controls.Add(labels[a, b]);

            pics[a, b].Location = new Point(12 + b * 56, 73 + 56 * a);
            pics[a, b].Size = new Size(50, 50);
            pics[a, b].BackColor = Color.Yellow;
            this.Controls.Add(pics[a, b]);
            pics[a, b].BringToFront();
        }
        private void GenerateNewPic(string Nominal)
        {
            Random rnd = new Random();
            int a = rnd.Next(0, N);
            int b = rnd.Next(0, N);
            while (pics[a, b] != null)
            {
                a = rnd.Next(0, N);
                b = rnd.Next(0, N);
            }
            map[a, b] = 1;
            pics[a, b] = new PictureBox();
            labels[a, b] = new Label();
            labels[a, b].Text = Nominal;
            labels[a, b].Size = new Size(50, 50);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[a, b].Controls.Add(labels[a, b]);
            pics[a, b].Location = new Point(12 + b * 56, 73 + 56 * a);
            pics[a, b].Size = new Size(50, 50);
            ChangeColor(Nominal, a, b);
            this.Controls.Add(pics[a, b]);
            pics[a, b].BringToFront();
        }

        private void ChangeColor(int sum, int k, int j)
        {
            if (sum % 1024 == 0) pics[k, j].BackColor = Color.Pink;
            else if (sum % 512 == 0) pics[k, j].BackColor = Color.Red;
            else if (sum % 256 == 0) pics[k, j].BackColor = Color.DarkViolet;
            else if (sum % 128 == 0) pics[k, j].BackColor = Color.Blue;
            else if (sum % 64 == 0) pics[k, j].BackColor = Color.Brown;
            else if (sum % 32 == 0) pics[k, j].BackColor = Color.Coral;
            else if (sum % 16 == 0) pics[k, j].BackColor = Color.Cyan;
            else if (sum % 8 == 0) pics[k, j].BackColor = Color.Maroon;
            else pics[k, j].BackColor = Color.Green;
        }

        private void ChangeColor(string Nominal, int k, int j)
        {
            if (Nominal == "1024") pics[k, j].BackColor = Color.Pink;
            else if (Nominal == "512" ) pics[k, j].BackColor = Color.Red;
            else if (Nominal == "256" ) pics[k, j].BackColor = Color.DarkViolet;
            else if (Nominal == "128" ) pics[k, j].BackColor = Color.Blue;
            else if (Nominal == "64") pics[k, j].BackColor = Color.Brown;
            else if (Nominal == "32") pics[k, j].BackColor = Color.Coral;
            else if (Nominal == "16" ) pics[k, j].BackColor = Color.Cyan;
            else if (Nominal == "8") pics[k, j].BackColor = Color.Maroon;
            else pics[k, j].BackColor = Color.Green;
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            bool ifPicWasMoved = false;
            
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    for (int k = 0; k < N; k++)
                    {
                        for (int l = N-1; l >= 0; l--)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = l + 1; j < N; j++)
                                {
                                    if (map[k, j] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[k, j - 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j - 1];
                                        pics[k, j - 1] = null;
                                        labels[k, j] = labels[k, j - 1];
                                        labels[k, j - 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X + 56, pics[k, j].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, j].Text);
                                        int b = int.Parse(labels[k, j - 1].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[k, j].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, k, j);
                                            label1.Text = "Score: " + score;
                                            map[k, j - 1] = 0;
                                            this.Controls.Remove(pics[k, j - 1]);
                                            this.Controls.Remove(labels[k, j - 1]);
                                            pics[k, j - 1] = null;
                                            labels[k, j - 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "Left":
                    for (int k = 0; k < N; k++)
                    {
                        for (int l = 1; l < N; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = l - 1; j >= 0; j--)
                                {
                                    if (map[k, j] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[k, j + 1] = 0;
                                        map[k, j] = 1;
                                        pics[k, j] = pics[k, j + 1];
                                        pics[k, j + 1] = null;
                                        labels[k, j] = labels[k, j + 1];
                                        labels[k, j + 1] = null;
                                        pics[k, j].Location = new Point(pics[k, j].Location.X - 56, pics[k, j].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[k, j].Text);
                                        int b = int.Parse(labels[k, j + 1].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[k, j].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, k, j);
                                            label1.Text = "Score: " + score;
                                            map[k, j + 1] = 0;
                                            this.Controls.Remove(pics[k, j + 1]);
                                            this.Controls.Remove(labels[k, j + 1]);
                                            pics[k, j + 1] = null;
                                            labels[k, j + 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "Down":
                    for (int k = N-1; k >= 0; k--)
                    {
                        for (int l = 0; l < N; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k + 1; j < N; j++)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[j - 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j - 1, l];
                                        pics[j - 1, l] = null;
                                        labels[j, l] = labels[j - 1, l];
                                        labels[j - 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y + 56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j - 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[j, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, j, l);
                                            label1.Text = "Score: " + score;
                                            map[j - 1, l] = 0;
                                            this.Controls.Remove(pics[j - 1, l]);
                                            this.Controls.Remove(labels[j - 1, l]);
                                            pics[j - 1, l] = null;
                                            labels[j - 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
                case "Up":
                    for (int k = 1; k < N; k++)
                    {
                        for (int l = 0; l < N; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k - 1; j >= 0; j--)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[j + 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j + 1, l];
                                        pics[j + 1, l] = null;
                                        labels[j, l] = labels[j + 1, l];
                                        labels[j + 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y - 56);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j + 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[j, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, j, l);
                                            label1.Text = "Score: " + score;
                                            map[j + 1, l] = 0;
                                            this.Controls.Remove(pics[j + 1, l]);
                                            this.Controls.Remove(labels[j + 1, l]);
                                            pics[j + 1, l] = null;
                                            labels[j + 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }

            if (ifPicWasMoved)
            {
                if (isHard)
                {
                    GenerateNewPic(N);
                    GenerateNewPic(N);
                }
                else
                {
                    GenerateNewPic(N);
                }
            }
            bool ifLoose = chekLoose();
            bool isWin = CheckWin();
            if (ifLoose)
            {
                MessageBox.Show("Вы проиграли!");
                ShowAddResultForm(score,isHard);
                refresh();
            }else if(isWin && !isInfinity)
            {
                MessageBox.Show("Вы победили!");
                ShowAddResultForm(score, isHard);
                refresh();
            }
        }

        private void завериштьСейчасToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void сгенерироватьСлучайноеПолеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            randomize(N);
        }

        private void таблицаЛидеровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LeadersForm lf = new LeadersForm();
            lf.Show();
            lf.Activate();
        }

        private void сложноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isHard = true;
            
            сложноToolStripMenuItem.Enabled = false;
            простоToolStripMenuItem.Enabled = true;
        }

        private void простоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isHard = false;
            простоToolStripMenuItem.Enabled = false;
            сложноToolStripMenuItem.Enabled = true;
        }

       

        private void бескоенечныйРежимToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isInfinity = true;
        }

        private void помощьИПодсказкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Раздел помощи\n" +
               "Навигация по меню:\n" +
               "В меню имеется 7 разделов:\n" +
               "1. Завершить сейчас - завершить досрочно без возможности сохранить счет\n" +
               "2. Сгенерировать случайное поле - генерация случайных ячеек на поле\n" +
               "3. Помощь и подсказки - информационный раздел\n" +
               "4. Таблица лидеров - таблица с указанием имени игрока и его счета\n" +
               "5. Изменить сложность - В игре имеются 2 уровня сложности: сложный и простой.\n" +
               "Сложный: появляется по 2 ячейки за ход,\n" +
               "Простой: одна ячейка за ход\n" +
               "6. Изменить размер поля - в игре имеется 2 размера поля 4х4 и 6х6\n" +
               "7. Бесконечный режим - режим без победного конца, 2048 - не предел", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void возможнныеСтратегииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Первое что следует сделать – определиться с рабочей областью, тем участком игрового поля, где вы собственно и будете собирать «2048». " +
                "Им может быть любой из углов. На рисунке выше показан вариант с расположением плитки наибольшего значения в левом нижнем углу." +
                " Суть всего метода в том, чтобы исключить одно из направлений движения. В нашем случае это «вверх». Просто, если вы поднимете все блоки вверх (особенно на поздних этапах игры, когда большая часть поля уже занята плитками)," +
                " на месте в углу с высокой долей вероятности может возникнуть «2» или «4». Это сломает стратегию, ведь «костяшку» такого значения очень тяжело будет собрать," +
                " особенно если она окружена плитками достояния 64 и больше. В противном же случае, собирая именно в угловой клетке наибольшее число, вероятность победы равняется 80%. " +
                "Остальные блоки нужно стараться собирать в этой же строке по принципу уменьшения их номинала (как показано на рисунке). Это позволит вам, используя большую часть игрового поля, " +
                "собирать нужные плитки и поочерёдно их соединять, что очень удобно.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

        }

        private void х6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(N==4)
                ClearPlayField(4);
            else if(N==6)
                ClearPlayField(4);
            map = new int[6, 6];
            labels = new Label[6, 6];
            pics = new PictureBox[6, 6];
            mapPics = new PictureBox[6, 6];

            CreateMap(6);
            GenerateNewPic(6);
            GenerateNewPic(6);
            Refresh();
            N = 6;
            х6ToolStripMenuItem.Enabled = false;
            х4ToolStripMenuItem.Enabled = true;
        }

        private void х4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearPlayField(6);
            map = new int[4, 4];
            labels = new Label[4, 4];
            pics = new PictureBox[4, 4];
            mapPics = new PictureBox[4, 4];

            CreateMap(4);
            GenerateNewPic(4);
            GenerateNewPic(4);
            Refresh();
            N = 4;
            х6ToolStripMenuItem.Enabled = true;
            х4ToolStripMenuItem.Enabled = false;
            this.Width = 265;
            this.Height = 355;
        }

        private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вы играете в игру под названеим 2048\n" +
                "Правила:\n" +
                "1. В каждом раунде появляется плитка номинала «2» (две, если уровень игры - сложный)\n " +
                "2. Нажатием стрелки Вы можету скинуть все плитки игрового поля в одну из 4 сторон." +
                "Если при сбрасывании две плитки одного номинала «налетают» одна на другую, то они превращаются в одну, номинал которой равен сумме соединившихся плиток." +
                "После каждого хода на свободной секции поля появляется новая плитка номиналом «2» или «4». Если при нажатии кнопки местоположение плиток или их номинал не изменится, то ход не совершается.\n" +
                "3. Если в одной строчке или в одном столбце находится более двух плиток одного номинала, то при сбрасывании они начинают соединяться с той стороны, в которую были направлены.Например, находящиеся в одной строке плитки(4, 4, 4) после хода влево превратятся в(8, 4), а после хода вправо — в(4, 8).Данная обработка неоднозначности позволяет более точно формировать стратегию игры.\n" +
                "4. За каждое соединение игровые очки увеличиваются на номинал получившейся плитки.\n" +
                "5. Игра заканчивается поражением, если после очередного хода невозможно совершить действие.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

        }
    }
}
