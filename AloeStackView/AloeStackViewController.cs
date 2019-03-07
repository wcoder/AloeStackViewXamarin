//
// AloeStackViewController.cs
//
// C# port created by Yauheni Pakala
// Copyright (c) 2019
//
// Original Swift version created by Marli Oshlack
// Copyright 2018 Airbnb, Inc.
//
using System;
using Foundation;
using UIKit;

namespace AloeStackView
{
    /// <summary>
    /// A view controller that specializes in managing an AloeStackView.
    /// </summary>
    public class AloeStackViewController : UIViewController
    {
        public AloeStackViewController(IntPtr handler) : base(handler)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        #region Lifecycle

        public AloeStackViewController()
        {
        }

        public AloeStackViewController(NSCoder coder) => throw new NotImplementedException("ctor(coder:) has not been implemented");

        public override void LoadView()
        {
            View = StackView;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (AutomaticallyFlashScrollIndicators)
            {
                StackView.FlashScrollIndicators();
            }
        }

        #endregion

        #region Public

        /// The stack view this controller manages.
        public AloeStackView StackView = new AloeStackView();

        /// When true, automatically displays the scroll indicators in the stack view momentarily whenever the view appears.
        ///
        /// Default is `false`.
        public bool AutomaticallyFlashScrollIndicators = false;

        #endregion
    }
}
