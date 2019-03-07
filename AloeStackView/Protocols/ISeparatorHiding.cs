//
// SeparatorHiding.cs
//
// C# port created by Yauheni Pakala
// Copyright (c) 2019
//
// Original Swift version created by Marli Oshlack
// Copyright 2018 Airbnb, Inc.
//
namespace AloeStackView.Protocols
{
    /// <summary>
    /// Indicates that a row in an `AloeStackView` should hide its separator.
    ///
    /// Rows that are added to an `AloeStackView` can conform to this protocol to automatically their
    /// separators.
    ///
    /// This behavior can be useful when implementing shared, reusable rows that should always have this
    /// behavior when they are used in an `AloeStackView`.
    /// </summary>
    public interface ISeparatorHiding
    {
    }
}
