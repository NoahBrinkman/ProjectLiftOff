using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	private Player player;
	private Pivot objectOwner;
	public float gravity = .3f;

   	public int score = 0;
	EasyDraw scoreUI;
	public MyGame() : base(1366, 768, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		Sprite backGround = new Sprite("background-01.png", false, false);
		AddChild(backGround);

		Level level1 = new Level("testMapa.tmx");
		Scene scene1 = new Scene();
		SceneManager.instance.AddScene(level1);
		Level level2 = new Level("Map1.tmx");
		SceneManager.instance.AddScene(level2);

		objectOwner = new Pivot();
		player = new Player("square.png", 4,2,objectOwner,1,1,null);
		player.SetColor(0,255,0);
		player.SetOrigin(player.width / 2 + .1f, player.height / 2 +.1f);
		player.SetScaleXY(.5f,.5f);
		player.x = width / 2;
		player.y = height - 100;
		AddChild(objectOwner);
		//PlatformSpawner platformSpawner = new PlatformSpawner(2.5f,objectOwner,33,3);
		//AddChild(platformSpawner);
		objectOwner.AddChild(player);
		
		// Score UI 
		scoreUI = new EasyDraw(200, 30, false);
		scoreUI.SetXY(width - (scoreUI.width), 30);
		AddChild(scoreUI);

		
		SceneManager.instance.LoadScene(0);
	}


	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		scoreUI.Text("HIGHSCORE: " + score, true);
		objectOwner.Move(0, gravity);
	    gravity += 0.0045f * (float)Time.deltaTime/ 1000;
	   //Console.WriteLine(gravity);
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}