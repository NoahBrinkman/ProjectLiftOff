using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	private Player player;
	private Pivot objectOwner;
	private float gravity = .3f;
	public MyGame() : base(1366, 768, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		Level level1 = new Level("testMap.tmx");
		Scene scene1 = new Scene();
		scene1.AddChild(level1);
		SceneManager.instance.AddScene(scene1);


		objectOwner = new Pivot();
		Platform p1 = new Platform("square.png",1,1);
		Platform p2 = new Platform("square.png",1,1);
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
		player = new Player("square.png", 4,2,objectOwner,1,1,null);
		player.SetColor(0,255,0);
		player.SetOrigin(player.width / 2 + .1f, player.height / 2 +.1f);
		player.SetScaleXY(.5f,.5f);
		player.x = width / 2;
		player.y = height - 100;
		AddChild(objectOwner);
		PlatformSpawner platformSpawner = new PlatformSpawner(2.5f,objectOwner,33,3);
		AddChild(platformSpawner);
		objectOwner.AddChild(player);

		SceneManager.instance.LoadScene(0);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		objectOwner.Move(0, gravity);
		gravity += 0.0045f * (float)Time.deltaTime/ 1000;
			Console.WriteLine(gravity);
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}