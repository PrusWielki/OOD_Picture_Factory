using System;

namespace PictureProduction
{
    interface IPicture
    {
        string LeftFrame { get; set; }
        string RightFrame { get; set; }
        string Color { get; set; }
        string Text { get; set; }
        
        void Print();
    }
    public class Picture : IPicture
    {
        public string LeftFrame { get; set; }
        public string RightFrame { get; set; }
        public string Color { get; set; }
        public string Text { get; set; }

        public void Print()
        {
            Console.WriteLine($"{LeftFrame}{Color}{RightFrame} {Text} {LeftFrame}{Color}{RightFrame}");
        }
    }
}
