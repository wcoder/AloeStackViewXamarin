//
// Tappable.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
namespace AloeStackView.Protocols
{
    /**
     * Notifies a row in an `AloeStackView` when it receives a user tap.
     *
     * Rows that are added to an `AloeStackView` can conform to this protocol to be notified when a
     * user taps on the row. This notification happens regardless of whether the row has a tap handler
     * set for it or not.
     *
     * This notification can be used to implement default behavior in a view that should always happen
     * when that view is tapped.
     */
    public interface Tappable
    {
        /// Called when the row is tapped by the user.
        void didTapView();
    }
}
