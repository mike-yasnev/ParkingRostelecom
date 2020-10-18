using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace ParkingServer
{
    public class DetectedObject
    {
        public DetectedObject()
        {

        }

        public string Type { get; set; }

        public long CenterX { get; set; }

        public long CenterY { get; set; }

        public long Width { get; set; }

        public long Height { get; set; }

        public long XMin { get; set; }

        public long YMin { get; set; }

        public long XMax { get; set; }

        public long YMax { get; set; }

    }


    public class Person : DetectedObject
    {

    }

    public class Car : DetectedObject
    {

    }

    public class ObjectDetectionFactory
    {
        //car, 0.995291531085968, (678, 443), (1133, 674)
 
        public static DetectedObject CreateObject(string row)
        {
            string[] s = row.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            DetectedObject result = null;
            switch(s[0].Trim())
            {
                case "car": result = new Car(); break;
                case "person": result = new Person(); break;
            }

            if(result == null)
            {
                return null;
            }

            result.XMin = Convert.ToInt32(s[2].Trim().Replace("(", "").Replace(")", ""));
            result.YMin = Convert.ToInt32(s[3].Trim().Replace("(", "").Replace(")", ""));
            result.XMax = Convert.ToInt32(s[4].Trim().Replace("(", "").Replace(")", ""));
            result.YMax = Convert.ToInt32(s[5].Trim().Replace("(", "").Replace(")", ""));

            result.CenterX = (result.XMin + result.XMax) / 2;
            result.CenterY = (result.YMin + result.YMax) / 2;

            result.Width = (result.XMax - result.XMin);
            result.Height = (result.YMax - result.YMin);

            return result;

        }
    }


}
