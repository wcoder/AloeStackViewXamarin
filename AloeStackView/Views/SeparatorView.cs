﻿//
// SeparatorView.cs
//
// C# port created by Yauheni Pakala
// Copyright (c) 2019
//
// Original Swift version created by Marli Oshlack
// Copyright 2018 Airbnb, Inc.
//
using System;
using CoreGraphics;
using UIKit;
using Foundation;

namespace AloeStackView.Views
{
    internal class SeparatorView : UIView
    {
        #region Lifecycle

        public SeparatorView() : base(CGRect.Empty)
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        public SeparatorView(NSCoder coder) => throw new NotImplementedException("ctor(coder:) has not been implemented");

        #endregion

        #region Internal

        public override CGSize IntrinsicContentSize => new CGSize(UIView.NoIntrinsicMetric, height: Height);

        public UIColor Color
        {
            get => BackgroundColor ?? UIColor.Clear;
            set => BackgroundColor = value;
        }

        private nfloat _height = 1;
        public nfloat Height
        {
            get => _height;
            set
            {
                _height = value;
                InvalidateIntrinsicContentSize();
            }
        }

        #endregion
    }
}
