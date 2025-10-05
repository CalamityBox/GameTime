# Documentation

## Installation

This package can be installed creating a new folder in your Unity Packages directory and running

```
git clone https://github.com/CalamityBox/GameTime.git
```
from your terminal in the empty folder. To verify installation, open the Unity **Tests** window and
run all available unit tests.

## Clock

The ```Clock``` is the primary component that uses a ```TimeOnly``` struct to track and manage in-game time and 
communicate it to other game components. Often a game needs a clock of only one format, such as a clock that 
displays times as "8 AM" and invokes events only when the hour changes, or a clock that displays as "8:00 AM" 
and invokes events whenever the hour or minute changes. Therefore, the ```Clock``` is arranged into several 
classes of varying levels of time specificity that are created with sensible defaults.

### Constructor

The ```BaseClock``` is an abstract class and thus cannot be initialized. However, every inheriting clock has
the same constructor which accepts a ```TimeOnly``` starting time and an optional ```string``` parameter to
set the default format of the clock.

### Events

#### OnTimeChanged

The ```OnTimeChanged``` event invokes when the ```Time``` property changes at the level of specificity set
by the inheriting class. For example, ```HourClock.OnTimeChanged``` event will only invoke when the ```Hour```
component of the ```Time``` property has changed. Behind the scenes, every ```Clock``` still stores the full
precision time down to the millisecond.

Every time changed event

- ```OnHourChanged```
- ```OnMinuteChanged```
- ```OnSecondChanged```
- ```OnMillisecondChanged```

Inherits from the ```OnTimeChangedEventArgs``` class and can access the same properties which includes
the following:

- **Time -** ```TimeOnly``` The time recorded by the clock when the event was invoked.
- **TimeString -** ```string``` The formatted string of ```Time``` using the clock's ```Format``` property. This is
useful because the ```TimeOnly``` struct cannot track a default format, so displaying the time would require
clunky reference to the clock. For example:

```csharp
public class TimeManager : ISingleton
{
    public MinuteClock Clock;
}

public class MyGameObject
{
    private TextMeshProUGui _timeDisplay;
    
    private void Start() 
    {
        TimeManager.Instance.Clock.OnTimeChanged += TimeManager_OnTimeChanged
            _timeDisplay = GetComponent<TextMeshProUGui>();
    }
    
    private void TimeManager_OnTimeChanged(object sender, BaseClock.OnTimeChangedEventArgs e)
    {
        // Without using TimeString event property
        _timeDisplay.text = e.Time.ToString(TimeManager.Instance.Clock.Format)
        
        // With TimeString property
        _timeDisplay.text = e.TimeString
    }
}
```

### Properties

#### Time

A ```TimeOnly``` that represents the current time recorded by the clock.

#### Format

A ```string``` that represents the default format of the clock when the ```ToString``` method is invoked
without a ```format``` provided or when it is called implicitly in a template string, such as

```csharp
// Create a new clock set to the time 9:45:03:601 AM
private TimeOnly _startTime = new(9, 45, 3, 601);
private MinuteClock _clock = new(_startTime); // Default MinuteClock time format is "h:mm tt"

Debug.Log($"{_clock}"); // Prints "9:45 AM" to the console
```

Every clock stores the full-precision time, and can therefore display the time in any format.

## HourClock

The ```HourClock``` is a clock whose specified level of precision is the ```Hour``` component of ```Time```. 
An example of where this class could be used is *Five Night's At Freddy's*. The game builds tension by only 
displaying the hour of the time because it is more stressful for the player to not know the exact amount of 
time remaining in the day.

### Events

#### OnTimeChanged

The ```OnTimeChanged``` event of an ```HourClock``` will only invoke when the ```Hour``` component of
```Time``` has changed.

#### OnHourChanged

The ```OnHourChanged``` event will invoke whenever the ```Hour``` component of ```Time``` has changed. The
```HourClock``` is unique in that the ```OnHourChanged``` event is functionally equivalent to the
```OnTimeChanged``` event.

## MinuteClock

