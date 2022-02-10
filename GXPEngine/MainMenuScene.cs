namespace GXPEngine
{
    public class MainMenuScene : Scene
    {
        private EasyDraw canvas;
        
        public MainMenuScene() : base()
        {
            canvas = new EasyDraw(game.width, game.height, false);
            AddChild(canvas);
        }

        void Update()
        {
            if(!isActive) return;
            canvas.Clear(0,0,0,0);
            canvas.TextAlign(CenterMode.Center,CenterMode.Center);
            canvas.TextSize(30);
            canvas.Text("Space Hopper (Working title)", game.width / 2, 100);
            canvas.Text("Pull the lever back to start", game.width / 2, game.height - 100);
            if (Input.GetKeyUp(Key.SPACE))
            {
                SceneManager.instance.TryLoadNextScene();
            }
        }
        
    }
}