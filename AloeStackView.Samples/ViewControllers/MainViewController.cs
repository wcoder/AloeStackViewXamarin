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
            StackView.automaticallyHidesLastSeparator = true;
        }

        private void SetUpRows()
        {
            SetUpDescriptionRow();
            SetUpSwitchRow();
            SetUpHiddenRows();
            SetUpExpandingRowView();
            setUpPhotoRow();
        }

        private void SetUpDescriptionRow()
        {
            var label = new UILabel
            {
                Font = UIFont.PreferredBody,
                Lines = 0,
                Text = "This simple app shows some ways you can use AloeStackView to lay out a screen in your app."
            };
            StackView.addRow(label);
        }

        private void SetUpSwitchRow()
        {
            var switchRow = new SwitchRowView();
            switchRow.Text = "Show and hide rows with animation";
            switchRow.SwitchChanged += (sender, isOn) =>
            {
                StackView.setRowsHidden(hiddenRows, isHidden: !isOn, animated: true);
            };
            StackView.addRow(switchRow);
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

            StackView.addRows(hiddenRows);
            StackView.hideRows(hiddenRows);

            var rowInset = new UIEdgeInsets(
                top: StackView.rowInset.Top,
                left: StackView.rowInset.Left * 2f,
                bottom: StackView.rowInset.Bottom,
                right: StackView.rowInset.Right);

            var separatorInset = new UIEdgeInsets(
                top: 0,
                left: StackView.separatorInset.Left * 2f,
                bottom: 0,
                right: 0);

            StackView.setInset(hiddenRows, inset: rowInset);
            StackView.setSeperatorInset(hiddenRows.SkipLast(1).ToArray(), inset: separatorInset);
        }

        private void SetUpExpandingRowView()
        {
            var expandingRow = new ExpandingRowView();
            StackView.addRow(expandingRow);
        }

        private void setUpPhotoRow()
        {
            var titleLabel = new UILabel();
            titleLabel.Font = UIFont.PreferredBody;
            titleLabel.Lines = 0;
            titleLabel.Text = "Handle user interaction";
            StackView.addRow(titleLabel);
            StackView.hideSeparator(titleLabel);
            StackView.setInset(forRow: titleLabel, inset: new UIEdgeInsets(
                top: StackView.rowInset.Top,
                left: StackView.rowInset.Left,
                bottom: 4,
                right: StackView.rowInset.Right));

            var captionLabel = new UILabel();
            captionLabel.Font = UIFont.PreferredCaption2;
            captionLabel.TextColor = UIColor.Blue;
            captionLabel.Lines = 0;
            captionLabel.Text = "(Try tapping on the photo!)";
            StackView.addRow(captionLabel);
            StackView.hideSeparator(forRow: captionLabel);
            StackView.setInset(forRow: captionLabel, inset: new UIEdgeInsets(
                top: 0,
                left: StackView.rowInset.Left,
                bottom: StackView.rowInset.Bottom,
                right: StackView.rowInset.Right));

            var image = new UIImage(); // TODO: add image
            var aspectRatio = 1; //image.Size.Height / image.Size.Width;

            var imageView = new UIImageView(new CGRect(0,0, 100, 100)); // image: image
            imageView.BackgroundColor = UIColor.Red;
            imageView.UserInteractionEnabled = true;
            imageView.ContentMode = UIViewContentMode.ScaleAspectFit;
            imageView.HeightAnchor.ConstraintEqualTo(imageView.WidthAnchor, multiplier: aspectRatio).Active = true;

            StackView.addRow(imageView);
            StackView.setTapHandler(imageView, _ =>
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