The ```MinuteClock``` is a clock whose specified level of precision is the ```Hour``` and ```Minute``` 
components of ```Time```. An example of where this class could be used is *Stardew Valley*. The time
displayed to the user only shows the hour and minute, and NPC scheduled activities update on hour
and minute level precision.

### Events

#### OnTimeChanged

The ```OnTimeChanged``` event of a ```MinuteClock``` will invoke when the ```Hour``` or ```Minute``` components
of ```Time``` have changed.

#### OnHourChanged

The ```MinuteClock``` has access to the ```OnHourChanged``` event of the ```HourClock```.

#### OnMinuteChanged

The ```OnMinuteChanged``` event will invoke whenever the ```Minute``` component of ```Time``` has changed.

## SecondClock

The ```SecondClock``` is a clock whose specified level of precision is the ```Hour```, ```Minute```,
and ```Second``` components of ```Time```.

### Events

#### OnTimeChanged

The ```OnTimeChanged``` event of a ```SecondClock``` will invoke when the ```Hour```, ```Minute```, 
or ```Second``` components of ```Time``` have changed.

#### OnHourChanged

The ```SecondClock``` has access to the ```OnHourChanged``` event of the ```HourClock```.

#### OnMinuteChanged

The ```SecondClock``` has access to the ```OnMinuteChanged``` event of the ```OnMinuteChanged```.

### OnSecondChanged

The ```OnSecondChanged``` event will invoke whenever the ```Second``` component of ```Time``` has changed.

## MillisecondClock

The ```MillisecondClock``` is a clock whose specified level of precision is the ```Hour```, ```Minute```,
```Second```, and ```Millisecond``` components of ```Time```.

### Events

#### OnTimeChanged

The ```OnTimeChanged``` event of a ```SecondClock``` will invoke when the ```Hour```, ```Minute```,
```Second```, or ```Millisecond``` components of ```Time``` have changed. The ```Millisecond``` clock
is unique in that the ```OnTimeChanged``` event is equivalent to any change in the ```Time```
property.

#### OnHourChanged

The ```MillisecondClock``` has access to the ```OnHourChanged``` event of the ```HourClock```.

#### OnMinuteChanged

The ```MillisecondClock``` has access to the ```OnMinuteChanged``` event of the ```MinuteClock```.

### OnSecondChanged

The ```MillisecondClock``` has access to the ```OnSecondChanged``` event of the ```SecondClock```.

### OnMillisecondChanged

The ```OnMillisecondChanged``` event will invoke whenever the ```Millisecond``` component of ```Time``` has changed.

## TimeOnly

The ```TimeOnly``` struct represents a time as an amount of time elapsed since midnight, which can be accessed
and displayed in standard clock formats. The ```TimeOnly``` struct also supports common equality, comparison,
and arithmetic operations.

### Usage

#### Constructors

##### 1 ```TimeOnly(Duration duration)```

Converts a ```Duration``` into a ```TimeOnly```. The ```Day``` component of the ```Duration``` is dropped because
adding or subtracting multiples of ```OneDay``` to a ```TimeOnly``` is congruent to adding
```Zero```. Then, the remaining time in the ```Duration``` is treated as the time elapsed since midnight, if the
```Duration``` is positive, or as the time until midnight, if the ```Duration``` is negative.

For example:

```csharp
// The time is 01:00:00:000 AM because that is one hour since midnight
TimeOnly time = new(Duration.OneHour);
    
// The time is 11:00:00:000 PM because that is one hour until midnight
time = new(-1 * Duration.OneHour);
```

##### 2 ```TimeOnly(int hours, int minutes, int seconds, int milliseconds)```

Creates a new ```TimeOnly``` at the specified time. Note that the individual components
are not required to be bound by clock standards, such as the hour being between 0 and 23.
The parameters will be totaled and converted into simplest form on assignment. The input
can be thought of as the time elapsed since midnight. That being said, when creating a new
```TimeOnly``` to represent a specific time, such as 12:38 PM, the most logical and readable
way to define this time is by adhering to clock conventions.

Example:

