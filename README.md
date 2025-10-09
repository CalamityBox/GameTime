# Game Time

An extensible solution for managing in-game time of day, and time-based calculations.

## Clock

The central component for managing in-game time. The clock tracks the time via the ```TimeOnly``` struct,
and includes events to communicate the current time to other game components, such as ```OnHourChanged```
and ```OnSecondChanged```.

## TimeOnly

A ```struct``` that records the time and allows times to be compared, added to, and displayed.

## Duration

A ```struct``` that represents a span of time, such as 36 hours or -23 days. This can be used to determine the time
between ```TimeOnly``` values, add or subtract time from a ```TimeOnly```, record the length of tasks, and so on.

# AI non-usage

This library was written without the use of generative AI models.