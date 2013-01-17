﻿// XAML Map Control - http://xamlmapcontrol.codeplex.com/
// Copyright © 2013 Clemens Fischer
// Licensed under the Microsoft Public License (Ms-PL)

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MapControl
{
    public partial class MapBase
    {
        // FillBehavior must be set to Stop to re-enable local property values
        private const FillBehavior AnimationFillBehavior = FillBehavior.Stop;

        public static readonly DependencyProperty ForegroundProperty =
            System.Windows.Controls.Control.ForegroundProperty.AddOwner(typeof(MapBase));

        static MapBase()
        {
            UIElement.ClipToBoundsProperty.OverrideMetadata(
                typeof(MapBase), new FrameworkPropertyMetadata(true));

            Panel.BackgroundProperty.OverrideMetadata(
                typeof(MapBase), new FrameworkPropertyMetadata(Brushes.Transparent));
        }

        partial void Initialize()
        {
            AddVisualChild(tileContainer);

            SizeChanged += OnRenderSizeChanged;
        }

        private void OnRenderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ResetTransformOrigin();
            UpdateTransform();
        }

        private void SetTransformMatrixes(double scale)
        {
            Matrix rotateMatrix = Matrix.Identity;
            rotateMatrix.Rotate(Heading);
            rotateTransform.Matrix = rotateMatrix;
            scaleTransform.Matrix = new Matrix(scale, 0d, 0d, scale, 0d, 0d);
            scaleRotateTransform.Matrix = scaleTransform.Matrix * rotateMatrix;
        }

        protected override int VisualChildrenCount
        {
            get { return InternalChildren.Count + 1; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index == 0)
            {
                return tileContainer;
            }

            return InternalChildren[index - 1];
        }
    }
}