```csharp
TimeOnly time = new(12, 38, 6, 52); // The time is 12:38:06:052 PM
```

##### 3 `TimeOnly(int hours, int minutes, int seconds = 0)`

Similar to the *Constructor 2* except there is no `milliseconds` argument and
the seconds` argument is optional with a default set to `0`. The allows
for more concise initializations.

Example:

```csharp
TimeOnly time = new(18, 9); // The time is 06:09:00:000 PM

time = new(7, 45, 16); // The time is 07:45:16:000 AM
```

##### `TimeOnly(float milliseconds)`

Creates a new `TimeOnly` where `milliseconds` is the total amount of time elapsed
since midnight. The float will be **truncated** to an integer, and then
converted to simplest form. That is to say, while more than one day's worth of
milliseconds can be passed to the `constructor`, excess days will be discarded.

**Note** the input `milliseconds` can also be negative, in which case it will be treated
as the time until midnight.

Example:

```csharp
TimeOnly time = new(86_399_999.87f); // The time is 11:59:59:999 PM
    
time = new(-1.04f); // The time is 11:59:59:999 PM
```

##### `TimeOnly(int milliseconds)`

Creates a new `TimeOnly` where `milliseconds` is the time elapsed since midnight,
or the time until midnight if `milliseconds` is negative.

##### `TimeOnly(long milliseconds)`

Creates a new `TimeOnly` where `milliseconds` is the time elapsed since midnight,
or the time until midnight if `milliseconds` is negative.

#### Equality

Two `TimeOnly` structs are equal if and only if their `BackingTime` properties are
equal. Since the `BackingTime` is bound between `0` and `86,399,999`, every time in
the range `[0, 86,399,999]` maps to a unique time representation of the `Hour`, `Minute`,
`Second`, and `Millisecond` components. Therefore, two `TimeOnly` structs are also equivalent
if and only if their `Hour`, `Minute`, `Second`, and `Millisecond` components are equal.

Examples:

```csharp
TimeOnly time1 = new(86_399_999); // The time is 11:59:59:999 PM
TimeOnly time2 = new(23, 59, 59, 999); // The time is 11:59:59:999 PM

time1 == time2; // true because the resultant backing times are equal

TimeOnly time3 = new(12, 1, 0, 0); // The time is 12:01:00:000 PM
TimeOnly time4 = new(12, 0, 0, 0); // Time time is 12:00:00:000 PM

time3 == time4; // false because the resultant backing times are not equal
```

#### Comparison

`TimeOnly` structs can be compared using the `<`, `<=`, `>`, and `>=` operators. A `TimeOnly` is
greater than another if and only if its `BackingTime` is greater than the other's. Without the concept
of a date, the comparison is only made within the same "day". Therefore, `11 PM` is always strictly
greater than midnight even though midnight could refer to the start of the "next" day. In this context,
midnight will be interpreted as the start of "today".

Examples

```csharp
TimeOnly time1 = new(12, 0); // The time is 12:00:00:000 PM
TimeOnly time2 = new(12, 1); // The time is 12:01:00:000 PM

time1 >= time1; // true because time1 == time1
time1 > time1; // false because time1.BackingTime is not greater than time2.BackingTime

time1 > time2; // false because time1.BackingTime < time2.BackingTime
time1 >= time2; // false
time1 < time2; // true
time1 <= time2; // true
```

#### Arithmetic

`TimeOnly` structs support some addition and subtraction operations. This allows for more complex
manipulation and comparison of both `TimeOnly` and `Duration` structs.

##### Addition

Two `TimeOnly` structs cannot be added together because there is not a consistent and reasonable
definition of, say, `12:00 PM + 3 AM`. Moreover, this is not a way people talk
about time in the real world, so this is not a useful operation to attempt to define.

However, a `TimeOnly` can be summed with a `Duration`. This is equivalent to asking, "if it
is 12 PM now, what time will it be in 3 hours and 15 minutes?" or "if it is 1 AM, what time
was it 9 hours ago?" which has a reasonable definition.

The result of adding a `Duration` to a `TimeOnly` is a `TimeOnly` where the total duration is added
to the `BackingTime` of the `TimeOnly`. This leads to reasonable and natural addition
operations on `TimeOnly` structs such as:

```csharp
TimeOnly time1 = new(12, 0); // The time is 12:00:00:000 PM

