//
// SeparatorHiding.cs
//
// Author:
//       Yauheni Pakala <evgeniy.pakalo@gmail.com>
//
// Copyright (c) 2019 Yauheni Pakala
//
namespace AloeStackView.Protocols
{
    /**
     * Indicates that a row in an `AloeStackView` should hide its separator.
     *
     * Rows that are added to an `AloeStackView` can conform to this protocol to automatically their
     * separators.
     *
     * This behavior can be useful when implementing shared, reusable rows that should always have this
     * behavior when they are used in an `AloeStackView`.
     */
    public interface SeparatorHiding
    {
    }
}
