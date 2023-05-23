using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RectangleCoordinatesApi.Entities
{
    public class Rectangle
    {
        [Key]
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
    public class CoordinateRequest
    {
        public List<Coordinate> Coordinates { get; set; }
    }

    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
