using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalGeneration : MonoBehaviour
{
    public int textureWidth = 512;
    public int textureHeight = 512;
    public float zoom = 2.0f;
    public Vector2 center = new Vector2(-0.5f, 0f);

    void Start()
    {
        Texture2D mandelbrotTexture = new Texture2D(textureWidth, textureHeight);

        for (int y = 0; y < textureHeight; y++)
        {
            for (int x = 0; x < textureWidth; x++)
            {
                float complexX = Map(x, 0, textureWidth, center.x - zoom, center.x + zoom);
                float complexY = Map(y, 0, textureHeight, center.y - zoom, center.y + zoom);

                int iterations = MandelbrotIterations(new Complex(complexX, complexY));

                float t = (float)iterations / 100.0f;
                Color color = new Color(t, t * 0.5f, t * 0.8f); // Добавил красивый градиент

                mandelbrotTexture.SetPixel(x, y, color);
            }
        }

        mandelbrotTexture.Apply();
        GetComponent<Renderer>().material.mainTexture = mandelbrotTexture;
    }

    float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }

    int MandelbrotIterations(Complex c)
    {
        Complex z = Complex.Zero;
        int iterations = 0;

        while (z.MagnitudeSquared() < 4.0f && iterations < 100)
        {
            z = z * z + c;
            iterations++;
        }

        return iterations;
    }
}

public struct Complex
{
    public float Real;
    public float Imaginary;

    public Complex(float real, float imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static Complex Zero { get { return new Complex(0, 0); } }

    public static Complex operator *(Complex a, Complex b)
    {
        return new Complex(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
    }

    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    public float MagnitudeSquared()
    {
        return Real * Real + Imaginary * Imaginary;
    }
}
