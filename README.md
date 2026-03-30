<h1 align="center">
  <a>StringBuilderArray</a>
</h1>

<h3 align="center">

  [![Nuget](https://img.shields.io/nuget/v/StringBuilderArray?logo=StringBuilderArray)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Downloads](https://img.shields.io/nuget/dt/StringBuilderArray.svg)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Stars](https://img.shields.io/github/stars/SoftStoneDevelop/StringBuilderArray?color=brightgreen)](https://github.com/SoftStoneDevelop/StringBuilderArray/stargazers)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

</h3>

Collect the resulting string without allocating memory.

## Benchmark:

<details>
  <summary>ToString</summary>
  
```C#

[IterationSetup]
public void Setup()
{
    _str = new string('S', StrLength);
    _sb = new System.Text.StringBuilder();
    for (int i = 0; i < 1000; i++)
    {
        _sb.AppendLine(_str);
    }

    _sbArr = new StringBuilderArray.StringBuilderArray();
    for (int i = 0; i < 1000; i++)
    {
        _sbArr.AppendLine(_str);
    }
}

[Benchmark(Baseline = true, Description = "StringBuilderArray")]
public void StringBuilderArray()
{
    _sbArr.ToString();
}

[Benchmark(Description = "StringBuilder")]
public void StringBuilder()
{
    _sb.ToString();
}

```

| Method             | Job            | Runtime        | StrLength | Mean           | Ratio | Allocated     | Alloc Ratio |
|------------------- |--------------- |--------------- |---------- |---------------:|------:|--------------:|------------:|
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **5**         |      **33.669 μs** |  **1.01** |       **13.7 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 5         |       7.276 μs |  0.22 |       13.7 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       7.282 μs |  1.00 |       13.7 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       1.231 μs |  0.17 |       13.7 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1000**      |     **622.783 μs** |  **1.01** |    **1957.05 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1000      |     582.523 μs |  0.94 |    1957.05 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     558.016 μs |  1.00 |    1957.05 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     554.259 μs |  1.00 |    1957.05 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **5000**      |   **3,029.181 μs** |  **1.01** |    **9769.55 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 5000      |   3,300.664 μs |  1.10 |    9769.55 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |   3,028.320 μs |  1.01 |    9769.55 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |   3,231.850 μs |  1.08 |    9769.55 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **10000**     |   **6,082.757 μs** |  **1.00** |   **19535.18 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 10000     |   6,634.693 μs |  1.09 |   19535.18 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |   5,988.090 μs |  1.00 |   19535.18 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |   6,478.808 μs |  1.08 |   19535.18 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1071741**   | **289,587.367 μs** |  **1.00** | **2093248.07 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1071741   | 318,024.700 μs |  1.10 | 2093248.07 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 292,666.936 μs |  1.00 | 2093248.07 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 320,468.233 μs |  1.10 | 2093248.07 KB |        1.00 |
</details>
<br>
<details>
  <summary>AppendLine</summary>
  
```C#

[IterationSetup]
public void Setup()
{
    _str = new string('S', StrLength);
}

[Benchmark(Baseline = true, Description = "StringBuilderArray")]
public void StringBuilderArray()
{
    var sb = new StringBuilderArray.StringBuilderArray();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLine(_str);
    }
}

[Benchmark(Description = "StringBuilder")]
public void StringBuilder()
{
    var sb = new System.Text.StringBuilder();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLine(_str);
    }
}

[Benchmark(Description = "DefaultInterpolatedStringHandler")]
public void DefaultInterpolatedStringHandler()
{
    var sb = new DefaultInterpolatedStringHandler();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLiteral(_str);
    }
}

```

| Method                           | Job            | Runtime        | StrLength | Mean           | Ratio     | Allocated     | Alloc Ratio |
|--------------------------------- |--------------- |--------------- |---------- |---------------:|----------:|--------------:|------------:|
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **5**         |      **28.829 μs** |      **1.01** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 5         |      28.224 μs |      0.99 |       16.7 KB |        0.82 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 5         |      22.851 μs |      0.80 |      16.02 KB |        0.78 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       4.460 μs |      1.01 |      20.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       7.596 μs |      1.72 |      17.36 KB |        0.85 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       4.743 μs |      1.07 |      16.02 KB |        0.78 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1000**      |      **32.125 μs** |      **1.03** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1000      |     106.430 μs |      3.41 |    1977.89 KB |       96.56 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1000      |     299.669 μs |      9.60 |    2048.02 KB |       99.98 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |       4.457 μs |      1.01 |      20.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |      80.492 μs |     18.20 |    1977.89 KB |       96.56 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     288.582 μs |     65.25 |    2048.68 KB |      100.01 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **5000**      |      **28.757 μs** |      **1.01** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 5000      |   1,595.614 μs |     56.29 |    9813.62 KB |      479.08 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 5000      |   2,611.600 μs |     92.13 |   16384.02 KB |      799.83 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |       4.217 μs |      1.00 |      20.81 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |   1,329.322 μs |    315.67 |    9813.62 KB |      471.53 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |   3,079.688 μs |    731.33 |    16384.4 KB |      787.24 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **10000**     |      **29.442 μs** |      **1.01** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 10000     |   2,762.829 μs |     94.57 |   19627.01 KB |      958.15 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 10000     |   3,986.533 μs |    136.46 |   32768.02 KB |    1,599.66 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |       4.487 μs |      1.01 |      20.81 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |   2,513.377 μs |    565.05 |   19627.01 KB |      943.04 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |   4,283.320 μs |    962.96 |    32768.4 KB |    1,574.46 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1071741**   |      **29.945 μs** |      **1.03** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1071741   | 123,382.045 μs |  4,229.27 | 2093438.98 KB |  102,196.87 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1071741   | 231,279.579 μs |  7,927.77 | 2097152.02 KB |  102,378.13 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   |       6.277 μs |      1.02 |      20.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 126,442.383 μs | 20,631.07 | 2093440.23 KB |  102,196.93 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 227,531.679 μs | 37,125.38 |  2097152.4 KB |  102,378.15 |
</details>
<br>
<details>
  <summary>AppendLine + Clear</summary>
  
```C#

[IterationSetup]
public void Setup()
{
    _str = new string('S', StrLength);
}

[Benchmark(Baseline = true, Description = "StringBuilderArray")]
public void StringBuilderArray()
{
    var sb = new StringBuilderArray.StringBuilderArray();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLine(_str);
    }

    sb.Clear();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLine(_str);
    }
}

[Benchmark(Description = "StringBuilder")]
public void StringBuilder()
{
    var sb = new System.Text.StringBuilder();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLine(_str);
    }

    sb.Clear();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLine(_str);
    }
}

[Benchmark(Description = "DefaultInterpolatedStringHandler")]
public void DefaultInterpolatedStringHandler()
{
    var sb = new DefaultInterpolatedStringHandler();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLiteral(_str);
    }

    sb.Clear();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLiteral(_str);
    }
}

```

| Method                           | Job            | Runtime        | StrLength | Mean           | Ratio     | Allocated     | Alloc Ratio |
|--------------------------------- |--------------- |--------------- |---------- |---------------:|----------:|--------------:|------------:|
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **5**         |      **43.771 μs** |      **1.03** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 5         |      39.806 μs |      0.94 |      32.73 KB |        0.80 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 5         |      27.016 μs |      0.63 |      16.02 KB |        0.39 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       9.252 μs |      1.00 |      40.82 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 5         |      12.936 μs |      1.40 |      32.73 KB |        0.80 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 5         |       7.050 μs |      0.76 |      16.02 KB |        0.39 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1000**      |      **38.334 μs** |      **1.02** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1000      |     462.959 μs |     12.33 |    3946.66 KB |       96.68 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1000      |     471.047 μs |     12.55 |    2048.02 KB |       50.17 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |       9.976 μs |      1.01 |      41.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     408.669 μs |     41.44 |    3947.32 KB |       95.17 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     394.807 μs |     40.03 |    2048.02 KB |       49.38 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **5000**      |      **44.691 μs** |      **1.05** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 5000      |   3,305.321 μs |     77.54 |   19583.17 KB |      479.74 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 5000      |   4,304.716 μs |    100.98 |   16384.02 KB |      401.37 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |       9.493 μs |      1.01 |      40.82 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |   3,022.488 μs |    320.10 |   19583.83 KB |      479.76 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |   5,267.342 μs |    557.85 |   16384.68 KB |      401.39 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **10000**     |      **40.294 μs** |      **1.04** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 10000     |   5,777.839 μs |    148.69 |   39166.08 KB |      959.48 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 10000     |   8,032.577 μs |    206.72 |   32768.02 KB |      802.74 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |      11.144 μs |      1.03 |      41.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |   5,508.849 μs |    507.47 |   39166.08 KB |      944.29 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |   8,564.185 μs |    788.93 |   32768.02 KB |      790.04 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1071741**   |      **39.325 μs** |      **1.03** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1071741   | 233,685.746 μs |  6,141.00 | 4186788.48 KB |  102,566.30 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1071741   | 466,345.020 μs | 12,255.03 | 2097152.02 KB |   51,375.21 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   |      10.092 μs |      1.01 |      40.87 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 240,078.979 μs | 23,996.71 | 4186789.73 KB |  102,448.69 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 455,444.213 μs | 45,523.19 | 2097152.02 KB |   51,316.28 |
</details>
<br>
<details>
  <summary>Insert</summary>
  
```C#

[IterationSetup]
public void Setup()
{
    _str = new string('S', StrLength);
}

[Benchmark(Baseline = true, Description = "StringBuilderArray")]
public void StringBuilderArray()
{
    var sb = new StringBuilderArray.StringBuilderArray();
    for (int i = 0; i < 100; i++)
    {
        sb.Append(_str);
    }

    sb.Insert(0, _str);

    //in start
    sb.Insert(100, _str);

    //after 5 str from start
    sb.Insert(96, _str);
}

[Benchmark(Description = "StringBuilder")]
public void StringBuilder()
{
    var sb = new System.Text.StringBuilder();
    for (int i = 0; i < 100; i++)
    {
        sb.Append(_str);
    }

    //before 1 str from end
    sb.Insert(99 * _str.Length, _str);

    //in start
    sb.Insert(0, _str);

    //after 5 str from start
    sb.Insert(5 * _str.Length, _str);
}

```

| Method             | Job            | Runtime        | StrLength | Mean          | Ratio    | Allocated    | Alloc Ratio |
|------------------- |--------------- |--------------- |---------- |--------------:|---------:|-------------:|------------:|
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **5**         |      **5.328 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 5         |      3.128 μs |     0.60 |      1.73 KB |        1.19 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 5         |      2.038 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 5         |      1.795 μs |     0.89 |      1.73 KB |        1.19 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1000**      |      **5.178 μs** |     **1.03** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1000      |     11.124 μs |     2.20 |    210.39 KB |      144.78 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |      2.048 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     13.151 μs |     6.47 |    210.77 KB |      145.04 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **5000**      |      **5.640 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 5000      |     89.192 μs |    16.11 |   1022.36 KB |      703.56 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |      2.252 μs |     1.02 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 5000      |     45.768 μs |    20.70 |   1023.02 KB |      704.01 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **10000**     |      **5.249 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 10000     |     77.956 μs |    15.20 |   2019.03 KB |    1,389.44 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |      1.869 μs |     1.00 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |     72.492 μs |    38.84 |   2019.03 KB |    1,389.44 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1071741**   |      **5.069 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1071741   | 14,424.515 μs | 2,909.17 | 215628.59 KB |  148,389.57 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   |      2.073 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 13,324.245 μs | 6,492.04 | 215629.18 KB |  148,389.97 |

</details>
