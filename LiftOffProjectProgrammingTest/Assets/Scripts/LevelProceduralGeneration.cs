/*
MIT License

Copyright (c) 2021 Michele Maione - mikymaione@hotmail.it

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
*/

using System;
using UnityEngine;
using Random = System.Random;

public class LevelProceduralGeneration : MonoBehaviour
{

    [Min(1)]
    public int TerrainDepth = 1;

    [Min(1)]
    public int TerrainLength = 1;

    [Min(1)]
    public int MountainMaxHeigth = 1;


    private readonly Random _rnd = new Random(DateTime.Now.Second);


    void Start()
    {
        //w = 2h - 1
        var mountainMaxWidth = 2 * MountainMaxHeigth - 1;

        int x, y;
        var spaceAvailable = TerrainLength;
        var map = new bool[TerrainLength, TerrainDepth + MountainMaxHeigth];

        // fill the ground map
        for (x = 0; x < TerrainLength; x++)
            for (y = 0; y < TerrainDepth; y++)
                map[x, y] = true;

        // generate the mountains map
        x = 0;
        y = TerrainDepth;
        while (spaceAvailable > 1)
        {
            var mountainX = _rnd.Next(1, mountainMaxWidth + 1);
            var mountainH = _rnd.Next(1, MountainMaxHeigth + 1);
            var mountainW = 2 * mountainH - 1;

            var spaceRequired = mountainX + mountainW;

            if (spaceAvailable < spaceRequired)
                break;

            spaceAvailable -= spaceRequired;

            GenerateMountain(ref map, x + mountainX, y, mountainH);

            x += spaceRequired;
        }

        // place the cube from the map
        for (x = 0; x < TerrainLength; x++)
            for (y = 0; y < TerrainDepth + MountainMaxHeigth; y++)
                if (map[x, y])
                {
                    var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = new Vector3(x, y, 0);
                    cube.transform.tag = "Ground";
                }
    }

    private static void GenerateMountain(ref bool[,] map, int x, int y, int h)
    {
        var step = 0;

        for (var j = 0; j < h; j++)
        {
            var w = 2 * (h - j) - 1;

            for (var i = 0; i < w; i++)
                map[x + i + step, y + j] = true;

            step++;
        }
    }

}