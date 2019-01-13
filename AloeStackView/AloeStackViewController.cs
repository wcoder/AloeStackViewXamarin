//
// AloeStackViewController.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
using System;
using Foundation;
using UIKit;

namespace AloeStackView
{
    /*
     * A view controller that specializes in managing an AloeStackView.
     */
    public class AloeStackViewController : UIViewController
    {
        #region Lifecycle

        public AloeStackViewController()
        {
        }

        public AloeStackViewController(NSCoder coder)
        {
            throw new NotImplementedException("init(coder:) has not been implemented");
        }

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
