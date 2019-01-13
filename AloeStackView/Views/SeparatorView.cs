//
// SeparatorView.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
using System;
using CoreGraphics;
using UIKit;
using Foundation;

namespace AloeStackView.Views
{
    public class SeparatorView : UIView
    {
        #region Lifecycle

        public SeparatorView() : base(CGRect.Empty)
        {
            TranslatesAutoresizingMaskIntoConstraints = false;
        }

        public SeparatorView(NSCoder coder)
        {
            throw new NotImplementedException("init(coder:) has not been implemented");
        }

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
