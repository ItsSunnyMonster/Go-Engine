using System;
using System.Collections.Generic;
using UnityEngine;

namespace SunnyMonster.GoEngine.Core.Utils
{
    public static class Algorithms
    {
        // ! THIS STUPID FUNCTION TOOK ME A WHOLE MORNING TO WRITE
        // ! JUST TO FIND OUT THAT I COULD HAVE USED THE "INEFFICIENT" DFS VERSION INSTEAD
        /// <summary>
        /// A flood fill algorithm for 2D arrays.
        /// </summary>
        /// <typeparam name="T">The type of items in the array</typeparam>
        /// <param name="array">The array</param>
        /// <param name="validator">A function that takes in an array item and returns true if this item should be part of the fill and false otherwise</param>
        /// <param name="callback">A function that takes in the coordinates of the currently processed item and returns false to stop the floodfill</param>
        /// <param name="startX">The x coordinate of the seed point</param>
        /// <param name="startY">The y coordinate of the seed point</param>
        /// <returns></returns>
        public static List<(int, int)> FloodFill2D<T>(T[,] array, Func<T, bool> validator, Func<int, int, bool> callback, int startX, int startY)
        {
            if (!validator(array[startX, startY])) return new List<(int, int)>();

            var result = new List<(int, int)>();
            var visited = new bool[array.GetLength(0), array.GetLength(1)]; // Array to track visited cells

            var stack = new Stack<(int, int)>();
            stack.Push((startX, startY));

            while (stack.Count != 0)
            {
                var (x, y) = stack.Pop();

                if (x < 0 || x >= array.GetLength(0) || y < 0 || y >= array.GetLength(1))
                    continue; // Skip out-of-bounds cells

                if (visited[x, y] || !validator(array[x, y]))
                    continue; // Skip already visited or invalid cells

                visited[x, y] = true; // Mark cell as visited

                result.Add((x, y));
                if (!callback(x, y)) return result;

                // Add neighboring cells to the stack
                stack.Push((x + 1, y));
                stack.Push((x - 1, y));
                stack.Push((x, y + 1));
                stack.Push((x, y - 1));
            }

            return result;
        }
    }
}