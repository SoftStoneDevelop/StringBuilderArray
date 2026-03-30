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
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **7**         |      **35.207 μs** |  **1.02** |       **17.6 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 7         |       7.027 μs |  0.20 |       17.6 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 7         |       7.300 μs |  1.00 |       17.6 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 7         |       1.355 μs |  0.19 |       17.6 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **10**        |      **33.554 μs** |  **1.02** |      **23.46 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 10        |      10.028 μs |  0.30 |      23.46 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 10        |       7.623 μs |  1.00 |      23.46 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 10        |       1.701 μs |  0.22 |      23.46 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **50**        |      **31.637 μs** |  **1.02** |     **101.59 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 50        |      12.651 μs |  0.41 |     101.59 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 50        |      11.707 μs |  1.01 |     102.24 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 50        |       4.717 μs |  0.40 |     101.63 KB |        0.99 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **100**       |      **31.975 μs** |  **1.00** |     **199.24 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 100       |       8.155 μs |  0.26 |     199.24 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      12.808 μs |  1.01 |      199.9 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 100       |       8.166 μs |  0.64 |     199.62 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1000**      |     **622.783 μs** |  **1.01** |    **1957.05 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1000      |     582.523 μs |  0.94 |    1957.05 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     558.016 μs |  1.00 |    1957.05 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     554.259 μs |  1.00 |    1957.05 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **2500**      |   **1,502.066 μs** |  **1.02** |    **4886.74 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 2500      |   1,568.527 μs |  1.07 |    4886.74 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |   1,511.755 μs |  1.02 |    4886.74 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |   1,583.013 μs |  1.07 |    4886.74 KB |        1.00 |
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
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **100000**    |  **36,388.479 μs** |  **1.12** |  **195316.43 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 100000    |  40,719.257 μs |  1.25 |  195316.43 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |  36,097.599 μs |  1.12 |  195316.43 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |  42,669.377 μs |  1.32 |  195316.43 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **500000**    | **122,649.853 μs** |  **1.00** |  **976566.43 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 500000    | 147,244.900 μs |  1.20 |  976566.46 KB |        1.00 |
|                    |                |                |           |                |       |               |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 500000    | 122,473.371 μs |  1.00 |  976566.43 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 500000    | 145,881.462 μs |  1.19 |  976566.43 KB |        1.00 |
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
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **7**         |      **32.491 μs** |      **1.03** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 7         |      35.086 μs |      1.11 |       32.4 KB |        1.58 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 7         |      23.348 μs |      0.74 |      16.02 KB |        0.78 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 7         |       5.244 μs |      1.03 |      20.86 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 7         |       7.439 μs |      1.46 |       32.4 KB |        1.55 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 7         |       3.492 μs |      0.68 |      16.02 KB |        0.77 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **10**        |      **27.978 μs** |      **1.01** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 10        |      34.293 μs |      1.24 |       32.4 KB |        1.58 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 10        |      24.460 μs |      0.89 |      32.02 KB |        1.56 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 10        |       4.513 μs |      1.01 |      20.77 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 10        |       7.482 μs |      1.67 |       32.4 KB |        1.56 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 10        |       4.206 μs |      0.94 |      32.02 KB |        1.54 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **50**        |      **29.031 μs** |      **1.01** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 50        |      58.124 μs |      2.02 |     104.19 KB |        5.09 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 50        |      17.854 μs |      0.62 |     128.02 KB |        6.25 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 50        |       5.623 μs |      1.05 |      20.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 50        |      12.617 μs |      2.35 |     104.19 KB |        5.09 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 50        |       8.605 μs |      1.60 |      128.4 KB |        6.27 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **100**       |      **29.182 μs** |      **1.02** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 100       |      25.782 μs |      0.90 |     213.98 KB |       10.45 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 100       |      21.943 μs |      0.76 |     256.02 KB |       12.50 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 100       |       4.238 μs |      1.00 |      20.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      15.478 μs |      3.65 |     213.98 KB |       10.45 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      12.065 μs |      2.85 |     256.02 KB |       12.50 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1000**      |      **32.125 μs** |      **1.03** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1000      |     106.430 μs |      3.41 |    1977.89 KB |       96.56 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1000      |     299.669 μs |      9.60 |    2048.02 KB |       99.98 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |       4.457 μs |      1.01 |      20.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |      80.492 μs |     18.20 |    1977.89 KB |       96.56 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     288.582 μs |     65.25 |    2048.68 KB |      100.01 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **2500**      |      **28.407 μs** |      **1.01** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 2500      |     328.924 μs |     11.68 |    4916.75 KB |      240.02 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 2500      |   1,139.790 μs |     40.48 |    8192.02 KB |      399.92 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |       4.467 μs |      1.01 |      20.86 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |     339.591 μs |     76.67 |    4916.75 KB |      235.71 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |   1,109.438 μs |    250.47 |    8192.02 KB |      392.73 |
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
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **100000**    |      **29.165 μs** |      **1.01** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 100000    |  27,692.434 μs |    959.33 |  195492.95 KB |    9,543.52 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 100000    |  31,988.820 μs |  1,108.16 |  262144.02 KB |   12,797.27 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |       4.468 μs |      1.01 |      20.53 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |  26,839.153 μs |  6,054.40 |  195493.72 KB |    9,521.76 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |  31,571.277 μs |  7,121.88 |  262144.07 KB |   12,768.05 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **500000**    |      **27.448 μs** |      **1.01** |      **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 500000    |  91,899.048 μs |  3,385.58 |  976759.31 KB |   47,683.14 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 500000    | 138,593.583 μs |  5,105.82 | 1048576.02 KB |   51,189.07 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 500000    |       5.170 μs |      1.04 |      20.53 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 500000    |  96,018.619 μs | 19,240.48 |  976760.53 KB |   47,574.33 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 500000    | 140,312.614 μs | 28,116.24 |  1048576.4 KB |   51,072.21 |
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
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **7**         |      **41.704 μs** |      **1.04** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 7         |      43.363 μs |      1.08 |      53.52 KB |        1.31 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 7         |      27.282 μs |      0.68 |      16.02 KB |        0.39 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 7         |       9.843 μs |      1.01 |      40.82 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 7         |      13.203 μs |      1.36 |      53.52 KB |        1.31 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 7         |       6.746 μs |      0.69 |      16.02 KB |        0.39 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **10**        |      **40.319 μs** |      **1.04** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 10        |      42.668 μs |      1.10 |      60.55 KB |        1.48 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 10        |      27.611 μs |      0.71 |      32.02 KB |        0.78 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 10        |       9.656 μs |      1.01 |      40.82 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 10        |      12.745 μs |      1.33 |      60.55 KB |        1.48 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 10        |       7.225 μs |      0.75 |      32.07 KB |        0.79 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **50**        |      **38.560 μs** |      **1.02** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 50        |      57.250 μs |      1.52 |     207.34 KB |        5.08 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 50        |      25.116 μs |      0.67 |     128.02 KB |        3.14 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 50        |       9.740 μs |      1.01 |      40.82 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 50        |      15.408 μs |      1.60 |     207.38 KB |        5.08 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 50        |      13.312 μs |      1.38 |      128.4 KB |        3.15 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **100**       |      **40.744 μs** |      **1.04** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 100       |      33.287 μs |      0.85 |      426.5 KB |       10.45 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 100       |      33.555 μs |      0.85 |     256.02 KB |        6.27 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 100       |       9.048 μs |      1.00 |      40.87 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      21.942 μs |      2.43 |      426.5 KB |       10.44 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      21.439 μs |      2.37 |     256.07 KB |        6.27 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1000**      |      **38.334 μs** |      **1.02** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1000      |     462.959 μs |     12.33 |    3946.66 KB |       96.68 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1000      |     471.047 μs |     12.55 |    2048.02 KB |       50.17 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |       9.976 μs |      1.01 |      41.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     408.669 μs |     41.44 |    3947.32 KB |       95.17 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     394.807 μs |     40.03 |    2048.02 KB |       49.38 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **2500**      |      **46.008 μs** |      **1.05** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 2500      |   1,193.708 μs |     27.35 |     9811.3 KB |      240.35 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 2500      |   1,776.668 μs |     40.71 |    8192.02 KB |      200.68 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |      10.840 μs |      1.03 |       41.2 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |   1,150.996 μs |    109.18 |     9811.3 KB |      238.17 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |   1,696.873 μs |    160.97 |     8192.4 KB |      198.87 |
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
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **100000**    |      **42.654 μs** |      **1.05** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 100000    |  43,376.727 μs |  1,068.08 |  390825.05 KB |    9,574.28 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 100000    |  63,910.329 μs |  1,573.69 |  262144.02 KB |    6,421.90 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |       9.453 μs |      1.00 |      40.82 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |  42,463.964 μs |  4,510.15 |  390826.34 KB |    9,574.31 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |  63,277.550 μs |  6,720.78 |   262144.4 KB |    6,421.91 |
|                                  |                |                |           |                |           |               |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **500000**    |      **37.744 μs** |      **1.04** |      **40.82 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 500000    | 173,603.250 μs |  4,770.29 | 1953485.94 KB |   47,855.73 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 500000    | 277,643.779 μs |  7,629.13 | 1048576.02 KB |   25,687.60 |
|                                  |                |                |           |                |           |               |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 500000    |       9.965 μs |      1.01 |      40.82 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 500000    | 172,229.347 μs | 17,466.81 | 1953487.34 KB |   47,855.77 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 500000    | 277,286.800 μs | 28,121.31 | 1048576.02 KB |   25,687.60 |
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
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **7**         |      **5.318 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 7         |      3.040 μs |     0.58 |       2.8 KB |        1.92 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 7         |      2.153 μs |     1.02 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 7         |      2.565 μs |     1.22 |       2.8 KB |        1.92 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **10**        |      **5.506 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 10        |      3.306 μs |     0.61 |       2.8 KB |        1.92 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 10        |      2.167 μs |     1.02 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 10        |      2.084 μs |     0.98 |       2.8 KB |        1.92 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **50**        |      **5.287 μs** |     **1.03** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 50        |     11.205 μs |     2.18 |     13.66 KB |        9.40 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 50        |      2.054 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 50        |      2.549 μs |     1.25 |     14.31 KB |        9.85 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **100**       |      **5.249 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 100       |     17.902 μs |     3.48 |     26.43 KB |       18.19 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      2.028 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      3.086 μs |     1.53 |     26.43 KB |       18.19 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1000**      |      **5.178 μs** |     **1.03** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1000      |     11.124 μs |     2.20 |    210.39 KB |      144.78 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |      2.048 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     13.151 μs |     6.47 |    210.77 KB |      145.04 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **2500**      |      **5.479 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 2500      |     52.944 μs |     9.85 |    505.53 KB |      347.89 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |      2.259 μs |     1.01 |      1.78 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 2500      |     31.054 μs |    13.95 |    505.91 KB |      284.02 |
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
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **100000**    |      **5.267 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 100000    |  4,281.993 μs |   827.39 |  20133.02 KB |   13,854.98 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |      1.904 μs |     1.00 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |  4,304.607 μs | 2,263.82 |  20133.47 KB |   13,855.29 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **500000**    |      **5.399 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 500000    | 10,447.390 μs | 1,965.77 | 100609.71 KB |   69,236.79 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 500000    |      2.116 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 500000    | 14,360.348 μs | 6,869.61 |  100610.3 KB |   69,237.19 |
|                    |                |                |           |               |          |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1071741**   |      **5.069 μs** |     **1.02** |      **1.45 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1071741   | 14,424.515 μs | 2,909.17 | 215628.59 KB |  148,389.57 |
|                    |                |                |           |               |          |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   |      2.073 μs |     1.01 |      1.45 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1071741   | 13,324.245 μs | 6,492.04 | 215629.18 KB |  148,389.97 |

</details>
