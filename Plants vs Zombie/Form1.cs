using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using NAudio.Wave;

namespace Plants_vs_Zombie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        SoundPlayer player = new SoundPlayer("C:\\Users\\Fryzs\\source\\repos\\Plants vs Zombie\\Plants vs Zombie\\MainTheme.wav");
        List<PictureBox> availableSunflowers = new List<PictureBox>();
        List<PictureBox> availablePeas = new List<PictureBox>();
        List<PictureBox> SunnyDr = new List<PictureBox>();
        List<PictureBox> PeaList = new List<PictureBox>();
        List<PictureBox> PeaFlower = new List<PictureBox>();
        List<PictureBox> Zombie = new List<PictureBox>();

        int[] ZombiePositions = { 84, 175, 283, 374, 477 };
        int Type = 0;
        int SunflowerScore = 0;
        int PeaScore = 0;
        int Song = 0;
        private IWavePlayer zombieDiedPlayer;
        private AudioFileReader zombieDiedReader;

        int[] PeaHealth = new int[45];
        int[] SunnyHealth = new int[45];

        string[] SunflowerNames = new string[45];
        Point[] SunflowerPositions = new Point[45];

        string[] PeaNames = new string[45];
        Point[] PeaPositions = new Point[45];

        private void Form1_Load(object sender, EventArgs e) {
   
            switch (Song)
            {
                case 0:
                    
                    player.PlayLooping();
                    break;
                default:
                    break;
            }
            
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if ((Convert.ToInt32(((PictureBox)sender).Tag) == 0))
            {
                switch (Type)
                {
                    case 0:
                        break;
                    case 1: // Sunflower
                        ((PictureBox)sender).Image = Properties.Resources.SunflowerPng;
                        ((PictureBox)sender).SizeMode = PictureBoxSizeMode.StretchImage;
                        ((PictureBox)sender).Tag = 1;
                        SunflowerNames[SunflowerScore] = ((PictureBox)sender).Name; // Зберігаємо назву
                        SunflowerPositions[SunflowerScore] = ((PictureBox)sender).Location; // Зберігаємо позицію
                        ((PictureBox)sender).Invalidate();
                        SunnyHealth[SunflowerScore] = 100;
                        SunflowerScore++;
                        Type = 0;
                        break;

                    case 2: // Pea
                        ((PictureBox)sender).Image = Properties.Resources.PeaPng;
                        ((PictureBox)sender).SizeMode = PictureBoxSizeMode.StretchImage;
                        ((PictureBox)sender).Tag = 2;
                        PeaNames[PeaScore] = ((PictureBox)sender).Name; // Зберігаємо назву
                        PeaPositions[PeaScore] = new Point(((PictureBox)sender).Location.X + 60, ((PictureBox)sender).Location.Y + 12);
                        PeaFlower.Add(((PictureBox)sender));
                        ((PictureBox)sender).Invalidate();
                        PeaHealth[PeaScore] = 100;  
                        PeaScore++;
                        Type = 0;
                        break;
                }
            }

            // Удаление
            if (Type == 3)
            {
                DeleteFlower(sender);
            } 

        }
        private void DeleteFlower(object sender)
        {
                ((PictureBox)sender).Image = null;
                ((PictureBox)sender).Invalidate();

                if (Convert.ToInt32(((PictureBox)sender).Tag) == 1) // Sunflower
                {
                    for (int i = 0; i < SunflowerScore; i++)
                    {
                        if (((PictureBox)sender).Name == SunflowerNames[i])
                        {
                            // Зсуваємо масив вліво
                            for (int j = i; j < SunflowerScore - 1; j++)
                            {
                                SunflowerNames[j] = SunflowerNames[j + 1];
                                  SunnyHealth[j] = SunnyHealth[j+ 1]; 
                            }

                            ((PictureBox)sender).Tag = 0;
                            // Зменшуємо лічильник
                            SunflowerScore--;

                            // Видалення з SunnyDr
                            SunnyDr.Remove((PictureBox)sender);
                            break; // Виходимо з циклу, оскільки рослина вже видалена
                        }
                    }
                }
                else if (Convert.ToInt32(((PictureBox)sender).Tag) == 2) // Pea
                {
                    for (int i = 0; i < PeaScore; i++)
                    {
                        if (((PictureBox)sender).Name == PeaNames[i])
                        {
                            // Зсуваємо масив вліво
                            for (int j = i; j < PeaScore - 1; j++)
                            {
                                PeaNames[j] = PeaNames[j + 1];
                                PeaHealth[j] = PeaHealth[j + 1];
                            }
                            ((PictureBox)sender).Tag = 0;
                        // Видалення з PeaList
                        PeaList.Remove((PictureBox)sender);
                        PeaFlower.Remove(((PictureBox)sender));
                                                             // Зменшуємо лічильник
                            PeaScore--;
                            break; // Виходимо з циклу, оскільки рослина вже видалена
                        }
                    
                    }
           
                }
         Type = 0;
        }
        private void sunflower_Click(object sender, EventArgs e)
        {
            if (Type == 0)
            {
                if (Convert.ToInt32(Sunnyscore.Text) >= 50)
                {
                    int currentScore = Convert.ToInt32(Sunnyscore.Text);
                    currentScore -= 50;
                    Sunnyscore.Text = currentScore.ToString();
                    Type = 1;
                }
            }
        }

        private void Pea_Click(object sender, EventArgs e)
        {
            if (Type == 0)
            {
                if (Convert.ToInt32(Sunnyscore.Text) >= 100)
                {
                    int currentScore = Convert.ToInt32(Sunnyscore.Text);
                    currentScore -= 100;
                    Sunnyscore.Text = currentScore.ToString();
                    Type = 2;
                }
            }
        }

        private void pictureBox46_Click(object sender, EventArgs e)
        {
            Type = 3; // Устанавливаем режим удаления
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < SunflowerScore; i++)
            {
                var newPictureBox = GetAvailableSunflower(); // Отримуємо доступний PictureBox

                // Налаштовуємо властивості
                newPictureBox.Size = new Size(30, 30); // Встановіть розмір
                newPictureBox.Location = SunflowerPositions[i]; // Встановіть позицію
                newPictureBox.Image = Properties.Resources.Sun; // Встановіть зображення
                newPictureBox.SizeMode = PictureBoxSizeMode.StretchImage; // Встановіть режим відображення
                newPictureBox.Tag = i; // Можна додати тег для ідентифікації
                newPictureBox.BackColor = Color.Transparent;

                // Додаємо PictureBox на форму
                this.Controls.Add(newPictureBox);
                newPictureBox.BringToFront();

                SunnyDr.Add(newPictureBox); // Додаємо до списку активних
                newPictureBox.Click += Sunny_Click; // Додаємо обробник кліка
            }
        }

        // Метод для отримання доступного PictureBox
        private PictureBox GetAvailableSunflower()
        {
            if (availableSunflowers.Count > 0)
            {
                var sunflower = availableSunflowers[0];
                availableSunflowers.RemoveAt(0);
                sunflower.Visible = true; // Повертаємо видимість
                return sunflower;
            }
            else
            {
                // Створюємо новий якщо немає доступних
                PictureBox newSunflower = new PictureBox
                {
                    Size = new Size(30, 30),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                return newSunflower; // Повертаємо новий об'єкт
            }
        }

        private void Sunny_Click(object sender, EventArgs e)
        {
            // Видаляємо PictureBox при натисканні
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox != null)
            {
                int currentScore = Convert.ToInt32(Sunnyscore.Text); // Перетворюємо текст у число
                currentScore += 25; // Додаємо 25
                Sunnyscore.Text = currentScore.ToString();
                this.Controls.Remove(clickedPictureBox); // Видаляємо PictureBox з форми
                clickedPictureBox.Visible = false; // Сховуємо об'єкт
                availableSunflowers.Add(clickedPictureBox); // Додаємо в пул доступних
            }
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            UpdateSunny();
            UpdatePeas();
            UpdateZombies();
        }

        private void UpdateSunny()
        {
            for (int i = SunnyDr.Count - 1; i >= 0; i--)
            {
                var pictureBox = SunnyDr[i];
                pictureBox.Location = new Point(pictureBox.Location.X, pictureBox.Location.Y + 6);

                if (pictureBox.Location.Y > 600)
                {
                    this.Controls.Remove(pictureBox);
                    pictureBox.Visible = false; // Сховуємо об'єкт замість його видалення
                    availableSunflowers.Add(pictureBox); // Додаємо в пул доступних
                    SunnyDr.RemoveAt(i); // Використовуйте RemoveAt для коректного видалення елементу
                }
            }
        }

        private PictureBox GetAvailablePea()
        {
            if (availablePeas.Count > 0)
            {
                var pea = availablePeas[0];
                availablePeas.RemoveAt(0);
                pea.Visible = true; // Повертаємо видимість
                return pea;
            }
            else
            {
                // Створюємо новий якщо немає доступних
                PictureBox newPea = new PictureBox
                {
                    Size = new Size(30, 30),
                    Image = Properties.Resources.PeaBool,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                    Tag = "pea"
                };
                this.Controls.Add(newPea);
                newPea.BringToFront();
                return newPea;
            }
        }

        private void UpdatePeas()
        {
            for (int i = PeaList.Count - 1; i >= 0; i--)
            {
                var peaPictureBox = PeaList[i];
                peaPictureBox.Location = new Point(peaPictureBox.Location.X + 20, peaPictureBox.Location.Y);

                if (peaPictureBox.Location.X > this.ClientSize.Width) // Перевіряємо, чи не вийшов за межі
                {
                    this.Controls.Remove(peaPictureBox);

                    peaPictureBox.Visible = false;
                    availablePeas.Add(peaPictureBox); // Додаємо в пул доступних
                    PeaList.RemoveAt(i); // Видаляємо з активного списку
                }
                //Перевірка на попадання в зомбі
                for (int j = Zombie.Count - 1; j >= 0; j--)
                {
                    var zombiePictureBox = Zombie[j];

                    if (peaPictureBox.Bounds.IntersectsWith(zombiePictureBox.Bounds))
                    {
                        PlayDamageSound();
                        if (Convert.ToInt32(zombiePictureBox.Tag) <= 0)
                        {
                            PlayZombieDiedSound();
                            this.Controls.Remove(zombiePictureBox); // Видаляємо зомбі з форми
                            zombiePictureBox.Dispose(); // Звільняємо ресурси PictureBox
                            Zombie.RemoveAt(j); // Видаляємо зомбі зі списку активних
                        }

                        zombiePictureBox.Tag = Convert.ToInt32(zombiePictureBox.Tag) - 15;//Дамаг по зомбі

                        this.Controls.Remove(peaPictureBox);

                        peaPictureBox.Visible = false;
                        availablePeas.Add(peaPictureBox); // Додаємо в пул доступних
                        PeaList.RemoveAt(i); // Видаляємо з активного списку
                    }
                }
            }
        }


        // Таймер для стрільби горохом
        private void PeaShoot_Tick(object sender, EventArgs e)
        {
            // Генеруємо нові горохи на основі актуальної кількості
            for (int i = 0; i < PeaScore; i++)
            {
                var PeaFlowereBox = PeaFlower[i];
                for (int j = Zombie.Count - 1; j >= 0; j--)
                {
                    var ZombieBox = Zombie[j];
                    if (PeaFlowereBox.Location.Y == ZombieBox.Location.Y)
                    {
                        var newPictureBox = GetAvailablePea(); // Отримуємо доступний горох
                        newPictureBox.Location = PeaPositions[i]; // Встановлюємо позицію
                        PeaList.Add(newPictureBox); // Додаємо в список активних горохів
                        this.Controls.Add(newPictureBox); // Додаємо горох на форму
                        newPictureBox.BringToFront();
                    }
                }
            }
        }

        private PictureBox CreateZombie()
        {
            PictureBox newZombie = new PictureBox
            {
                Size = new Size(40, 60),
                Image = Properties.Resources.ZombiePng, // Вкажіть правильне зображення
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Tag = "100"
            };
            int YPosition = new Random().Next(0, 5); // Генеруємо випадкову позицію по Y
            newZombie.Location = new Point(this.ClientSize.Width, ZombiePositions[YPosition]); // Розміщуємо зомбі на правому краю форми
            this.Controls.Add(newZombie);
            newZombie.BringToFront();
            return newZombie;
        }

        private void ZomdieSpawn_Tick(object sender, EventArgs e)
        {
            if (GameProgress.Value < GameProgress.Maximum)
            {
                // Створюємо нового зомбі та додаємо його в список активних
                var newZombie = CreateZombie();
                Zombie.Add(newZombie); // Додаємо зомбі в список активних
                GameProgress.Value++;
            }
            else if (Zombie == null)
            {
                MessageBox.Show("Перемога!");
            }
        }

        private void UpdateZombies()
        {
            for (int i = PeaScore - 1; i >= 0; i--)
            {
                var PeaFlowereBox = PeaFlower[i];
                for (int j = 0; j < Zombie.Count; j++)
                {
                    var zombiePictureBox = Zombie[j];
                    if (PeaFlowereBox.Bounds.IntersectsWith(zombiePictureBox.Bounds))
                    {
                        PeaHealth[i] -= 25;
                        if(PeaHealth[i] <= 0)
                        {
                            DeleteFlower(PeaFlower[i]);
                        }
                    }
                    else
                    {
                        zombiePictureBox.Location = new Point(zombiePictureBox.Location.X - 1, zombiePictureBox.Location.Y); // Рухаємо зомбі вліво
                    }
                    if (zombiePictureBox.Location.X < 10) // Якщо зомбі вийшов за лівий край
                    {
                        MainTimer.Enabled = false;
                        PeaShoot.Enabled = false;
                        Sunnydrop.Enabled = false;
                        ZomdieSpawn.Enabled = false;
                        player.Stop();
                        Menu form = new Menu();
                        form.ShowDialog();        
                        /*  this.Controls.Remove(zombiePictureBox); // Видаляємо зомбі з форми
                          zombiePictureBox.Dispose(); // Звільняємо ресурси PictureBox
                          Zombie.RemoveAt(j); // Видаляємо зомбі зі списку активних
                        */
                    }
                }


            }

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void MenuBut_Click(object sender, EventArgs e)
        {
            Menu form = new Menu();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {

            }
        }
        private void PlayDamageSound()
        {
            // Используем новый экземпляр, чтобы звук не прерывал другие
            var damagePlayer = new WaveOutEvent();
            var damageReader = new AudioFileReader("C:\\Users\\Fryzs\\source\\repos\\Plants vs Zombie\\Plants vs Zombie\\DamageSound.wav");
            damagePlayer.Init(damageReader);
            damagePlayer.Play();

            // Освобождаем ресурсы после воспроизведения
            damagePlayer.PlaybackStopped += (sender, args) =>
            {
                damageReader.Dispose();
                damagePlayer.Dispose();
            };
        }
        private void PlayZombieDiedSound()
        {
            // Создаем новый экземпляр для каждого воспроизведения
            zombieDiedPlayer = new WaveOutEvent();
            zombieDiedReader = new AudioFileReader("C:\\Users\\Fryzs\\source\\repos\\Plants vs Zombie\\Plants vs Zombie\\ZombieDied.wav");
            zombieDiedPlayer.Init(zombieDiedReader);
            zombieDiedPlayer.Play();

            // Освобождаем ресурсы после воспроизведения
            zombieDiedPlayer.PlaybackStopped += (sender, args) =>
            {
                zombieDiedReader.Dispose();
                zombieDiedPlayer.Dispose();
            };
        }
    }

}
