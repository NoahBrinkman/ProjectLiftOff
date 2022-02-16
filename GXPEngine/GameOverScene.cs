using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace GXPEngine
{
    public class GameOverScene : Scene
    {
        private EasyDraw canvas;
        private float timer = 10;
        //add highscore thingy here

        private List<Score> highScore;
        private bool shouldAddToHighScore = false;
        private bool hasSaved = false;
        private float timeToSpellName = 30.0f;
        private MyGame myGame;
        private Font textFont;
        private string nameInput = string.Empty;
        private char currentChosenChar = 'A';
        private int currentAlphabetIndex = 0;
        private Char[] alphabet = new Char[26]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z'
        };
        public GameOverScene() : base()
        {
            textFont = Utils.LoadFont("Filetto_Bold.ttf", 50);
            canvas = new EasyDraw(game.width, game.height, false);
            AddChild(canvas);
            myGame = (MyGame)game;
        }

        protected override void Start()
        {
            base.Start();
            highScore = myGame.highScoreManager.LoadData("HighScores");
            if (highScore != null)
            {
                if (highScore.Any(x => x.score < myGame.currentScore) || highScore.Count < 10)
                {
                    shouldAddToHighScore = true;

                }
                else
                {
                    hasSaved = true;
                }
            }
            else
            {
                shouldAddToHighScore = true;
            }

        }

        protected override void Update()
        {
            if (!isActive) return;
            canvas.Clear(0, 0, 0, 0);
            canvas.TextFont(textFont);
            if (shouldAddToHighScore)
            {
                timeToSpellName -= (float)Time.deltaTime / 1000;
                if (timeToSpellName <= 0 || nameInput.Length >= 3)
                {
                    if (highScore != null)
                    {
                        if (highScore.Count >= 10)
                        {
                            myGame.highScoreManager.SaveData("HighScores", myGame.currentScore, nameInput, true);
                        }
                        else
                        {
                            myGame.highScoreManager.SaveData("HighScores", myGame.currentScore, nameInput);
                        }
                    }
                    else
                    {
                        myGame.highScoreManager.SaveData("HighScores", myGame.currentScore, nameInput);
                    }
                    highScore = myGame.highScoreManager.LoadData("HighScores");
                    highScore = highScore.OrderByDescending(x => x.score).ToList();
                    hasSaved = true;
                    shouldAddToHighScore = false;
                }
                canvas.TextFont("Filleto_Bold.ttf",1);
                canvas.TextAlign(CenterMode.Center, CenterMode.Center);
                canvas.TextSize(30);
                canvas.Text("Game Over", game.width / 2, 100);
                canvas.TextSize(20);
                canvas.Text("Select Your name:", game.width / 2, 150);
                canvas.TextSize(30);
                canvas.Text($"{(nameInput.Length > 0 ? nameInput[0].ToString() : '_'.ToString())} {(nameInput.Length > 1 ? nameInput[1].ToString() : '_'.ToString())} {(nameInput.Length > 2 ? nameInput[2].ToString() : '_'.ToString())}", game.width / 2 - 15, game.height / 2);
                canvas.TextSize(20);
                canvas.Text(currentChosenChar.ToString(), game.width / 2, game.height - 200);
                
                if (Input.GetKeyDown(Key.A))
                {
                    if (currentAlphabetIndex == 0)
                    {
                        currentAlphabetIndex = 25;
                    }
                    else
                    {
                        currentAlphabetIndex--;
                    }   
                    currentChosenChar = alphabet[currentAlphabetIndex];
                }
                if (Input.GetKeyDown(Key.D))
                {
                    if (currentAlphabetIndex == 25)
                    {
                        currentAlphabetIndex = 0;
                    }
                    else
                    {
                        currentAlphabetIndex++;
                    }   
                    currentChosenChar = alphabet[currentAlphabetIndex];
                }

                if (Input.GetKeyDown(Key.SPACE))
                {
                    nameInput += currentChosenChar;
                }
            }



            if (hasSaved)
            {
                timer -= (float)Time.deltaTime / 1000;
                if (timer <= 0)
                {
                    SceneManager.instance.ReloadGame();
                }
                canvas.TextAlign(CenterMode.Center, CenterMode.Center);
                canvas.TextSize(40);
                canvas.Text("Game Over", game.width / 2, 100);
                canvas.TextSize(25);
                for (int i = 0; i < highScore.Count; i++) 
                { 
                    canvas.Text($"{i + 1}. {highScore[i].name}: {highScore[i].score}", game.width / 2, 160 + (40 * i)); 
                }

                canvas.TextSize(30);
                canvas.Text("Your score: " + myGame.currentScore, game.width / 2, game.height - 100);
            }
        }
    }
}