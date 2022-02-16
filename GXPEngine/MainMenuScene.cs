using System.Collections.Generic;
using System.Drawing;

namespace GXPEngine
{
    public class MainMenuScene : Scene
    {
        private EasyDraw canvas;
        private List<Score> highScore;
        private float timeBeforeTextSwitch = 2;
        private bool pullLeverTextShouldBeVisible = true;
        private Font titleFont;
        private Font textFont;
        public MainMenuScene() : base()
        {
            titleFont = Utils.LoadFont("Manils.otf", 50);
            textFont = Utils.LoadFont("Filetto_Bold.ttf", 50);
            canvas = new EasyDraw(game.width, game.height, false);
            AddChild(canvas);
            pullLeverTextShouldBeVisible = true;
        }

        protected override void Start()
        {
            base.Start();
            MyGame myGame = (MyGame)game;
            highScore = myGame.highScoreManager.LoadData("HighScores");
        }
        
        protected override void Update()
        {
            if(!isActive) return;
            canvas.Clear(0,0,0,0);
            canvas.TextAlign(CenterMode.Center,CenterMode.Center);
            canvas.TextSize(40);
            canvas.TextFont(titleFont);
            canvas.Text("Super Galaxy Hopper", game.width / 2, 100);
            canvas.TextFont(textFont);
            if (pullLeverTextShouldBeVisible)
            {
                canvas.TextSize(30);
                canvas.Text("Pull the lever back to start", game.width / 2, game.height - 100);
            }
            if (highScore != null)
            { 
                canvas.TextSize(20);
                for (int i = 0; i < highScore.Count; i++) 
                { 
                    canvas.Text($"{i + 1}. {highScore[i].name}: {highScore[i].score}", game.width / 2, 160 + (40 * i)); 
                }
            }

            if (pullLeverTextShouldBeVisible)
            {
                timeBeforeTextSwitch -= (float)Time.deltaTime / 1000;
            }
            else
            {
                timeBeforeTextSwitch += (float)Time.deltaTime / 1000;
            }

            if (timeBeforeTextSwitch <= 0 || timeBeforeTextSwitch >= 2f)
            {
                pullLeverTextShouldBeVisible = !pullLeverTextShouldBeVisible;
            }
            
            if (Input.GetKeyUp(Key.SPACE))
            {
                SceneManager.instance.TryLoadNextScene();
            }
        }
        
    }
}