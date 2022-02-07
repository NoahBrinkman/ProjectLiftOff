using System;									// System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;							// System.Drawing contains drawing tools such as Color definitions

public class MyGame : Game
{
	private Player player;
	private Pivot objectOwner;
	
	public MyGame() : base(800, 600, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		objectOwner = new Pivot();
		Platform p1 = new Platform("circle.png");
		Platform p2 = new Platform("circle.png");
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
		Platform p3 = new Platform("circle.png");
		Platform p4 = new Platform("circle.png");
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
		player = new Player("triangle.png", 4,2);
		player.SetOrigin(player.width / 2, player.height / 2);
		player.SetScaleXY(.5f,.5f);
		player.x = width / 2;
		player.y = height - 100;
		AddChild(objectOwner);
		objectOwner.AddChild(player);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update()
	{
		objectOwner.Move(0, .2f);
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}