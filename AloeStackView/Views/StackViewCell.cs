//
// StackViewCell.cs
//
// C# port created by Yauheni Pakala
// Copyright (c) 2019
//
// Original Swift version created by Marli Oshlack
// Copyright 2018 Airbnb, Inc.
//
using System;
using UIKit;
using CoreGraphics;
using Foundation;
using AloeStackView.Protocols;

namespace AloeStackView.Views
{
    /// <summary>
    /// A view that wraps every row in a stack view.
    /// </summary>
    public class StackViewCell : UIView
    {
        #region Lifecycle

        public StackViewCell(UIView contentView) : base(CGRect.Empty)
        {
            this.contentView = contentView;

            TranslatesAutoresizingMaskIntoConstraints = false;

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                InsetsLayoutMarginsFromSafeArea = false;
            }

            SetUpViews();
            SetUpConstraints();
            SetUpTapGestureRecognizer();
        }

        public StackViewCell(NSCoder coder) => throw new NotImplementedException("ctor(coder:) has not been implemented");

        #endregion

        #region Open

        public override bool Hidden
        {
            get => base.Hidden;
            set
            {
                if (Hidden != value)
                {
                    separatorView.Alpha = value ? 0 : 1;
                }

                base.Hidden = value;
            }
        }

        public UIColor RowHighlightColor { get; set; } = new UIColor(red: 217 / 255, green: 217 / 255, blue: 217 / 255, alpha: 1);

        private UIColor _rowBackgroundColor = UIColor.Clear;
        public UIColor RowBackgroundColor
        {
            get => _rowBackgroundColor;
            set
            {
                _rowBackgroundColor = value;
                BackgroundColor = _rowBackgroundColor;
            }
        }

        public UIEdgeInsets RowInset
        {
            get => LayoutMargins;
            set => LayoutMargins = value;
        }

        public UIColor SeparatorColor
        {
            get => separatorView.Color;
            set => separatorView.Color = value;
        }

        public nfloat SeparatorHeight
        {
            get => separatorView.Height;
            set => separatorView.Height = value;
        }

        private UIEdgeInsets _separatorInset = UIEdgeInsets.Zero;
        public UIEdgeInsets SeparatorInset
        {
            get => _separatorInset;
            set
            {
                _separatorInset = value;
                UpdateSeparatorInset();
            }
        }

        public bool IsSeparatorHidden
        {
            get => separatorView.Hidden;
            set => separatorView.Hidden = value;
        }

        #endregion

        #region Public

        public UIView contentView;

        #endregion

        #region UIResponder

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (contentView != null && contentView.UserInteractionEnabled) return;

            if (contentView is IHighlightable highlightable && highlightable.IsHighlightable)
            {
                contentView.SetIsHighlighted(true);
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            if (contentView != null && contentView.UserInteractionEnabled) return;

            if (contentView is IHighlightable highlightable && highlightable.IsHighlightable)
            {
                contentView.SetIsHighlighted(false);
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (contentView != null && contentView.UserInteractionEnabled) return;

            if (contentView is IHighlightable highlightable && highlightable.IsHighlightable)
            {
                contentView.SetIsHighlighted(false);
            }
        }

        #endregion

        #region Internal

        private Action<UIView> _tapHandler;
        internal Action<UIView> TapHandler
        {
            get => _tapHandler;
            set
            {
                _tapHandler = value;
                UpdateTapGestureRecognizerEnabled();
            }
        }

        // Whether the separator should be hidden or not for this cell. Note that this doesn't always
        // reflect whether the separator is hidden or not, since, for example, the separator could be
        // hidden because it's the last row in the stack view and
        // `automaticallyHidesLastSeparator` is `true`.
        public bool ShouldHideSeparator;

        #endregion

        #region Private

        private SeparatorView separatorView = new SeparatorView();
        private UITapGestureRecognizer tapGestureRecognizer = new UITapGestureRecognizer();

        private NSLayoutConstraint separatorLeadingConstraint;
        private NSLayoutConstraint separatorTrailingConstraint;

        private void SetUpViews()
        {
            SetUpSelf();
            SetUpContentView();
            SetUpSeparatorView();
        }

        private void SetUpSelf()
        {
            ClipsToBounds = true;
        }

        private void SetUpContentView()
        {
            contentView.TranslatesAutoresizingMaskIntoConstraints = false;
            AddSubview(contentView);
        }

        private void SetUpSeparatorView()
        {
            AddSubview(separatorView);
        }

        private void SetUpConstraints()
        {
            SetUpContentViewConstraints();
            SetUpSeparatorViewConstraints();
        }

        private void SetUpContentViewConstraints()
        {
            var bottomConstraint = contentView.BottomAnchor.ConstraintEqualTo(LayoutMarginsGuide.BottomAnchor);
            bottomConstraint.Priority = (float)UILayoutPriority.Required - 1;

            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[]
            {
                contentView.LeadingAnchor.ConstraintEqualTo(LayoutMarginsGuide.LeadingAnchor),
                contentView.TrailingAnchor.ConstraintEqualTo(LayoutMarginsGuide.TrailingAnchor),
                contentView.TopAnchor.ConstraintEqualTo(LayoutMarginsGuide.TopAnchor),
                bottomConstraint
            });
        }

        private void SetUpSeparatorViewConstraints()
        {
            var leadingConstraint = separatorView.LeadingAnchor.ConstraintEqualTo(LeadingAnchor);
            var trailingConstraint = separatorView.TrailingAnchor.ConstraintEqualTo(TrailingAnchor);

            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[]
            {
                separatorView.BottomAnchor.ConstraintEqualTo(BottomAnchor),
                leadingConstraint,
                trailingConstraint
            });

            separatorLeadingConstraint = leadingConstraint;
            separatorTrailingConstraint = trailingConstraint;
        }

        private void SetUpTapGestureRecognizer()
        {
            tapGestureRecognizer.AddTarget(HandleTap);
            AddGestureRecognizer(tapGestureRecognizer);

            UpdateTapGestureRecognizerEnabled();
        }

        private void HandleTap()
        {
            if (contentView != null && !contentView.UserInteractionEnabled) return;

            if (contentView is ITappable tappableView)
            {
                tappableView.DidTapView();
            }

            TapHandler?.Invoke(contentView);
        }

        private void UpdateTapGestureRecognizerEnabled()
        {
            tapGestureRecognizer.Enabled = contentView is ITappable || TapHandler != null;
        }

        private void UpdateSeparatorInset()
        {
            if (separatorLeadingConstraint != null)
            {
                separatorLeadingConstraint.Constant = SeparatorInset.Left;
            }
            if (separatorTrailingConstraint != null)
            {
                separatorTrailingConstraint.Constant = -SeparatorInset.Right;
            }
        }

        #endregion
    }
}
