# AloeStackViewXamarin

C# port of [airbnb/AloeStackView](https://github.com/airbnb/AloeStackView) v1.0.1.

A simple class for laying out a collection of views with a convenient API, while leveraging the power of Auto Layout.

- [Documentation](https://github.com/airbnb/AloeStackView#table-of-contents)

## Installation

> TODO: NuGet

## Introduction

`AloeStackView` is a class that allows a collection of views to be laid out in a vertical list. In a broad sense, it is similar
to `UITableView`, however its implementation is quite different and it makes a different set of trade-offs.

We first started using `AloeStackView` at Airbnb in our iOS app in 2016. We have since used it to implement nearly
200 screens in the app. The use cases are quite varied: everything from settings screens, to forms for creating a new
listing, to the listing share sheet.

|![Airbnb app 1](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_1.png)|![Airbnb app 2](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_2.png)|![Airbnb app 3](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_3.png)|![Airbnb app 11](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_11.png)|![Airbnb app 4](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_4.png)|![Airbnb app 8](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_8.png)|
| --- | --- | --- | --- | --- | --- |
|![Airbnb app 7](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_7.png)|![Airbnb app 6](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_6.png)|![Airbnb app 5](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_5.png)|![Airbnb app 9](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_9.png)|![Airbnb app 10](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_10.png)|![Airbnb app 12](https://github.com/airbnb/AloeStackView/raw/master/Docs/Images/airbnb_app_12.png)|

`AloeStackView` focuses first and foremost on making UI very quick, simple, and straightforward to implement. It
does this in two ways:

* It leverages the power of Auto Layout to automatically update the UI when making changes to views.

* It forgoes some features of `UITableView`, such as view recycling, in order to achieve a much simpler and safer API.

We've found `AloeStackView` to be a useful piece of infrastructure and hope you find it useful too!

## Features

* Allows you to keep strong references to views and dynamically change their properties, while Auto Layout
automatically keeps the UI up-to-date.

* Allows views to be dynamically added, removed, hidden and shown, with optional animation.

* Includes built-in support for customizable separators between views.

* Provides an extensible API, allowing specialized features to be added without modifying `AloeStackView` itself.

* Widely used and vetted in a highly-trafficked iOS app.

* Small, easy-to-understand codebase (under 500 lines of code) with no external dependencies keeps binary size
increase to a minimum and makes code contributions and debugging painless.

### When to use AloeStackView

#### The Short Answer

`AloeStackView` is best used for shorter screens with less than a screenful or two of content. It is particularly suited to
screens that accept user input, implement forms, or are comprised of a heterogeneous set of views.

However, it's also helpful to dig a bit deeper into the technical details of `AloeStackView`, as this can help develop a
better understanding of appropriate use cases.

#### More Details

`AloeStackView` is a very useful tool to have in the toolbox. Its straightforward, flexible API allows you to build UI
quickly and easily.

Unlike `UITableView` and `UICollectionView`, you can keep strong references to views in an `AloeStackView` and
make changes to them at any point. This will automatically update the entire UI thanks to Auto Layout - there is no
need to notify `AloeStackView` of the changes.

This makes `AloeStackView` great for use cases such as forms and screens that take user input. In these situations,
it's often convenient to keep a strong reference to the fields a user is editing, and directly update the UI with validation
feedback.

`AloeStackView` has no `reloadData` method, or any way to notify it about changes to your views. This makes it less
error-prone and easier to debug than a class like `UITableView`. For example, `AloeStackView` won't crash if not
notified of changes to the underlying data of the views it manages.

Since `AloeStackView` uses `UIStackView` under the hood, it doesn't recycle views as you scroll. This eliminates
common bugs caused by not recycling views correctly. You also don't need to independently maintain the state of
views as the user interacts with them, which makes it simpler to implement certain kinds of UI.

However, `AloeStackView` is not suitable in all situations. `AloeStackView` lays out the entire UI in a single pass
when your screen loads. As such, longer screens will start seeing a noticeable delay before the UI is displayed for the
first time. This is not a great experience for users and can make an app feel unresponsive to navigation actions.
Hence, `AloeStackView` should not be used when implementing UI with more than a screenful or two of content.

Forgoing view recycling is also a trade-off: while `AloeStackView`  is faster to write UI with and less error-prone, it will
perform worse and use more memory for longer screens than a class like `UITableView`. Hence, `AloeStackView` is
generally not appropriate for screens that contain many views of the same type, all showing similar data. Classes like
`UITableView` or `UICollectionView` often perform better in those situations.

While `AloeStackView` is not the only piece of infrastructure we use to build iOS UI at Airbnb, it has been valuable for
us in many situations. We hope you find it useful too!

## Contributions

`AloeStackView` is feature complete for the use cases it was originally designed to address. However, UI
development on iOS is never a solved problem, and we expect new use cases to arise and old bugs to be uncovered.

As such we fully welcome contributions, including new features, feature requests, bug reports, and fixes. If you'd like
to contribute, simply push a PR with a description of your changes. You can also file a GitHub Issue for any bug
reports or feature requests.

Please feel free to email the project maintainers if you'd like to get in touch. We'd love to hear from you if you or your
company has found this library useful!

## Maintainers

`AloeStackViewXamarin` is ported by: Yauheni Pakala

`AloeStackView` is developed and maintained by:

Marli Oshlack (marli@oshlack.com)

Fan Cox (fan.cox@airbnb.com)

Arthur Pang (arthur.pang@airbnb.com)

## Contributors

`AloeStackView` has benefited from the contributions of many other Airbnb engineers:

Daniel Crampton, Francisco Diaz, David He, Jeff Hodnett, Eric Horacek, Garrett Larson, Jasmine Lee, Isaac Lim,
Jacky Lu, Noah Martin, Phil Nachum, Gonzalo Nuñez, Laura Skelton, Cal Stephens, and Ortal Yahdav

In addition, open sourcing this project wouldn't have been possible without the help and support of Jordan Harband,
Tyler Hedrick, Michael Bachand, Laura Skelton, Dan Federman, and John Pottebaum.

## License

`AloeStackViewXamarin` is released under the MIT License. See [LICENSE](LICENSE) for details.

`AloeStackView` is released under the Apache License 2.0. See [NOTICE](NOTICE.txt) for details.

## Why is it called AloeStackView?

We like succulents and find the name soothing 😉
