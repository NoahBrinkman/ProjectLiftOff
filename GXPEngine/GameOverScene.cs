namespace GXPEngine
{
    public class GameOverScene : Scene
    {
        private EasyDraw canvas;
        private float timer = 10;
        
        
        public GameOverScene() : base()
        {
            canvas = new EasyDraw(game.width, game.height, false);
            AddChild(canvas);
        }
        void Update()
        {
            if(!isActive) return;
            timer -= (float)Time.deltaTime / 1000;
            if (timer <= 0)
            {
                SceneManager.instance.ReloadGame();
            }
            MyGame mg = (MyGame)game;
            canvas.Clear(0,0,0,0);
            canvas.TextAlign(CenterMode.Center,CenterMode.Center);
            canvas.TextSize(30);
            canvas.Text("Game Over", game.width / 2, 100);
            canvas.Text("Your score: " + mg.currentScore, game.width / 2, game.height /2);
        }
    }
}