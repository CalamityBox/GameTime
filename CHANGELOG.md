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