Debug.Log(time1 + Duration.Zero); // The time is 12:00:00:000 PM
Debug.Log(time1 + Duration.OneMinute); // The time is 12:01:00:000 PM
Debug.Log(time1 + Duration.OneHour); // The time is 1:00:00:000 PM
Debug.Log(time1 + Duration.OneDay); // The time is 12:00:00:000 PM
Debug.Log(time1 + 2 * Duration.OneHour + 30 * Duration.OneMinute); // The time is 2:30:00:000 PM
```

Note that if the time moves past midnight, it will wrap back around. In other words, `"11 PM + 3 hours
= 2 AM"`.

##### Subtraction

Unlike addition, two `TimeOnly` structs can be subtracted from each other, and the result is
a `Duration`. Subtraction can be thought of as the distance between two times. If the leftmost
time is greater, then their difference is "how much time until" the leftmost time. On the other hand,
if the rightmost time is greater, then their difference is "how long ago" was the rightmost time.

Examples illustrate this much more clearly:

```csharp
TimeOnly time1 = new(18, 30); // The time is 6:30:00:000 PM
TimeOnly time2 = new(15, 0); // The time is 3:00:00:000 PM

Duration duration = new(3, 30); // 3 hours and 30 minutes

// true because 6:30 PM is 3 hours and 30 minutes in the future from 3:00 PM
time1 - time2 == duration;

// true because 3:00 PM was 3 hours and 30 minutes ago from 6:30 PM
time2 - time1 == -1 * duration;
```

A `TimeOnly` may also have a `Duration` subtracted from it. The result is a `TimeOnly` that has
"moved in the past" by the magnitude of the `Duration`. In plain language, `"12 PM - 30 minutes
= 11:30 AM"`.

Note that if the time moves below midnight, it will wrap back around. In other words, `"1 AM - 5
hours = 8 PM"`.

### Properties

#### Hour

The hour of the time in simplest form. The hour is bound between `0` and `23`.

#### Minute

The minute of the time in simplest form. The minute is bound between `0` and `59`.

#### Second

The second of the time in simplest form. The second is bound between `0` and `59`.

#### Millisecond

The millisecond of the time in simplest form. The millisecond is bound between `0` and `999`.

#### BackingTime

The total number of milliseconds elapsed since midnight. The `BackingTime` is bound between `0`
and `86,399,999`.

### Public methods

#### IsBetween

Determines if this `TimeOnly` is between a lower and upper bound. The method will
always assume that the upper bound is greater than the lower bound. For example, 12 PM
is not between 1 PM and 2 PM. However, if the bounds are reversed such that 2 PM is the
lower bound and 1 PM is the upper bound, the method will assume that the upper bound is
1 PM the following day. Therefore, 12 PM is between 2 PM and 1 PM. 

This is a convention chosen to maximize the parallels between this library and natural 
language discussion of time. For example, if you are working a graveyard shift from 11 PM
to 7 AM, then midnight is considered to be between 11 PM and 7 AM.

The method returns `true` if this `TimeOnly` is between the lower and upper bounds. Returns
`false` otherwise.

```csharp
TimeOnly time = new(1, 0); // The time is 1 AM
TimeOnly bound1 = new(23, 0); // The time is 11 PM
TimeOnly bound2 = new(3, 0); // The time is 3 AM

Debug.Log(time.IsBetween(bound1, bound2)); // true because 1 AM is between 11 PM and 3 AM
Debug.Log(time.IsBetween(bound2, bound1)); // false because 1 AM is not between 3 AM and 11 PM
```

#### IsHourEqual

Determines if the `Hour` component of this `TimeOnly` is equal to
the `Hour` component of another `TimeOnly`.

