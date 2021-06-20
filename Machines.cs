using PictureProduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOD_Retake_2
{
    class ColorMachine:PictureProduction.AbstractMachine
    {
        
        string color;
        public ColorMachine(string color)
        {
            this.color = color;
        }
        public override void Handle(Order order, IPicture picture)
        {
            //Console.WriteLine(2);

            if (order.Color == color)
                picture.Color = order.Color;
            else
                picture.Color = String.Empty;

            base.Handle(order, picture);
        }
    }
    class Validator : PictureProduction.AbstractMachine
    {

        
        public override void Handle(Order order, IPicture picture)
        {
            //Console.WriteLine(1);
            bool valid = true;
            if (order.Color == null || order.Operation == null || order.Shape == null || order.Text == null)
                valid = false;
            else if (order.Color == String.Empty || order.Operation == String.Empty || order.Shape == String.Empty || order.Text == String.Empty)
                valid = false;
            else if (Regex.Matches(order.Color, @"[a-zA-Z]").Count!=order.Color.Length||
                Regex.Matches(order.Operation, @"[a-zA-Z]").Count != order.Operation.Length||
                Regex.Matches(order.Shape, @"[a-zA-Z]").Count != order.Shape.Length||
                Regex.Matches(order.Text, @"[a-zA-Z]").Count != order.Text.Length)
                valid = false;
            if (valid)
                base.Handle(order, picture);
            else
                Console.WriteLine("Error: Invalid order!");
        }
    }
    class Printer : PictureProduction.AbstractMachine
    {
       
      
        public override void Handle(Order order, IPicture picture)
        {
            //Console.WriteLine(5);
            if (picture.LeftFrame == null || picture.RightFrame == null || picture.LeftFrame == String.Empty || picture.RightFrame == String.Empty)
            {

                Console.WriteLine("Error: Cannot create picture!");
                return;
            }
            picture.Print();
            base.Handle(order, picture);
        }
    }
    class SignMachine : PictureProduction.AbstractMachine
    {
       
        string operation;
        public SignMachine(string operation)
        {
            this.operation = operation;
        }
        public override void Handle(Order order, IPicture picture)
        {
            //Console.WriteLine(3);
            if (picture.Text==String.Empty|| picture.Text ==null)
            picture.Text = order.Text;
            if(order.Operation==operation)
            picture.Text = TextOperation.operation(operation, order.Text);
            base.Handle(order, picture);
        }
    }
    class FrameMachine : PictureProduction.AbstractMachine
    {
        string shape;
        public FrameMachine(string operation)
        {
            this.shape = operation;
        }
        public override void Handle(Order order, IPicture picture)
        {
            //Console.WriteLine(4);
            if (order.Shape != shape)
            {             
                base.Handle(order, picture);
                return;
            }
            if (picture.Color == null || picture.Text == null)
            {
                
                Console.WriteLine("Error: Cannot create picture!");
                return;
                //base.Handle(order, picture);
            }
           
            else
            {
                picture.LeftFrame += Framing.LeftFrame(order.Shape);
                picture.RightFrame += Framing.RightFrame(order.Shape);
                //base.Handle(order, picture);
            }
            base.Handle(order, picture);
        }
    }
    class ComplexFrameMachine : PictureProduction.AbstractMachine
    {
        string shape;
        public ComplexFrameMachine(string operation)
        {
            this.shape = operation;
        }
        public override void Handle(Order order, IPicture picture)
        {
            //Console.WriteLine(6);
            if (order.Shape != shape)
            {
                base.Handle(order, picture);
                return;
            }
            if (picture.Color == null || picture.Text == null)
            {

                Console.WriteLine("Error: Cannot create picture!");
                return;
                //base.Handle(order, picture);
            }
            else if (order.Operation == "spacing"&& picture.Color == String.Empty)
            {
                picture.LeftFrame += Framing.LeftFrame(order.Shape) + Framing.LeftFrame(order.Shape) + Framing.LeftFrame(order.Shape);
                picture.RightFrame += Framing.RightFrame(order.Shape) + Framing.RightFrame(order.Shape) + Framing.RightFrame(order.Shape);

            }
            else if(picture.Color==String.Empty)
            {
                picture.LeftFrame += Framing.LeftFrame(order.Shape)+ Framing.LeftFrame(order.Shape);
                picture.RightFrame += Framing.RightFrame(order.Shape)+ Framing.RightFrame(order.Shape);
                //base.Handle(order, picture);
            }
            else if( order.Operation=="spacing")
            {
                picture.LeftFrame += Framing.LeftFrame(order.Shape) + Framing.LeftFrame(order.Shape);
                picture.RightFrame += Framing.RightFrame(order.Shape) + Framing.RightFrame(order.Shape);

            }
            else
            {
                picture.LeftFrame += Framing.LeftFrame(order.Shape);
                picture.RightFrame += Framing.RightFrame(order.Shape);
            }
            base.Handle(order, picture);
        }
    }
    static class TextOperation
    {
        
        static public string operation(string operation, string text)
        {
            if (operation == "spacing")
            {
                string temp= text.Aggregate(string.Empty, (c, i) => c + i + ' ');

                return temp.Remove(temp.Length - 1, 1);
            }
            else if (operation == "uppercase")
            {
                return text.ToUpper();
            }
            else if (operation == "lowercase")
            {
                return text.ToLower();
            }
            else
                return text;
        }


    }
    static class Framing
    {

        static public string LeftFrame(string shape)
        {

            if (shape == "circle")
            {
               
                return "(";

            }
            else if (shape == "square")
            {
                return "[";
            }
            else if (shape == "triangle")
                return "<";
            else
            {

                return String.Empty;
            }
        }
        static public string RightFrame(string shape)
        {

            if (shape == "circle")
            {
                return ")";

            }
            else if (shape == "square")
            {
                return "]";
            }
            else if (shape == "triangle")
            {
                return ">";
            }
            else
            {

                return String.Empty;
            }
        }

    }

}
