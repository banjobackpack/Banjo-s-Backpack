﻿// Decompiled with JetBrains decompiler
// Type: BanjoKazooieLevelEditor.ImageHandler
// Assembly: BanjoKazooieLevelEditor, Version=2.0.19.0, Culture=neutral, PublicKeyToken=null
// MVID: 9E4E8A9F-6E2F-4B24-B56C-5C2BF24BF813
// Assembly location: C:\Users\runem\Desktop\BanjosBackpack\BB.exe

using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace BanjoKazooieLevelEditor
{
  public class ImageHandler
  {
    public static Bitmap[] LoadGIFFrames(string gifFile, int requiredFrameCount)
    {
      Image image = Image.FromFile(gifFile);
      FrameDimension dimension = new FrameDimension(image.FrameDimensionsList[0]);
      int frameCount = image.GetFrameCount(dimension);
      Bitmap[] bitmapArray = new Bitmap[requiredFrameCount];
      for (int frameIndex = 0; frameIndex < frameCount && frameIndex < requiredFrameCount; ++frameIndex)
      {
        image.SelectActiveFrame(dimension, frameIndex);
        bitmapArray[frameIndex] = new Bitmap(image.Size.Width, image.Size.Height);
        Graphics.FromImage((Image) bitmapArray[frameIndex]).DrawImage(image, new Point(0, 0));
        Bitmap bitmap = new Bitmap(32, 32, PixelFormat.Format32bppArgb);
        using (Graphics graphics = Graphics.FromImage((Image) bitmap))
          graphics.DrawImage((Image) bitmapArray[frameIndex], new Rectangle(0, 0, 32, 32));
        bitmapArray[frameIndex] = bitmap;
        bitmapArray[frameIndex] = ImageHandler.ConvertTo256Colour(bitmapArray[frameIndex]);
      }
      for (int index = 0; frameCount + index < requiredFrameCount; ++index)
        bitmapArray[frameCount + index] = bitmapArray[index];
      return bitmapArray;
    }

    public static byte[] ConvertFramesToBKSprite(Bitmap[] frames, byte animationByte)
    {
      List<byte> byteList = new List<byte>();
      byteList.Add((byte) (((IEnumerable<Bitmap>) frames).Count<Bitmap>() >> 8));
      byteList.Add((byte) ((IEnumerable<Bitmap>) frames).Count<Bitmap>());
      byteList.Add((byte) 4);
      byteList.Add((byte) 0);
      byteList.Add(byte.MaxValue);
      byteList.Add(byte.MaxValue);
      byteList.Add(byte.MaxValue);
      byteList.Add(byte.MaxValue);
      byteList.Add((byte) 0);
      byteList.Add((byte) 190);
      byteList.Add((byte) 0);
      byteList.Add((byte) 190);
      if (animationByte > (byte) 0)
        byteList.Add((byte) 38);
      else
        byteList.Add((byte) 0);
      byteList.AddRange((IEnumerable<byte>) new byte[3]);
      int num = 0;
      if (((IEnumerable<Bitmap>) frames).Count<Bitmap>() % 2 != 0)
        num += 4;
      foreach (Bitmap frame in frames)
      {
        byteList.Add((byte) (num >> 24));
        byteList.Add((byte) (num >> 16));
        byteList.Add((byte) (num >> 8));
        byteList.Add((byte) num);
        num += 32 + frame.Width * frame.Height * 2;
      }
      if (((IEnumerable<Bitmap>) frames).Count<Bitmap>() % 2 != 0)
        byteList.AddRange((IEnumerable<byte>) new byte[4]
        {
          (byte) 170,
          (byte) 170,
          (byte) 170,
          (byte) 170
        });
      foreach (Bitmap frame in frames)
      {
        byteList.AddRange((IEnumerable<byte>) new byte[4]);
        byteList.Add((byte) (frame.Width >> 8));
        byteList.Add((byte) frame.Width);
        byteList.Add((byte) (frame.Height >> 8));
        byteList.Add((byte) frame.Height);
        byteList.AddRange((IEnumerable<byte>) new byte[16]
        {
          (byte) 0,
          (byte) 1,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 2,
          (byte) 0,
          (byte) 1,
          (byte) 0,
          (byte) 30,
          (byte) 0,
          (byte) 23,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0
        });
        byteList.Add((byte) (frame.Width >> 8));
        byteList.Add((byte) frame.Width);
        byteList.Add((byte) (frame.Height >> 8));
        byteList.Add((byte) frame.Height);
        byteList.AddRange((IEnumerable<byte>) new byte[4]
        {
          (byte) 170,
          (byte) 170,
          (byte) 170,
          (byte) 170
        });
        byteList.AddRange((IEnumerable<byte>) ImageHandler.ConvertImageToRGBA5551(frame));
      }
      return byteList.ToArray();
    }

    public static Bitmap ConvertSpriteToImage(ref byte[] file)
    {
      try
      {
        short num1 = (short) (((int) file[0] << 8) + (int) file[1]);
        List<int> intList = new List<int>();
        for (int index = 16; index < 16 + (int) num1 * 4; index += 4)
          intList.Add(((int) file[index] << 24) + ((int) file[index + 1] << 16) + ((int) file[index + 2] << 8) + (int) file[index + 3]);
        int num2 = 16 + (int) num1 * 4;
        int num3 = (int) file[3];
        int sourceIndex = num2 + 24;
        int num4 = sourceIndex;
        int num5 = (num3 != 4 ? num4 + 32 : num4 + 512) + 8;
        int width = ((int) file[num2 + 4] << 8) + (int) file[num2 + 5];
        int height = ((int) file[num2 + 6] << 8) + (int) file[num2 + 7];
        byte[] palette = new byte[512];
        Array.Copy((Array) file, sourceIndex, (Array) palette, 0, 512);
        List<byte[]> numArrayList = ImageHandler.LoadPalette(ref palette, 512);
        byte[] source = new byte[width * height * 4];
        int num6 = width * height;
        int index1 = 0;
        switch (num3)
        {
          case 4:
            for (int index2 = 0; index2 < num6; ++index2)
            {
              int index3 = (int) file[num5 + index2];
              source[index1] = numArrayList[index3][2];
              int index4 = index1 + 1;
              source[index4] = numArrayList[index3][1];
              int index5 = index4 + 1;
              source[index5] = numArrayList[index3][0];
              int index6 = index5 + 1;
              source[index6] = numArrayList[index3][3];
              index1 = index6 + 1;
            }
            goto case 1024;
          case 1024:
            Rectangle rect = new Rectangle(0, 0, width, height);
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            IntPtr scan0 = bitmapdata.Scan0;
            Marshal.Copy(source, 0, scan0, source.Length);
            bitmap.UnlockBits(bitmapdata);
            return bitmap;
          default:
            int num7 = num6 / 2;
            for (int index2 = 0; index2 < num7; ++index2)
            {
              int index3 = (int) file[num5 + index2] >> 4;
              source[index1] = numArrayList[index3][2];
              int index4 = index1 + 1;
              source[index4] = numArrayList[index3][1];
              int index5 = index4 + 1;
              source[index5] = numArrayList[index3][0];
              int index6 = index5 + 1;
              source[index6] = numArrayList[index3][3];
              int index7 = index6 + 1;
              int index8 = (int) file[num5 + index2] & 15;
              source[index7] = numArrayList[index8][2];
              int index9 = index7 + 1;
              source[index9] = numArrayList[index8][1];
              int index10 = index9 + 1;
              source[index10] = numArrayList[index8][0];
              int index11 = index10 + 1;
              source[index11] = numArrayList[index8][3];
              index1 = index11 + 1;
            }
            goto case 1024;
        }
      }
      catch
      {
      }
      return new Bitmap(1, 1);
    }

    public static void ConvertSprite(ref byte[] file, Sprite sprite)
    {
      try
      {
        sprite.animationByte = file[12];
        int num1 = (int) file[6];
        int num2 = (int) file[7];
        short num3 = (short) (((int) file[0] << 8) + (int) file[1]);
        List<int> intList = new List<int>();
        int num4 = 16 + (int) num3 * 4;
        for (int index = 16; index < 16 + (int) num3 * 4; index += 4)
          intList.Add(((int) file[index] << 24) + ((int) file[index + 1] << 16) + ((int) file[index + 2] << 8) + (int) file[index + 3]);
        int num5 = ((int) file[2] << 8) + (int) file[3];
        int num6 = ((int) file[num4 + intList[0] + 8] << 8) + (int) file[num4 + intList[0] + 9];
        sprite.imagesPerFrame = num6;
        foreach (int num7 in intList)
        {
          int num8 = ((int) file[num4 + num7 + 4] << 8) + (int) file[num4 + num7 + 5];
          int num9 = ((int) file[num4 + num7 + 6] << 8) + (int) file[num4 + num7 + 7];
          byte[] numArray = new byte[num8 * num9 * 4];
          int num10 = num8 * num9;
          int index1 = 0;
          sprite.textureFormat = (SpriteTextureFormat) num5;
          if (num5 == 1024)
          {
            int num11 = num4 + num7;
            int width = ((int) file[num11 + 24] << 8) + (int) file[num11 + 25];
            int height = ((int) file[num11 + 26] << 8) + (int) file[num11 + 27];
            int num12 = num11 + 32;
            int num13 = width * height;
            byte[] source = new byte[width * height * 4];
            for (int index2 = 0; index2 < num13 * 2; index2 += 2)
            {
              bool alpha = false;
              byte[] rgbA8888 = ImageHandler.RGBA5551ToRGBA8888(new byte[2]
              {
                file[num12 + index2],
                file[num12 + index2 + 1]
              }, ref alpha);
              source[index1] = rgbA8888[2];
              int index3 = index1 + 1;
              source[index3] = rgbA8888[1];
              int index4 = index3 + 1;
              source[index4] = rgbA8888[0];
              int index5 = index4 + 1;
              source[index5] = rgbA8888[3];
              index1 = index5 + 1;
            }
            Rectangle rect = new Rectangle(0, 0, width, height);
            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            IntPtr scan0 = bitmapdata.Scan0;
            Marshal.Copy(source, 0, scan0, source.Length);
            bitmap.UnlockBits(bitmapdata);
            sprite.frames.Add(bitmap);
          }
          else
          {
            int width = 0;
            int height = 0;
            int sourceIndex = num4 + num7 + 24;
            int num11 = sourceIndex;
            int num12 = num5 != 4 ? num11 + 32 : num11 + 512;
            for (int index2 = 0; index2 < sprite.imagesPerFrame; ++index2)
            {
              int index3 = 0;
              int num13 = num5 != 4 ? num12 + width * height / 2 : num12 + width * height;
              width = ((int) file[num13 + 4] << 8) + (int) file[num13 + 5];
              height = ((int) file[num13 + 6] << 8) + (int) file[num13 + 7];
              byte[] source = new byte[width * height * 4];
              int num14 = width * height;
              num12 = num13 + 8;
              if (num5 == 4)
              {
                byte[] palette = new byte[512];
                Array.Copy((Array) file, sourceIndex, (Array) palette, 0, 512);
                List<byte[]> numArrayList = ImageHandler.LoadPalette(ref palette, 512);
                for (int index4 = 0; index4 < num14; ++index4)
                {
                  int index5 = (int) file[num12 + index4];
                  source[index3] = numArrayList[index5][2];
                  int index6 = index3 + 1;
                  source[index6] = numArrayList[index5][1];
                  int index7 = index6 + 1;
                  source[index7] = numArrayList[index5][0];
                  int index8 = index7 + 1;
                  source[index8] = numArrayList[index5][3];
                  index3 = index8 + 1;
                }
              }
              else
              {
                byte[] palette = new byte[32];
                Array.Copy((Array) file, sourceIndex, (Array) palette, 0, 32);
                List<byte[]> numArrayList = ImageHandler.LoadPalette(ref palette, 32);
                int num15 = num14 / 2;
                for (int index4 = 0; index4 < num15; ++index4)
                {
                  int index5 = (int) file[num12 + index4] >> 4;
                  source[index3] = numArrayList[index5][2];
                  int index6 = index3 + 1;
                  source[index6] = numArrayList[index5][1];
                  int index7 = index6 + 1;
                  source[index7] = numArrayList[index5][0];
                  int index8 = index7 + 1;
                  source[index8] = numArrayList[index5][3];
                  int index9 = index8 + 1;
                  int index10 = (int) file[num12 + index4] & 15;
                  source[index9] = numArrayList[index10][2];
                  int index11 = index9 + 1;
                  source[index11] = numArrayList[index10][1];
                  int index12 = index11 + 1;
                  source[index12] = numArrayList[index10][0];
                  int index13 = index12 + 1;
                  source[index13] = numArrayList[index10][3];
                  index3 = index13 + 1;
                }
              }
              Rectangle rect = new Rectangle(0, 0, width, height);
              Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
              BitmapData bitmapdata = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
              IntPtr scan0 = bitmapdata.Scan0;
              Marshal.Copy(source, 0, scan0, source.Length);
              bitmap.UnlockBits(bitmapdata);
              sprite.frames.Add(bitmap);
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private static List<byte[]> LoadPalette(ref byte[] palette, int palSize)
    {
      byte[] numArray1 = new byte[palSize / 2];
      byte[] numArray2 = new byte[palSize / 2];
      byte[] numArray3 = new byte[palSize / 2];
      byte[] numArray4 = new byte[palSize / 2];
      List<byte[]> numArrayList = new List<byte[]>();
      int index1 = 0;
      int index2 = 0;
      while (index1 < palSize / 2)
      {
        numArray1[index1] = palette[index2];
        numArray1[index1] >>= 3;
        numArray1[index1] *= (byte) 8;
        ushort num = (ushort) ((uint) (ushort) ((uint) (ushort) ((uint) (ushort) ((uint) palette[index2] * 256U + (uint) palette[index2 + 1]) << 5) >> 3) >> 8);
        numArray2[index1] = (byte) num;
        numArray2[index1] *= (byte) 8;
        numArray3[index1] = palette[index2 + 1];
        numArray3[index1] <<= 2;
        numArray3[index1] >>= 3;
        numArray3[index1] *= (byte) 8;
        numArray4[index1] = palette[index2 + 1];
        numArray4[index1] <<= 7;
        numArray4[index1] >>= 7;
        numArray4[index1] = numArray4[index1] != (byte) 1 ? (byte) 0 : byte.MaxValue;
        ++index1;
        index2 += 2;
      }
      for (int index3 = 0; index3 < numArray1.Length; ++index3)
      {
        byte[] numArray5 = new byte[4]
        {
          numArray1[index3],
          numArray2[index3],
          numArray3[index3],
          numArray4[index3]
        };
        numArrayList.Add(numArray5);
      }
      return numArrayList;
    }

    public static void CorrectSize(ref Bitmap bitmap)
    {
      int width1 = bitmap.Width;
      int height1 = bitmap.Height;
      int width2 = bitmap.Width;
      int height2 = bitmap.Height;
      if (bitmap.Width != 16 && bitmap.Width != 32 && bitmap.Width != 64)
      {
        int num1 = Math.Abs(16 - bitmap.Width);
        int num2 = Math.Abs(32 - bitmap.Width);
        int num3 = Math.Abs(64 - bitmap.Width);
        if (num1 <= num2 && num1 <= num3)
          width2 = 16;
        if (num2 <= num1 && num2 <= num3)
          width2 = 32;
        if (num3 <= num2 && num3 <= num1)
          width2 = 64;
      }
      if (bitmap.Height != 16 && bitmap.Height != 32 && bitmap.Height != 64)
      {
        int num1 = Math.Abs(16 - bitmap.Height);
        int num2 = Math.Abs(32 - bitmap.Height);
        int num3 = Math.Abs(64 - bitmap.Height);
        if (num1 <= num2 && num1 <= num3)
          height2 = 16;
        if (num2 <= num1 && num2 <= num3)
          height2 = 32;
        if (num3 <= num2 && num3 <= num1)
          height2 = 64;
      }
      int num = height2;
      if (height1 == num && width1 == width2)
        return;
      Bitmap bitmap1 = new Bitmap(width2, height2, PixelFormat.Format32bppArgb);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap1))
        graphics.DrawImage((Image) bitmap, new Rectangle(0, 0, width2, height2));
      bitmap = bitmap1;
    }

    public static List<Color> GetUniqueColors(ref Bitmap image)
    {
      int width = image.Width;
      int height = image.Height;
      int index1 = 0;
      Color[] colorArray = new Color[width * height];
      for (int y = 0; y < height; ++y)
      {
        for (int x = 0; x < width; ++x)
        {
          colorArray[index1] = image.GetPixel(x, y);
          ++index1;
        }
      }
      List<Color> colorList = new List<Color>();
      for (int index2 = 0; index2 < colorArray.Length; ++index2)
      {
        if (!colorList.Contains(colorArray[index2]))
          colorList.Add(colorArray[index2]);
      }
      return colorList;
    }

    public static Bitmap ConvertTo256Colour(Bitmap image)
    {
      MagickImage magickImage = new MagickImage();
      magickImage.Read(image);
      magickImage.Quantize(new QuantizeSettings()
      {
        Colors = 256
      });
      return magickImage.ToBitmap();
    }

    public static Bitmap ConvertTo16Colour(Bitmap image)
    {
      MagickImage magickImage = new MagickImage();
      magickImage.Read(image);
      magickImage.Quantize(new QuantizeSettings()
      {
        Colors = 16
      });
      return magickImage.ToBitmap();
    }

    public static byte[] ConvertImageToRGBA8888(Bitmap image)
    {
      int width = image.Width;
      int height = image.Height;
      byte[] numArray = new byte[width * height * 4];
      int index = 0;
      for (int y = 0; y < height; ++y)
      {
        for (int x = 0; x < width; ++x)
        {
          Color pixel = image.GetPixel(x, y);
          numArray[index] = pixel.R;
          numArray[index + 1] = pixel.G;
          numArray[index + 2] = pixel.B;
          numArray[index + 3] = pixel.A;
          index += 4;
        }
      }
      return numArray;
    }

    public static byte[] ConvertImageToRGBA5551(Bitmap image)
    {
      int width = image.Width;
      int height = image.Height;
      byte[] numArray = new byte[width * height * 2];
      int index = 0;
      bool alpha = false;
      for (int y = 0; y < height; ++y)
      {
        for (int x = 0; x < width; ++x)
        {
          Color pixel = image.GetPixel(x, y);
          byte[] rgbA5551 = ImageHandler.RGBA8888ToRGBA5551(new byte[4]
          {
            pixel.R,
            pixel.G,
            pixel.B,
            pixel.A
          }, ref alpha);
          numArray[index] = rgbA5551[0];
          numArray[index + 1] = rgbA5551[1];
          index += 2;
        }
      }
      return numArray;
    }

    public static byte[] ConvertImageToCI4(Bitmap image, ref bool alpha)
    {
      int width = image.Width;
      int height = image.Height;
      int[] numArray1 = new int[width * height];
      int index1 = 0;
      Color[] colorArray = new Color[width * height];
      for (int y = 0; y < height; ++y)
      {
        for (int x = 0; x < width; ++x)
        {
          colorArray[index1] = image.GetPixel(x, y);
          ++index1;
        }
      }
      List<Color> colorList = new List<Color>();
      for (int index2 = 0; index2 < colorArray.Length; ++index2)
      {
        if (!colorList.Contains(colorArray[index2]))
          colorList.Add(colorArray[index2]);
      }
      byte[] numArray2 = new byte[colorList.Count * 4];
      int index3 = 0;
      for (int index2 = 0; index2 < colorList.Count; ++index2)
      {
        Color color = colorList[index2];
        numArray2[index3] = color.R;
        numArray2[index3 + 1] = color.G;
        numArray2[index3 + 2] = color.B;
        numArray2[index3 + 3] = color.A;
        index3 += 4;
      }
      int[] numArray3 = new int[width * height];
      int index4 = 0;
      foreach (Color color in colorArray)
      {
        int num = colorList.IndexOf(color);
        if (num == -1)
          num = 0;
        numArray3[index4] = num;
        ++index4;
      }
      byte[] numArray4 = new byte[32 + numArray3.Length / 2];
      int index5 = 0;
      for (int index2 = 0; index2 < numArray2.Length; index2 += 4)
      {
        byte[] rgbA5551 = ImageHandler.RGBA8888ToRGBA5551(new byte[4]
        {
          numArray2[index2],
          numArray2[index2 + 1],
          numArray2[index2 + 2],
          numArray2[index2 + 3]
        }, ref alpha);
        numArray4[index5] = rgbA5551[0];
        numArray4[index5 + 1] = rgbA5551[1];
        index5 += 2;
      }
      int index6 = 32;
      for (int index2 = 0; index2 < numArray3.Length; index2 += 2)
      {
        byte num1 = (byte) ((uint) (byte) numArray3[index2] << 4);
        byte num2 = (byte) numArray3[index2 + 1];
        numArray4[index6] = (byte) ((uint) num1 + (uint) num2);
        ++index6;
      }
      return numArray4;
    }

    public static byte[] ConvertImageToCI8(Bitmap image, ref bool alpha, bool frame = false)
    {
      int width = image.Width;
      int height = image.Height;
      int[] numArray1 = new int[width * height];
      int index1 = 0;
      Color[] colorArray = new Color[width * height];
      for (int y = 0; y < height; ++y)
      {
        for (int x = 0; x < width; ++x)
        {
          colorArray[index1] = image.GetPixel(x, y);
          ++index1;
        }
      }
      List<Color> colorList = new List<Color>();
      for (int index2 = 0; index2 < colorArray.Length; ++index2)
      {
        if (!colorList.Contains(colorArray[index2]))
          colorList.Add(colorArray[index2]);
      }
      byte[] numArray2 = new byte[colorList.Count * 4];
      int index3 = 0;
      for (int index2 = 0; index2 < colorList.Count; ++index2)
      {
        Color color = colorList[index2];
        numArray2[index3] = color.R;
        numArray2[index3 + 1] = color.G;
        numArray2[index3 + 2] = color.B;
        numArray2[index3 + 3] = color.A;
        index3 += 4;
      }
      int[] numArray3 = new int[width * height];
      int index4 = 0;
      foreach (Color color in colorArray)
      {
        int num = colorList.IndexOf(color);
        if (num == -1)
          num = 0;
        numArray3[index4] = num;
        ++index4;
      }
      byte[] numArray4 = new byte[512 + numArray3.Length];
      int index5 = 0;
      for (int index2 = 0; index2 < numArray2.Length; index2 += 4)
      {
        byte[] rgbA5551 = ImageHandler.RGBA8888ToRGBA5551(new byte[4]
        {
          numArray2[index2],
          numArray2[index2 + 1],
          numArray2[index2 + 2],
          numArray2[index2 + 3]
        }, ref alpha);
        numArray4[index5] = rgbA5551[0];
        numArray4[index5 + 1] = rgbA5551[1];
        index5 += 2;
      }
      int index6 = 512;
      if (frame)
      {
        numArray4[index6] = (byte) 0;
        numArray4[index6 + 1] = (byte) 0;
        numArray4[index6 + 2] = (byte) 0;
        numArray4[index6 + 3] = (byte) 0;
        numArray4[index6 + 4] = (byte) 0;
        numArray4[index6 + 5] = (byte) width;
        numArray4[index6 + 6] = (byte) 0;
        numArray4[index6 + 7] = (byte) height;
        index6 = 520;
      }
      for (int index2 = 0; index2 < numArray3.Length; ++index2)
      {
        numArray4[index6] = (byte) numArray3[index2];
        ++index6;
      }
      return numArray4;
    }

    public static byte[] RGBA8888ToRGBA5551(byte[] rgba8888, ref bool alpha)
    {
      byte num1 = (byte) ((int) rgba8888[0] >> 3 << 3);
      int num2 = (int) (byte) ((int) rgba8888[1] >> 3 << 3);
      byte num3 = (byte) ((int) rgba8888[2] >> 3 << 3);
      byte num4;
      if (rgba8888[3] == (byte) 0)
      {
        num4 = (byte) 0;
        alpha = true;
      }
      else
        num4 = (byte) 1;
      byte num5 = (byte) (num2 >> 5);
      return new byte[2]
      {
        (byte) ((uint) num1 + (uint) num5),
        (byte) ((uint) (byte) (num2 << 3) + ((uint) num3 >> 2) + (uint) num4)
      };
    }

    public static byte[] RGBA5551ToRGBA8888(byte[] rgba5551, ref bool alpha)
    {
      byte num1 = (byte) ((int) rgba5551[0] >> 3 << 3);
      byte num2 = (byte) ((((int) rgba5551[0] & 7) << 5) + ((int) rgba5551[1] >> 6));
      byte num3 = (byte) (((int) rgba5551[1] & 63) << 2 & 254);
      byte num4;
      if (((int) rgba5551[1] & 1) == 1)
      {
        num4 = byte.MaxValue;
      }
      else
      {
        num4 = (byte) 0;
        alpha = true;
      }
      return new byte[4]{ num1, num2, num3, num4 };
    }
  }
}
