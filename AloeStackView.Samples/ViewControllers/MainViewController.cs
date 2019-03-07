//
// ViewController.cs
//
// C# port created by Yauheni Pakala
// Copyright (c) 2019
//
// Original Swift version created by Marli Oshlack
// Copyright 2018 Airbnb, Inc.
//
using System;
using AloeStackView.Samples.Views;
using UIKit;
using System.Linq;
using CoreGraphics;

namespace AloeStackView.Samples.ViewControllers
{
    public partial class MainViewController : AloeStackViewController
    {
        #region ctor
        protected MainViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public MainViewController()
        {
        }
        #endregion

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetUpSelf();
            SetUpStackView();
            SetUpRows();
        }

        private void SetUpSelf()
        {
            Title = "AloeStackView Example";
        }

        private void SetUpStackView()
        {
            StackView.AutomaticallyHidesLastSeparator = true;
        }

        private void SetUpRows()
        {
            SetUpDescriptionRow();
            SetUpSwitchRow();
            SetUpHiddenRows();
            SetUpExpandingRowView();
            SetUpPhotoRow();
        }

        private void SetUpDescriptionRow()
        {
            var label = new UILabel
            {
                Font = UIFont.PreferredBody,
                Lines = 0,
                Text = "This simple app shows some ways you can use AloeStackView to lay out a screen in your app."
            };
            StackView.AddRow(label);
        }

        private void SetUpSwitchRow()
        {
            var switchRow = new SwitchRowView();
            switchRow.Text = "Show and hide rows with animation";
            switchRow.SwitchChanged += (sender, isOn) =>
            {
                StackView.SetRowsHidden(hiddenRows, isHidden: !isOn, animated: true);
            };
            StackView.AddRow(switchRow);
        }

        private UILabel[] hiddenRows = { new UILabel(), new UILabel(), new UILabel(), new UILabel(), new UILabel() };

        private void SetUpHiddenRows()
        {
            for (int index = 0; index < hiddenRows.Length; index++)
            {
                var row = hiddenRows[index];

                row.Font = UIFont.PreferredCaption2;
                row.Text = $"Hidden row {index + 1}";
            }

            StackView.AddRows(hiddenRows);
            StackView.HideRows(hiddenRows);

            var rowInset = new UIEdgeInsets(
                top: StackView.RowInset.Top,
                left: StackView.RowInset.Left * 2f,
                bottom: StackView.RowInset.Bottom,
                right: StackView.RowInset.Right);

            var separatorInset = new UIEdgeInsets(
                top: 0,
                left: StackView.SeparatorInset.Left * 2f,
                bottom: 0,
                right: 0);

            StackView.SetInset(hiddenRows, inset: rowInset);
            StackView.SetSeperatorInset(hiddenRows.SkipLast(1).ToArray(), inset: separatorInset);
        }

        private void SetUpExpandingRowView()
        {
            var expandingRow = new ExpandingRowView();
            StackView.AddRow(expandingRow);
        }

        private void SetUpPhotoRow()
        {
            var titleLabel = new UILabel();
            titleLabel.Font = UIFont.PreferredBody;
            titleLabel.Lines = 0;
            titleLabel.Text = "Handle user interaction";
            StackView.AddRow(titleLabel);
            StackView.HideSeparator(titleLabel);
            StackView.SetInset(forRow: titleLabel, inset: new UIEdgeInsets(
                top: StackView.RowInset.Top,
                left: StackView.RowInset.Left,
                bottom: 4,
                right: StackView.RowInset.Right));

            var captionLabel = new UILabel();
            captionLabel.Font = UIFont.PreferredCaption2;
            captionLabel.TextColor = UIColor.Blue;
            captionLabel.Lines = 0;
            captionLabel.Text = "(Try tapping on the photo!)";
            StackView.AddRow(captionLabel);
            StackView.HideSeparator(forRow: captionLabel);
            StackView.SetInset(forRow: captionLabel, inset: new UIEdgeInsets(
                top: 0,
                left: StackView.RowInset.Left,
                bottom: StackView.RowInset.Bottom,
                right: StackView.RowInset.Right));

            var image = new UIImage(); // TODO: add image
            var aspectRatio = 1; //image.Size.Height / image.Size.Width;

            var imageView = new UIImageView(new CGRect(0,0, 100, 100)); // image: image
            imageView.BackgroundColor = UIColor.Red;
            imageView.UserInteractionEnabled = true;
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            imageView.HeightAnchor.ConstraintEqualTo(imageView.WidthAnchor, multiplier: aspectRatio).Active = true;

            StackView.AddRow(imageView);
            StackView.SetTapHandler(imageView, _ =>
            {
                var alert = UIAlertController.Create("Title ;)", "Tapped", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Cancel, null));
                PresentViewController(alert, true, null);

                // TODO: create photo view controller
                //var vc = new PhotoViewController();
                //NavigationController.PushViewController(vc, animated: true);
            });
        }
    }
}