```csharp
TimeOnly time = new(5, 8); // The time is 5:08 AM
TimeOnly other = new(5, 43, 1, 2); // The time is 5:43:01:002 AM

Debug.Log(time.IsHourEqual(other)); // true
```

#### IsMinuteEqual

Determines if the `Minute` component of this `TimeOnly` is equal to
the `Minute` component of another `TimeOnly`.

```csharp
TimeOnly time = new(0, 15); // The time is 12:15 AM
TimeOnly other = new(13, 15); // The time is 1:15 PM

Debug.Log(time.IsMinuteEqual(other)); // true
```

#### IsSecondEqual

Determines if the `Second` component of this `TimeOnly` is equal to
the `Second` component of another `TimeOnly`.

```csharp
TimeOnly time = new(18, 4, 55); // The time is 6:04:55 PM
TimeOnly other = new(3, 18, 55); // The time is 3:18:55 AM

Debug.Log(time.IsSecondEqual(other)); // true
```

#### IsMillisecondEqual

Determines if the `Millisecond` component of this `TimeOnly` is equal to
the `Millisecond` component of another `TimeOnly`.

```csharp
TimeOnly time = new(7, 0, 12, 490); // The time is 7:00:12:490 AM
TimeOnly other = new(23, 58, 43, 490); // The time is 11:58:43:490 PM

Debug.Log(time.IsMillisecondEqual(other)); // true
```

#### GetDayProgress

Computes the proportion of the day completed relative to midnight and returns it as a float.
**Note:** Because midnight is defined as zero milliseconds elapsed during the day, and thus
is equivalent to 0% of the day progressed, the `GetDayProgress` function is bound between
`[0,1)`. 

```csharp
TimeOnly time1 = new(0); // The time is 12:00:00:000 AM
TimeOnly time2 = new(12. 0); // The time is 12:00:00:000 PM
TimeOnly time3 = new(18, 0); // The time is 6:00:00:000 PM

Debug.Log(time1.GetDayProgress()); // 0f
Debug.Log(time2.GetDayProgress()); // 0.5f
Debug.Log(time3.GetDayProgress()); // 0.75f
```

#### IsMorning

Determines if it is morning. Returns `true` if the `Time` of this clock is **strictly** less 
than the `Time_12_00` constant, which corresponds to noon. Returns `false` otherwise. 

```csharp
TimeOnly time1 = new(0); // The time is 12 AM
TimeOnly time2 = new(11, 0); // The time is 10 AM
TimeOnly time3 = new(12, 0); // The time is 12 PN
TimeOnly time4 = new(13, 0); // The time is 1 PM

Debug.Log(time1.IsMorning()); // true
Debug.Log(time2.IsMorning()); // true
Debug.Log(time3.IsMorning()); // false
Debug.Log(time4.IsMorning()); // false
```

#### GetTimeSuffix

Retrieves the 12-hour time suffix. Returns `"AM"` if `Time.IsMorning()` returns `true`.
Returns `false` otherwise.

```csharp
TimeOnly time1 = new(6, 0); // The time is 6 AM
TimeOnly time2 = new(12, 0); // The time is 12 PM

Debug.Log(time1.GetTimeSuffix()); // "AM"
Debug.Log(time2.GetTimeSuffix()); // "PM"
```

### Formatting

The `TimeOnly` struct supports formatting options similar to that of the dotnet `DateTime` and
`TimeOnly` structs.

#### Options

The following format codes are supported:

| Format Code | Value                                                     |
|:-----------:|:----------------------------------------------------------|
| `HH`        | The `Hour` component using a 24-hour format from 00 to 23 |
| `H`         | The `Hour` component using a 24-hour format from 0 to 23  |
| `hh`        | The `Hour` component using a 12-hour format from 00 to 12 |
| `h`         | The `Hour` component using a 12-hour format from 0 to 12  |
| `mm`        | The `Minute` component from 00 to 59                      |
| `m`         | The `Minute` component from 0 to 59                       |
| `ss`        | The `Second` component from 00 to 59                      |
| `s`         | The `Second` component from 0 to 59                       |
| `fff`       | The `Millisecond` component from 000 to 999               |
| `tt`        | The 12-hour time suffix, `"AM"` or `"PM"`                 |

