using NAudio.Wave;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Maze
{
    public partial class Form1 : Form
    {
        private WaveOutEvent player;
        private AudioFileReader audioFile;
        // размеры лабиринта в ячейках 16х16 пикселей
        int columns = 20;
        int rows = 20;
        int health = 100;
        int pictureSize = 16; // ширина и высота одной ячейки

        Labirint l; // ссылка на логику всего происходящего в лабиринте

        public Form1()
        {
            InitializeComponent();
            Options();
            StartGame();
            UpdateHealthText(l.objects[l.CharacterPositionY, l.CharacterPositionX].health);
        
    }
        int score = 0;
        public void UpdateHealthText(int health)
        {
            // Обновить заголовок программы с учетом здоровья героя
            Text = "Meizu - HP: " + health.ToString() + " | Score: " + score.ToString();
        }
        public void Options()
        {
            Text = "Meizu";
            BackColor = Color.FromArgb(255, 92, 118, 137);

            // размеры клиентской области формы (того участка, на котором размещаются ЭУ)
            ClientSize = new Size(columns * pictureSize, rows * pictureSize);

            StartPosition = FormStartPosition.CenterScreen;
        }

        public void StartGame()
        {
            l = new Labirint(this, columns, rows);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                // проверка на то, свободна ли ячейка справа
                if (l.objects[l.CharacterPositionY, l.CharacterPositionX + 1].IsWalkable())//check for wolkability
                {
                    l.objects[l.CharacterPositionY, l.CharacterPositionX].texture = MazeObject.images[0]; // hall
                    l.images[l.CharacterPositionY, l.CharacterPositionX].BackgroundImage = l.objects[l.CharacterPositionY, l.CharacterPositionX].texture;

                    l.CharacterPositionX++;
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.ENEMY)
                    {
                        // Уменьшить здоровье героя на 25
                        health = health - 25;

                        // Обновить текстовое представление здоровья
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();
                        // Проверка на смерть героя
                        if (health <= 0)
                        {
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\sad.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Wasted"); // Показать оповещение о проёбе
                        }


                        else if (health != 0)
                        {
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\enenmyDMG.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                        }
                    }
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.DRUGS)
                    {
                        // Бафнем здоровье героя на 5
                        //l.objects[l.CharacterPositionY, l.CharacterPositionX].health = l.objects[l.CharacterPositionY, l.CharacterPositionX].health + 5;
                        health = health + 5;
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();
                        // Проверка на макс хп
                        if (health >= 100)
                        {
                            health = 100;
                            //l.objects[l.CharacterPositionY, l.CharacterPositionX].health = 100;
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\peredoz.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if (l.objects[l.CharacterPositionY, l.CharacterPositionX].health < 100)
                        {

                            string audioFilePath3 = @"C:\Users\Vlados\Documents\weed.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        // Обновить текстовое представление здоровья
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();



                    }
                    // Проверка, является ли следующий объект MEDAL
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.MEDAL)
                    {
                        score++; // Увеличить счёт на 1
                        UpdateHealthText(l.objects[l.CharacterPositionY, l.CharacterPositionX].health); // Обновить заголовок программы


                        if (score == l.moneyAmount)
                        {
                            string audioFilePath2 = @"C:\Users\Vlados\Documents\israel-song.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath2);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Вы таки собрали все шекели!"); // Показать оповещение о победе
                        }
                        else if(score != l.moneyAmount)
                        {
                            string audioFilePath = @"C:\Users\Vlados\Desktop\money.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }

                    l.objects[l.CharacterPositionY, l.CharacterPositionX].texture = MazeObject.images[4]; // character
                    l.images[l.CharacterPositionY, l.CharacterPositionX].BackgroundImage = l.objects[l.CharacterPositionY, l.CharacterPositionX].texture;
                    if (l.CharacterPositionX == columns-1 || l.CharacterPositionY == rows-1)
                    {
                        string audioFilePath = @"C:\Users\Vlados\Desktop\snd_short3.mp3";
                        try
                        {
                            player = new WaveOutEvent();
                            audioFile = new AudioFileReader(audioFilePath);

                            player.Init(audioFile);
                            player.Play();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        MessageBox.Show("Вы победили!"); // Показать оповещение о победе



                    }
                }
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (l.objects[l.CharacterPositionY, l.CharacterPositionX - 1].IsWalkable())
                  
                {
                    l.objects[l.CharacterPositionY, l.CharacterPositionX].texture = MazeObject.images[0]; // это типо комент
                    l.images[l.CharacterPositionY, l.CharacterPositionX].BackgroundImage = l.objects[l.CharacterPositionY, l.CharacterPositionX].texture;

                    l.CharacterPositionX--;
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.ENEMY)
                    {
                        // Уменьшить здоровье героя на 25
                        health = health - 25;

                        // Обновить текстовое представление здоровья
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();
                        // Проверка на смерть героя
                        if (health <= 0)
                        {
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\sad.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Wasted"); // Показать оповещение о проёбе
                        }


                        else if (health != 0)
                        {
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\enenmyDMG.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                        }
                    }
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.DRUGS)
                    {
                        // Бафнем здоровье героя на 5
                        //l.objects[l.CharacterPositionY, l.CharacterPositionX].health = l.objects[l.CharacterPositionY, l.CharacterPositionX].health + 5;
                        health = health + 5;
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();
                        // Проверка на макс хп
                        if (health >= 100)
                        {
                            health = 100;
                            //l.objects[l.CharacterPositionY, l.CharacterPositionX].health = 100;
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\peredoz.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if (l.objects[l.CharacterPositionY, l.CharacterPositionX].health < 100)
                        {

                            string audioFilePath3 = @"C:\Users\Vlados\Documents\weed.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        // Обновить текстовое представление здоровья
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();



                    }
                    // Проверка, является ли следующий объект MEDAL
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.MEDAL)
                    {
                        score++; // Увеличить счёт на 1
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString(); // Обновить заголовок программы


                        if (score == l.moneyAmount)
                        {
                            string audioFilePath2 = @"C:\Users\Vlados\Documents\israel-song.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath2);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Вы таки собрали все шекели!"); // Показать оповещение о победе
                        }
                        else if (score != l.moneyAmount)
                        {
                            string audioFilePath = @"C:\Users\Vlados\Desktop\money.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    l.objects[l.CharacterPositionY, l.CharacterPositionX].texture = MazeObject.images[4]; // вау,что же это?Комент?
                    l.images[l.CharacterPositionY, l.CharacterPositionX].BackgroundImage = l.objects[l.CharacterPositionY, l.CharacterPositionX].texture;
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (l.objects[l.CharacterPositionY - 1, l.CharacterPositionX].IsWalkable()) //я кста начал в 9:00 10 июня
                {
                    l.objects[l.CharacterPositionY, l.CharacterPositionX].texture = MazeObject.images[0]; // а когда закончу?
                    l.images[l.CharacterPositionY, l.CharacterPositionX].BackgroundImage = l.objects[l.CharacterPositionY, l.CharacterPositionX].texture;

                    l.CharacterPositionY--;

                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.ENEMY)
                    {
                        // Уменьшить здоровье героя на 25
                        health = health - 25;

                        // Обновить текстовое представление здоровья
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();
                        // Проверка на смерть героя
                        if (health <= 0)
                        {
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\sad.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Wasted"); // Показать оповещение о проёбе
                        }


                        else if (health != 0)
                        {
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\enenmyDMG.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                        }
                    }
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.DRUGS)
                    {
                        // Бафнем здоровье героя на 5
                        //l.objects[l.CharacterPositionY, l.CharacterPositionX].health = l.objects[l.CharacterPositionY, l.CharacterPositionX].health + 5;
                        health = health + 5;
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();
                        // Проверка на макс хп
                        if (health >= 100)
                        {
                            health = 100;
                            //l.objects[l.CharacterPositionY, l.CharacterPositionX].health = 100;
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\peredoz.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        else if (l.objects[l.CharacterPositionY, l.CharacterPositionX].health < 100)
                        {

                            string audioFilePath3 = @"C:\Users\Vlados\Documents\weed.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        // Обновить текстовое представление здоровья
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();



                    }
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.MEDAL)
                    {
                        score++; // Увеличить счёт на 1
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString(); // Обновить заголовок программы


                        if (score == l.moneyAmount)
                        {
                            string audioFilePath2 = @"C:\Users\Vlados\Documents\israel-song.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath2);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Вы таки собрали все шекели!"); // Показать оповещение о победе
                        }
                        else if (score != l.moneyAmount)
                        {
                            string audioFilePath = @"C:\Users\Vlados\Desktop\money.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    l.objects[l.CharacterPositionY, l.CharacterPositionX].texture = MazeObject.images[4]; // character
                    l.images[l.CharacterPositionY, l.CharacterPositionX].BackgroundImage = l.objects[l.CharacterPositionY, l.CharacterPositionX].texture;
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (l.objects[l.CharacterPositionY + 1, l.CharacterPositionX].IsWalkable()) // проверяем ячейку левее на 1 позицию, является ли она коридором
                {
                    l.objects[l.CharacterPositionY, l.CharacterPositionX].texture = MazeObject.images[0]; // hall
                    l.images[l.CharacterPositionY, l.CharacterPositionX].BackgroundImage = l.objects[l.CharacterPositionY, l.CharacterPositionX].texture;

                    l.CharacterPositionY++;
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.ENEMY)
                    {
                        // Уменьшить здоровье героя на 25
                        health = health - 25;

                        // Обновить текстовое представление здоровья
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();
                        // Проверка на смерть героя
                        if (health <= 0)
                        {
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\sad.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Wasted"); // Показать оповещение о проёбе
                        }


                        else if (health != 0)
                        {
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\enenmyDMG.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                           

                        }
                    }
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.DRUGS)
                    {
                        // Бафнем здоровье героя на 5
                        //l.objects[l.CharacterPositionY, l.CharacterPositionX].health = l.objects[l.CharacterPositionY, l.CharacterPositionX].health + 5;
                        health = health + 5;
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();
                        // Проверка на макс хп
                        if (health >= 100)
                        {
                            health = 100;
                            //l.objects[l.CharacterPositionY, l.CharacterPositionX].health = 100;
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\peredoz.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                        }
                         else if (l.objects[l.CharacterPositionY, l.CharacterPositionX].health < 100)
                        {
                            
                            string audioFilePath3 = @"C:\Users\Vlados\Documents\weed.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath3);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        // Обновить текстовое представление здоровья
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString();



                    }
                    if (l.objects[l.CharacterPositionY, l.CharacterPositionX].type == MazeObject.MazeObjectType.MEDAL)
                    {
                        score++; // Увеличить счёт на 1
                        Text = "Meizu - HP: " + health + " | Score: " + score.ToString(); // Обновить заголовок программы


                        if (score == l.moneyAmount)
                        {
                            string audioFilePath2 = @"C:\Users\Vlados\Documents\israel-song.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath2);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            MessageBox.Show("Вы таки собрали все шекели!"); // Показать оповещение о победе
                        }
                        else if (score != l.moneyAmount)
                        {
                            string audioFilePath = @"C:\Users\Vlados\Desktop\money.mp3";
                            try
                            {
                                player = new WaveOutEvent();
                                audioFile = new AudioFileReader(audioFilePath);

                                player.Init(audioFile);
                                player.Play();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка при воспроизведении аудио: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    l.objects[l.CharacterPositionY, l.CharacterPositionX].texture = MazeObject.images[4]; // character
                    l.images[l.CharacterPositionY, l.CharacterPositionX].BackgroundImage = l.objects[l.CharacterPositionY, l.CharacterPositionX].texture;
                }
            }
        }
    }
}
