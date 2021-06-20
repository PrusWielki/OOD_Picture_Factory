using System;
using System.Collections.Generic;

namespace PictureProduction
{
    class Program
    {
        private readonly static Order[] orders =
        {
            new Order("circle", "red", "Hello", "spacing"),
            new Order("square", "green", "HelloWorld", "spacing"),
            new Order("triangle", "blue", "ChainIsBeauty", "spacing"),

            new Order("circle", "red", "Hello", "uppercase"),
            new Order("square", "green", "HelloWorld", "uppercase"),
            new Order("triangle", "blue", "ChainIsBeauty", "uppercase"),

            new Order("circle", "red", "Hello", "lowercase"),
            new Order("square", "yellow", "HelloWorld", "lowercase"),
            new Order("hash", "red", "ChainIsBeauty", "uppercase"),

            new Order("", "green", "ChainIsBeauty", "uppercase"), //invalid order
            new Order("star", "1234", "ChainIsBeauty", "uppercase"), //invalid order
            new Order("star", "green", null, "uppercase"), //invalid order
        };
        
        static void ProducePictures(IEnumerable<Order> orders,IMachine machine)
        {
            List<Picture> pictures = new List<Picture>();
                      
            var enumer = orders.GetEnumerator();
            while (enumer.MoveNext()){
                Picture temppic = new Picture();
                machine.Handle(enumer.Current, temppic);
                pictures.Add(temppic);
            }
            
        }

        static void Main(string[] args)
        {
            Console.WriteLine("--- Simple Production Line ---");
            var simpleprduction = new OOD_Retake_2.Validator();
            simpleprduction
                 .SetNext(new OOD_Retake_2.ColorMachine("red"))
                 .SetNext(new OOD_Retake_2.SignMachine(""))
                 .SetNext(new OOD_Retake_2.FrameMachine("circle"))
                 .SetNext(new OOD_Retake_2.FrameMachine("circle"))
                 .SetNext(new OOD_Retake_2.FrameMachine("square"))
                 .SetNext(new OOD_Retake_2.FrameMachine("square"))
                 .SetNext(new OOD_Retake_2.Printer());
            ProducePictures(orders,simpleprduction);

            Console.WriteLine();

            Console.WriteLine("--- Complex Production Line ---");
            var complexproduction = new OOD_Retake_2.Validator();
            complexproduction
                .SetNext(new OOD_Retake_2.ColorMachine("red"))
                .SetNext(new OOD_Retake_2.ColorMachine("green"))
                .SetNext(new OOD_Retake_2.ColorMachine("blue"))
                .SetNext(new OOD_Retake_2.SignMachine("spacing"))
                .SetNext(new OOD_Retake_2.SignMachine("uppercase"))
                .SetNext(new OOD_Retake_2.ComplexFrameMachine("circle"))
                .SetNext(new OOD_Retake_2.ComplexFrameMachine("square"))
                .SetNext(new OOD_Retake_2.ComplexFrameMachine("triangle"))
                .SetNext(new OOD_Retake_2.Printer());
            ProducePictures(orders,complexproduction);
        }
    }
}
