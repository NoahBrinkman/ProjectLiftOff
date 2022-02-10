using System;
using System.IO;
using System.IO.Ports;
using System.Threading;								// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	private Player player;
	private Pivot objectOwner;
	private float gravity = .3f;
	//public ArduinoInput ardInput;
	public MyGame() : base(1366, 768, false, true)		// Create a window that's 800x600 and NOT fullscreen
	{
		game.targetFps = 60;
		Level level1 = new Level("testMap.tmx");
		Scene scene1 = new Scene();
		SceneManager.instance.AddScene(level1);

		Sprite backGround = new Sprite("background-01.png", false, false);

		objectOwner = new Pivot();
		Sprite backGround = new Sprite("background-01.png", false, false);
		Platform p1 = new Platform("square.png");
		Platform p2 = new Platform("square.png");
		p1.SetOrigin(p1.width/2,p1.height/2);
		p2.SetOrigin(p2.width/2,p2.height/2);
		p1.SetScaleXY(2,2);
		p2.SetScaleXY(2,2);
		p1.x  = width / 2;
		p1.y = height - 100;
		p2.x = 200;
		p2.y = 400;
		objectOwner.AddChild(p1);
		objectOwner.AddChild(p2);
		Platform p3 = new Platform("square.png",1,1);
		Platform p4 = new Platform("square.png",1,1);
		p3.SetOrigin(p1.width/2,p1.height/2);
		p4.SetOrigin(p2.width/2,p2.height/2);
		p3.SetScaleXY(2,2);
		p4.SetScaleXY(2,2);
		p3.x  = width / 2;
		p3.y = height - 400;
		p4.x = 200;
		p4.y = 200;
		objectOwner.AddChild(p3);
		objectOwner.AddChild(p4);
		BoosterPlatform b1 = new BoosterPlatform("triangle.png", 1.6f);
		b1.rotation = -45;
		b1.alpha = .5f;
		b1.SetXY(width - 300, height /2);
		objectOwner.AddChild(b1);
		player = new Player("square.png", 4,2,objectOwner);
		player.SetColor(0,255,0);
		player.SetOrigin(player.width / 2 + .1f, player.height / 2 +.1f);
		player.SetScaleXY(.5f,.5f);
		player.x = width / 2;
		player.y = height - 100;
		AddChild(backGround);
		AddChild(objectOwner);
		PlatformSpawner platformSpawner = new PlatformSpawner(2.5f,objectOwner,10,3);
		AddChild(platformSpawner);
		objectOwner.AddChild(player);

		scoreUI = new EasyDraw(100, 30, false);
		scoreUI.SetXY(width - (scoreUI.width), 30);
		AddChild(scoreUI);


		//SceneManager.instance.LoadScene(0);
	}


	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		scoreUI.Text("Score: " + score, true);
		objectOwner.Move(0, gravity);
		gravity += 0.000045f * (1 + Mathf.Pow((float)Time.deltaTime/ 1000, gravity));
		//game.Destroy();
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{ 
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}