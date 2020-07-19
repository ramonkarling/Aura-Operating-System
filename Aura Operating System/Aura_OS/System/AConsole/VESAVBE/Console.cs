﻿/*
* PROJECT:          Aura Operating System Development
* CONTENT:          VBE VESA Console
* PROGRAMMERS:      Valentin Charbonnier <valentinbreiz@gmail.com>
*/


using System;
using System.Drawing;
using Cosmos.System.Graphics;

namespace Aura_OS.System.AConsole.VESAVBE
{
    public class VESAVBEConsole : Console
    {

        public Graphics.Graphics graphics;
        private const byte LineFeed = (byte)'\n';
        private const byte CarriageReturn = (byte)'\r';
        private const byte Tab = (byte)'\t';
        private const byte Space = (byte)' ';

        public VESAVBEConsole()
        {
            Name = "VESA";
            graphics = new Graphics.Graphics();
            mWidth = graphics.canvas.Mode.Columns / graphics.font.Width;
            mHeight = graphics.canvas.Mode.Rows / graphics.font.Height;

            mRows = mWidth;
            mCols = mHeight;
        }

        protected int mX = 0;
        public override int X
        {
            get { return mX; }
            set
            {
                mX = value;
            }
        }


        protected int mY = 0;
        public override int Y
        {
            get { return mY; }
            set
            {
                mY = value;
            }
        }

        public static int mWidth;
        public override int Width
        {
            get { return mWidth; }
        }

        public static int mHeight;
        public override int Height
        {
            get { return mHeight; }
        }

        public static int mCols;
        public override int Cols
        {
            get { return mCols; }
        }

        public static int mRows;
        public override int Rows
        {
            get { return mRows; }
        }

        public static uint foreground = (byte)ConsoleColor.White;
        public override ConsoleColor Foreground
        {
            get { return (ConsoleColor)foreground; }
            set
            {
                foreground = (byte)global::System.Console.ForegroundColor;
                graphics.ChangeForegroundPen(foreground);
            }
        }

        public static uint background = (byte)ConsoleColor.Black;

        public override ConsoleColor Background
        {
            get { return (ConsoleColor)background; }
            set
            {
                background = (byte)global::System.Console.BackgroundColor;
                graphics.ChangeBackgroundPen(background);
            }
        }

        public override int CursorSize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override bool CursorVisible { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Clear()
        {
            graphics.canvas.Clear();
            mX = 0;
            mY = 0;
        }

        public override void Clear(uint color)
        {
            graphics.canvas.Clear(Color.FromArgb((int)color));
            mX = 0;
            mY = 0;
        }

        public override void Write(byte[] aText)
        {
            foreach (byte ch in aText)
            {
                switch (ch)
                {

                    case Tab:
                        DoTab();
                        break;

                    default:
                        graphics.WriteByte(ch);
                        break;
                }
            }
        }

        private void DoTab()
        {
            graphics.WriteByte(Space);
            graphics.WriteByte(Space);
            graphics.WriteByte(Space);
            graphics.WriteByte(Space);
        }

        public override void DrawImage(ushort X, ushort Y, Bitmap image)
        {
            graphics.canvas.DrawImage(image, X, Y);
        }

    }
}