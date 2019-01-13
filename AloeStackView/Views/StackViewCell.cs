//
// StackViewCell.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
using System;
using UIKit;
using CoreGraphics;
using Foundation;
using AloeStackView.Protocols;

namespace AloeStackView.Views
{
    /*
     * A view that wraps every row in a stack view.
     */
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

            setUpViews();
            setUpConstraints();
            setUpTapGestureRecognizer();
        }

        public StackViewCell(NSCoder coder)
        {
            throw new NotImplementedException("init(coder:) has not been implemented");
        }

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

        public UIColor rowHighlightColor => new UIColor(red: 217 / 255, green: 217 / 255, blue: 217 / 255, alpha: 1);

        private UIColor _rowBackgroundColor = UIColor.Clear;
        public UIColor rowBackgroundColor
        {
            get => _rowBackgroundColor;
            set
            {
                _rowBackgroundColor = value;
                BackgroundColor = _rowBackgroundColor;
            }
        }

        public UIEdgeInsets rowInset
        {
            get => LayoutMargins;
            set => LayoutMargins = value;
        }

        public UIColor separatorColor
        {
            get => separatorView.Color;
            set => separatorView.Color = value;
        }

        public nfloat separatorHeight
        {
            get => separatorView.Height;
            set => separatorView.Height = value;
        }

        private UIEdgeInsets _separatorInset = UIEdgeInsets.Zero;
        public UIEdgeInsets separatorInset
        {
            get => _separatorInset;
            set
            {
                _separatorInset = value;
                updateSeparatorInset();
            }
        }

        public bool isSeparatorHidden
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

            if (contentView is Highlightable highlightable && highlightable.isHighlightable)
            {
                contentView.setIsHighlighted(true);
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            if (contentView != null && contentView.UserInteractionEnabled) return;

            if (contentView is Highlightable highlightable && highlightable.isHighlightable)
            {
                contentView.setIsHighlighted(false);
            }
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (contentView != null && contentView.UserInteractionEnabled) return;

            if (contentView is Highlightable highlightable && highlightable.isHighlightable)
            {
                contentView.setIsHighlighted(false);
            }
        }

        #endregion

        #region Internal


        private Action<UIView> tapHandler;
        // TODO:
        //internal var tapHandler: ((UIView) -> Void)? {
        //    didSet { updateTapGestureRecognizerEnabled() }
        //}

        // Whether the separator should be hidden or not for this cell. Note that this doesn't always
        // reflect whether the separator is hidden or not, since, for example, the separator could be
        // hidden because it's the last row in the stack view and
        // `automaticallyHidesLastSeparator` is `true`.
        public bool shouldHideSeparator = false;

        #endregion

        #region Private

        private SeparatorView separatorView = new SeparatorView();
        private UITapGestureRecognizer tapGestureRecognizer = new UITapGestureRecognizer();

        private NSLayoutConstraint separatorLeadingConstraint;
        private NSLayoutConstraint separatorTrailingConstraint;

        private void setUpViews()
        {
            setUpSelf();
            setUpContentView();
            setUpSeparatorView();
        }

        private void setUpSelf()
        {
            ClipsToBounds = true;
        }

        private void setUpContentView()
        {
            contentView.TranslatesAutoresizingMaskIntoConstraints = false;
            AddSubview(contentView);
        }

        private void setUpSeparatorView()
        {
            AddSubview(separatorView);
        }

        private void setUpConstraints()
        {
            setUpContentViewConstraints();
            setUpSeparatorViewConstraints();
        }

        private void setUpContentViewConstraints()
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

        private void setUpSeparatorViewConstraints()
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

        private void setUpTapGestureRecognizer()
        {
            tapGestureRecognizer.AddTarget(handleTap);
            AddGestureRecognizer(tapGestureRecognizer);

            updateTapGestureRecognizerEnabled();
        }

        private void handleTap()
        {
            if (contentView != null && contentView.UserInteractionEnabled) return;

            if (contentView is Tappable tappableView)
            {
                tappableView.didTapView();
            }

            tapHandler(contentView);
        }

        private void updateTapGestureRecognizerEnabled()
        {
            tapGestureRecognizer.Enabled = contentView is Tappable || tapHandler != null;
        }

        private void updateSeparatorInset()
        {
            if (separatorLeadingConstraint != null)
            {
                separatorLeadingConstraint.Constant = separatorInset.Left;
            }
            if (separatorTrailingConstraint != null)
            {
                separatorTrailingConstraint.Constant = -separatorInset.Right;
            }
        }

        #endregion
    }
}
