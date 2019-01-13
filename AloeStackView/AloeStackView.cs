//
// AloeStackView.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace AloeStackView
{
    /*
     * A simple class for laying out a collection of views with a convenient API, while leveraging the
     * power of Auto Layout.
     */
    public class AloeStackView : UIScrollView
    {
        #region Lifecycle

        public AloeStackView() : base(CGRect.Empty)
        {
            setUpViews();
            setUpConstraints();
        }

        public AloeStackView(NSCoder coder)
        {
            throw new NotImplementedException("init(coder:) has not been implemented");
        }

        #endregion

        #region Public

        #region Adding and Removing Rows

        /// Adds a row to the end of the stack view.
        ///
        /// If `animated` is `true`, the insertion is animated.
        public void addRow(UIView row, bool animated = false)
        {
            // TODO: 
            //insertCell(withContentView: row, atIndex: stackView.arrangedSubviews.count, animated: animated);
        }

        #endregion

        #endregion

        // TODO: 

        #region Private

        private UIStackView stackView = new UIStackView();

        private void setUpViews()
        {
            setUpSelf();
            setUpStackView();
        }

        private void setUpSelf()
        {
            BackgroundColor = UIColor.White;
        }

        private void setUpStackView()
        {
            stackView.TranslatesAutoresizingMaskIntoConstraints = false;
            stackView.Axis = UILayoutConstraintAxis.Vertical;
            AddSubview(stackView);
        }

        private void setUpConstraints()
        {
            setUpStackViewConstraints();
        }

        private void setUpStackViewConstraints()
        {
            NSLayoutConstraint.ActivateConstraints( new NSLayoutConstraint[] { 
              stackView.TopAnchor.ConstraintEqualTo(TopAnchor),
              stackView.BottomAnchor.ConstraintEqualTo(BottomAnchor),
              stackView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor),
              stackView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor),
              stackView.WidthAnchor.ConstraintEqualTo(WidthAnchor)
            });
        }

        // TODO:

        #endregion
    }
}