#### Usage

##### Via the `ToString` method

Any `TimeOnly` struct can be formatted as a string by calling `myTimeOnly.ToString()`.
The **default** behavior of the `ToString` method invoked without a format parameter
is to use the format `"hh:mm:ss:fff tt"`, which displays time in the full 12-hour format,
such as `"06:00:00:000 PM"`.

A specific format can also be passed to the `ToString` method, such as
`myTimeOnly.ToString("H:mm")` which displays the time as `"18:00"`.

##### Via string templating

Any `TimeOnly` struct can be inserted into a template string and the `ToString` method will
be called implicitly. The same default value as `ToString` applies, thus usage such as
`$"{myTimeOnly}"` displays the time as `"06:00:00:000 PM"`. A specific format can also be
passed in the string template, such as `$"{myTimeOnly:H:mm}"` which displays the time as `"18:00"`.

### Constants

In games such as *Stardew Valley* or *Fields of Mistria*, events such as
NPC scheduled activities often take place in half-hour increments of the
time of day. Thus, to increase readability and reduce the chance of mistakes
when defining many times, such as an NPC's entire schedule for the day,
the `TimeOnly` library provides constants for every half-hour increment of the
day. They are as follows:

|       Constant        |         Value          |     Time     |
|:---------------------:|:----------------------:|:------------:|
| `TimeOnly.Time_0_00`  |   `new TimeOnly(0)`    | `"12:00 AM"` |
| `TimeOnly.Time_0_30`  | `new TimeOnly(0, 30)`  | `"12:30 AM"` |
| `TimeOnly.Time_1_00`  |  `new TimeOnly(1, 0)`  | `"1:00 AM"`  |
| `TimeOnly.Time_1_30`  | `new TimeOnly(1, 30)`  | `"1:30 AM"`  |
| `TimeOnly.Time_2_00`  |  `new TimeOnly(2, 0)`  | `"2:00 AM"`  |
| `TimeOnly.Time_2_30`  | `new TimeOnly(2, 30)`  | `"2:30 AM"`  |
| `TimeOnly.Time_3_00`  |  `new TimeOnly(3, 0)`  | `"3:00 AM"`  |
| `TimeOnly.Time_3_30`  | `new TimeOnly(3, 30)`  | `"3:30 AM"`  |
| `TimeOnly.Time_4_00`  |  `new TimeOnly(4, 0)`  | `"4:00 AM"`  |
| `TimeOnly.Time_4_30`  | `new TimeOnly(4, 30)`  | `"4:30 AM"`  |
| `TimeOnly.Time_5_00`  |  `new TimeOnly(5, 0)`  | `"5:00 AM"`  |
| `TimeOnly.Time_5_30`  | `new TimeOnly(5, 30)`  | `"5:30 AM"`  |
| `TimeOnly.Time_6_00`  |  `new TimeOnly(6, 0)`  | `"6:00 AM"`  |
| `TimeOnly.Time_6_30`  | `new TimeOnly(6, 30)`  | `"6:30 AM"`  |
| `TimeOnly.Time_7_00`  |  `new TimeOnly(7, 0)`  | `"7:00 AM"`  |
| `TimeOnly.Time_7_30`  | `new TimeOnly(7, 30)`  | `"7:30 AM"`  |
| `TimeOnly.Time_8_00`  |  `new TimeOnly(8, 0)`  | `"8:00 AM"`  |
| `TimeOnly.Time_8_30`  | `new TimeOnly(8, 30)`  | `"8:30 AM"`  |
| `TimeOnly.Time_9_00`  |  `new TimeOnly(9, 0)`  | `"9:00 AM"`  |
| `TimeOnly.Time_9_30`  | `new TimeOnly(9, 30)`  | `"9:30 AM"`  |
| `TimeOnly.Time_10_00` | `new TimeOnly(10, 0)`  | `"10:00 AM"` |
| `TimeOnly.Time_10_30` | `new TimeOnly(10, 30)` | `"10:30 AM"` |
| `TimeOnly.Time_11_00` | `new TimeOnly(11, 0)`  | `"11:00 AM"` |
| `TimeOnly.Time_11_30` | `new TimeOnly(11, 30)` | `"11:30 AM"` |
| `TimeOnly.Time_12_00` | `new TimeOnly(12, 0)`  | `"12:00 AM"` |
| `TimeOnly.Time_12_30` | `new TimeOnly(12, 30)` | `"12:30 AM"` |
| `TimeOnly.Time_13_00` | `new TimeOnly(13, 0)`  | `"1:00 PM"`  |
| `TimeOnly.Time_13_30` | `new TimeOnly(13, 30)` | `"1:30 PM"`  |
| `TimeOnly.Time_14_00` | `new TimeOnly(14, 0)`  | `"2:00 PM"`  |
| `TimeOnly.Time_14_30` | `new TimeOnly(14, 30)` | `"2:30 PM"`  |
| `TimeOnly.Time_15_00` | `new TimeOnly(15, 0)`  | `"3:00 PM"`  |
| `TimeOnly.Time_15_30` | `new TimeOnly(15, 30)` | `"3:30 PM"`  |
| `TimeOnly.Time_16_00` | `new TimeOnly(16, 0)`  | `"4:00 PM"`  |
| `TimeOnly.Time_16_30` | `new TimeOnly(16, 30)` | `"4:30 PM"`  |
| `TimeOnly.Time_17_00` | `new TimeOnly(17, 0)`  | `"5:00 PM"`  |
| `TimeOnly.Time_17_30` | `new TimeOnly(17, 30)` | `"5:30 PM"`  |
| `TimeOnly.Time_18_00` | `new TimeOnly(18, 0)`  | `"6:00 PM"`  |
| `TimeOnly.Time_18_30` | `new TimeOnly(18, 30)` | `"6:30 PM"`  |
| `TimeOnly.Time_19_00` | `new TimeOnly(19, 0)`  | `"7:00 PM"`  |
| `TimeOnly.Time_19_30` | `new TimeOnly(19, 30)` | `"7:30 PM"`  |
| `TimeOnly.Time_20_00` | `new TimeOnly(20, 0)`  | `"8:00 PM"`  |
| `TimeOnly.Time_20_30` | `new TimeOnly(20, 30)` | `"8:30 PM"`  |
| `TimeOnly.Time_21_00` | `new TimeOnly(21, 0)`  | `"9:00 PM"`  |
| `TimeOnly.Time_21_30` | `new TimeOnly(21, 30)` | `"9:30 PM"`  |
| `TimeOnly.Time_22_00` | `new TimeOnly(22, 0)`  | `"10:00 PM"` |
| `TimeOnly.Time_22_30` | `new TimeOnly(22, 30)` | `"10:30 PM"` |
| `TimeOnly.Time_23_00` | `new TimeOnly(23, 0)`  | `"11:00 PM"` |
| `TimeOnly.Time_23_30` | `new TimeOnly(23, 30)` | `"11:30 PM"` |

