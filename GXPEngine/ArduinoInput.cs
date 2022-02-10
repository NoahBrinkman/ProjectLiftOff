using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;

namespace GXPEngine
{
    public class ArduinoInput
    {
        public int yValue { get; private set; }
        public int xValue { get; private set; }
        private SerialPort port;
        private Game game;
        private int frameCount = 0;
        public ArduinoInput(Game game)
        {
            //game.OnBeforeStep += Step;
           /* port = new SerialPort();
            port.PortName = "COM3";
            port.BaudRate = 1;
            port.RtsEnable = true;

            port.DtrEnable = true;
            port.Open();
            //port.Open();*/
        }

        private void Step()
        {
            port.Open();
            string outPut = port.ReadExisting();
            port.Close();
            string[] LastTwoInputs = outPut.Split('\n');
            string[] finalInputs = LastTwoInputs.ToList().Where(x => x != "").ToArray();
            //xValue = int.Parse(LastTwoInputs[0]);
            Console.WriteLine(yValue);
            yValue = int.Parse(finalInputs[finalInputs.Length - 1]);
        }
        
    }
}