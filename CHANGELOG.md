# [1.1.0] - 2025-10-11

Implemented the following changes to the `Clock` class:

- Reworked the `OnTimeChanged` event to correspond to **any** change in the clock's `Time` property.
- Moved previous behavior of `OnTimeChanged` into a new event, `OnClockChanged`.
- Updated the `IsEqual` methods of the inheriting clock classes to utilize the built-in `TimeOnly`
comparison methods, `IsHourEqual`, `IsMinuteEqual`, `IsSecondEqual`.
- Removed the `HandleSetterEvents` method and implementations.
- Reworked `BaseClock` to handle the `OnClockChanged` event for all inherting clocks by utilizing
the overriden `IsEqual` method.
- Reworked class constructors to assign their own `Time` component changed events.

## [1.0.2] - 2025-10-11

Implemented the following changes to the `TimeOnly` struct:

- Added static methods for generating struct from a single type of non-millisecond unit.
  - `TimeOnly.FromHours` creates a new `TimeOnly` where the time is equal to the specified number
  of hours elapsed since midnight.
  - `TimeOnly.FromMinutes` creates a new `TimeOnly` where the time is equal to the specified number
    of minutes elapsed since midnight.
  - `TimeOnly.FromSeconds` creates a new `TimeOnly` where the time is equal to the specified number
    of seconds elapsed since midnight.

Implemented the following changes to the `Duration` struct:

- Added static methods for generating struct from a single type of non-millisecond unit.
    - `Duration.FromDays` creates a new `Duration` where the timespan is equal to the specified number
      of days elapsed.
    - `Duration.FromHours` creates a new `Duration` where the timespan is equal to the specified number
      of hours elapsed.
    - `Duration.FromHours` creates a new `Duration` where the timespan is equal to the specified number
      of minutes elapsed.
    - `Duration.FromHours` creates a new `Duration` where the timespan is equal to the specified number
      of seconds elapsed.

## [1.0.1] - 2025-10-08

### Changes

Implemented the following fixes to the `TimeOnly` class:

- Fixed a mistake where the numerator and denominator of the `GetDayProgress` method were switched.
- Added missing unit tests for the `GetDayProgress` method.

Added the following properties to the `OnTimeChangedEventArgs` class:

- `PreviousTime`: A `TimeOnly` representing the time at the frame before the event was invoked.
- `PreviousTimeString`: A `string` representing the `PreviousTime` according to the default format of
clock that invoked the event.
- `DeltaTime`: A `Duration` representing the timespan between `Time` and `PreviousTime`.
- `DayProgress`: A float representing the proportion of the day completed relative to the start of the day, midnight.

## [1.0.0] - 2025-10-01

## First Release

The initial release of the Game Time package includes functionality for managing in game time via events,
evaluating specific time of day values, and evaluating specific time span values.

### Features

- **Clock:** manage in-game time and use events to communicate the time to other game components.
- **TimeOnly:** use this struct to evaluate and compare specific time of day values.
- **Duration**: use this struct to evaluate and compare ```TimeOnly``` structs and spans of time.