## Duration

The `Duration` struct is used to manage intervals of time, or timespans. Unlike
the `TimeOnly` struct, a `Duration` is not bound to one day. The max value of a
`Duration` is bound by `Int64.MaxValue`, which means that the `Duration` timespan
is bound between `[-Int64.MaxValue, Int64.MaxValue]` milliseconds where 
`Int64.MaxValue` is equal to `9,223,372,036,854,775,807`. In a more human-readable
format, this corresponds to a range of a little over $\pm$ 106,751,991,167 days.

### Constructors

#### 1 `Duration(TimeOnly timeOnly)`

Converts a `TimeOnly` struct into a `Duration`. The constructor will assume
that the `BackingTime` (time elapsed since midnight) of the `TimeOnly` is the 
timespan to use. For example, if the `TimeOnly` is equalivalent to `"2:30 AM"`,
then the timespan of the `Duration` will be equivalent to `2.5 hours`.

```csharp
TimeOnly time = new(2, 30); // The time is 2:30 AM
Duration duration = new(time); // The duration is 2.5 hours
```

#### 2 `Duration(int days, int hours, int minutes, int seconds, int milliseconds)`

Creates a new `Duration` struct equivalent to the sum of input timespan parameters. The
parameters will be converted to simplest form, relative to clock formatting, on assignment.

