using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleRain
{
    class ConsoleLayer
    {
        private char[] weather_options = new char[] {'/', '|', '\\'};
        private int weather_direction;
        private int weather_interval;
        private int console_height, console_width;
        private int[] row_pixels;
        private List<int[]> layer_rows;
        Random r =  new Random();
        public ConsoleLayer()
        {
            console_height = Console.WindowHeight;
            console_width = Console.WindowWidth;
            row_pixels = new int[console_width];
            layer_rows = new List<int[]>(console_height);
            for(int _rows = 0; _rows < console_height; _rows++)
            {
                layer_rows.Add(GenerateRow());
            }
            weather_interval = 10;
            weather_direction = 1;
        }

        public void Update()
        {
            AddRow();
            if(weather_interval < 1)
            {
                PickDirection();
                weather_interval = r.Next(10, 30);
            }
            weather_interval--;
            ApplyWeather();
            //every x seconds change direction to a random direction, could also be a random interval some time after x but before y etc.
        }

        public string Read()
        {
            string _output = "";
            for (int _row = 0; _row < layer_rows.Count; _row++)
            {
                _output += "\n";
                for (int _column = 0; _column < console_width; _column++)
                {
                    if (layer_rows[_row][_column] == 0) // no rain
                        _output += " ";
                    else if (_row < layer_rows.Count - 1) //rain drop
                        _output += weather_options[weather_direction];
                    else
                        _output += "*"; //splash
                }
            }
            return _output;
        }

        private void AddRow()
        {
            // make room for newly generated row
            layer_rows.RemoveAt(layer_rows.Count - 1);
            // add a new row
            layer_rows.Insert(0, GenerateRow());
        }

        private int[] GenerateRow()
        {
            int[] new_row = new int[console_width];
            new_row[r.Next(console_width - 1)] = 1;
            return new_row;
        }


        private void PickDirection()
        {
            int i = r.Next(3);
            weather_direction += i - 1;
            if (weather_direction < 0)
                weather_direction = 0;
            else if (weather_direction > weather_options.Length - 1)
                weather_direction = weather_options.Length - 1;
        }

        private void ApplyWeather()
        {
            if (weather_direction == 1)//1 = no weather
                return;
            for (int _row = 0; _row < layer_rows.Count; _row++)
            {
                if (weather_direction == 0)
                    RotateLeft(layer_rows[_row]);
                else
                    RotateRight(layer_rows[_row]);
            }
        }

        private int[] RotateLeft(int[] array)
        {
            int first = array[0];
            Array.Copy(array, 1, array, 0, array.Length - 1);
            array[array.Length - 1] = first;
            return array;
        }

        private int[] RotateRight(int[] array)
        {
            int last = array[array.Length - 1];
            Array.Copy(array, 0, array, 1, array.Length - 1);
            array[0] = last;
            return array;
        }
    }
}