#### 3 `Duration(int hours, int minutes, int seconds = 0)`

Creates a new `Duration` struct equivalent to the sum of input timespan parameters. The
parameters will be converted to simplest form, relative to clock formatting, on assignment.

#### 4 `Duration(float milliseconds)`

Creates a new `Duration` struct where `milliseconds` is the total amount of time elapsed.
The `float` be **truncated** to a `long`.

#### 5 `Duration(int milliseconds)`

Creates a new `Duration` struct where `milliseconds` is the total amount of time elapsed.
The `int` be **parsed** to a `long`.

#### 6 `Duration(long milliseconds)`

Creates a new `Duration` struct where `milliseconds` is the total amount of time elapsed.

### Properties

#### Day

An `int` representing the number of days elapsed in simplest form.

#### Hour

An `int` representing the number of hours elapsed in simplest form, relative to a clock format.

#### Minute

An `int` representing the number of minutes elapsed in simplest form, relative to a clock format.

#### Second

An `int` representing the number of seconds elapsed in simplest form, relative to a clock format.

#### Millisecond

An `int` representing the number of milliseconds elapsed in simplest form, relative to a clock format.

#### BackingTime

A `long` representing the total number of milliseconds elapsed for the entire `Duration`.

### Public methods

#### IsBetween

Determines if this `Duration` is between a lower and upper `Duration`, inclusive.
**Note** that the comparison is performed on the literal value and not the absolute value. Thus,
a `Duration` of -2 hours is between -3 and -1 hours. However, it is not between 1 and 3
hours, even though the absolute value is 2 hours.

```csharp
Duration duration = new(1, 0); // The duration is 1 hour
Duration lower = new (0); // The duration is 0 time elapsed
Duration upper = new(2, 0); // The duration is 2 hours

Debug.Log(duration.IsBetween(lower, upper)); // true
```

**ArgumentException:** The `IsBetween` method is not defined when the lower bound is
greater than the upper bound.

#### FlattenDays

Calculates the total milliseconds elapsed excluding the `Day` component and returns it
as a `long`. In practice, this returns the equivalent number of milliseconds elapsed to 
a `TimeOnly` struct whose `Hour`, `Minute`, `Second`, and `Millisecond` components are 
equal to this `Duration`.

#### Abs

Converts a `Duration` into its absolute value. If the timespan is zero or positive, then
`myDuration` is equal to `myDuration.Abs()`. If the timespan is negative, then
`myDuration` is equal to `-1 * myDuration.Abs()`.

### Constants

Depending on the needs of your game, it may be common to perform operations on a `Duration`
or `TimeOnly` in standard intervals of a unit, such as one day or one hour. For example,
there may be a cutscene that should advance in-game time by three hours upon completion,
or a "time trials" race where the player must beat a time of 5 minutes. Therefore, the 
`Duration` struct provides the following constants:

|         Constant          |             Value             |
|:-------------------------:|:-----------------------------:|
|      `Duration.Zero`      |       `new Duration(0)`       |
| `Duration.OneMillisecond` |       `new Duration(1)`       |
|   `Duration.OneSecond`    |    `new Duration(0, 0, 1)`    |
|   `Duration.OneMinute`    |     `new Duration(0, 1)`      |
|    `Duration.OneHour`     |     `new Duration(1, 0)`      |
|     `Duration.OneDay`     | `new Duration(1, 0, 0, 0, 0)` |

#### Example

```csharp
public class TeamTrialsManager : Monobehavior 
{
    
    private Duration _passingTime = 5 * Duration.OneMinute;
    
    // ... rest of the class
    
    private void IsPassingTime(Duration playerTime)
    {
        return playerTime <= _passingTime;
    }
    
}
```