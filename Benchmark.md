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

| Method             | Job            | Runtime        | StrLength | Mean          | Error         | StdDev         | Median        | Ratio | RatioSD | Allocated    | Alloc Ratio |
|------------------- |--------------- |--------------- |---------- |--------------:|--------------:|---------------:|--------------:|------:|--------:|-------------:|------------:|
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **100**       |     **26.243 μs** |     **0.5000 μs** |      **0.4433 μs** |     **26.050 μs** |  **1.00** |    **0.02** |    **199.24 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 100       |      7.625 μs |     0.1416 μs |      0.1390 μs |      7.650 μs |  0.29 |    0.01 |    199.24 KB |        1.00 |
|                    |                |                |           |               |               |                |               |       |         |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 100       |     10.238 μs |     0.2063 μs |      0.4983 μs |     10.200 μs |  1.00 |    0.07 |     199.9 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      7.412 μs |     0.1492 μs |      0.3983 μs |      7.600 μs |  0.73 |    0.05 |    199.29 KB |        1.00 |
|                    |                |                |           |               |               |                |               |       |         |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1000**      |    **649.375 μs** |    **25.3283 μs** |     **71.0232 μs** |    **615.400 μs** |  **1.01** |    **0.15** |   **1957.05 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1000      |    617.833 μs |    27.0701 μs |     79.8167 μs |    585.000 μs |  0.96 |    0.15 |   1957.05 KB |        1.00 |
|                    |                |                |           |               |               |                |               |       |         |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |    605.118 μs |    23.6142 μs |     64.6435 μs |    579.000 μs |  1.01 |    0.14 |   1957.43 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |    623.986 μs |    34.2037 μs |    100.3135 μs |    573.300 μs |  1.04 |    0.19 |   1957.43 KB |        1.00 |
|                    |                |                |           |               |               |                |               |       |         |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **10000**     |  **6,269.077 μs** |   **107.4912 μs** |    **132.0089 μs** |  **6,235.600 μs** |  **1.00** |    **0.03** |  **19535.18 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 10000     |  6,745.038 μs |    94.4811 μs |     78.8960 μs |  6,765.900 μs |  1.08 |    0.03 |  19535.18 KB |        1.00 |
|                    |                |                |           |               |               |                |               |       |         |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |  6,333.985 μs |    95.0184 μs |     79.3446 μs |  6,331.600 μs |  1.00 |    0.02 |  19535.23 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |  6,743.123 μs |    93.7938 μs |     78.3221 μs |  6,728.600 μs |  1.06 |    0.02 |  19535.18 KB |        1.00 |
|                    |                |                |           |               |               |                |               |       |         |              |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **100000**    | **36,991.816 μs** | **4,536.7692 μs** | **13,376.7673 μs** | **37,084.200 μs** |  **1.15** |    **0.60** | **195316.43 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 100000    | 43,104.633 μs | 5,045.5020 μs | 14,876.7775 μs | 29,825.150 μs |  1.34 |    0.68 | 195316.43 KB |        1.00 |
|                    |                |                |           |               |               |                |               |       |         |              |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 100000    | 36,667.143 μs | 4,421.4612 μs | 13,036.7792 μs | 36,614.150 μs |  1.14 |    0.59 | 195316.48 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 100000    | 43,404.288 μs | 4,890.3970 μs | 14,419.4471 μs | 44,659.250 μs |  1.35 |    0.67 | 195316.48 KB |        1.00 |

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
		sb.AppendLiteral(Environment.NewLine);
    }
}

```

| Method                           | Job            | Runtime        | StrLength | Mean          | Error         | StdDev        | Median        | Ratio    | RatioSD  | Gen0      | Gen1      | Gen2      | Allocated    | Alloc Ratio |
|--------------------------------- |--------------- |--------------- |---------- |--------------:|--------------:|--------------:|--------------:|---------:|---------:|----------:|----------:|----------:|-------------:|------------:|
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **100**       |     **28.359 μs** |     **0.8982 μs** |     **2.5186 μs** |     **28.900 μs** |     **1.01** |     **0.14** |         **-** |         **-** |         **-** |     **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 100       |     23.810 μs |     0.4865 μs |     1.3643 μs |     23.400 μs |     0.85 |     0.10 |         - |         - |         - |    213.98 KB |       10.45 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 100       |     26.079 μs |     0.5059 μs |     1.2505 μs |     25.900 μs |     0.93 |     0.10 |         - |         - |         - |    256.02 KB |       12.50 |
|                                  |                |                |           |               |               |               |               |          |          |           |           |           |              |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      4.838 μs |     0.1088 μs |     0.3070 μs |      4.800 μs |     1.00 |     0.09 |         - |         - |         - |     21.14 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 100       |     12.544 μs |     0.3960 μs |     1.0907 μs |     12.150 μs |     2.60 |     0.28 |         - |         - |         - |    214.35 KB |       10.14 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 100       |     11.008 μs |     0.1388 μs |     0.1084 μs |     11.000 μs |     2.28 |     0.14 |         - |         - |         - |    256.07 KB |       12.11 |
|                                  |                |                |           |               |               |               |               |          |          |           |           |           |              |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1000**      |     **30.324 μs** |     **1.6911 μs** |     **4.8247 μs** |     **29.900 μs** |     **1.03** |     **0.23** |         **-** |         **-** |         **-** |     **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1000      |     92.300 μs |     1.3518 μs |     1.3277 μs |     92.050 μs |     3.12 |     0.50 |         - |         - |         - |   1977.89 KB |       96.56 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1000      |    296.493 μs |     5.6953 μs |     5.0488 μs |    296.150 μs |    10.03 |     1.62 |         - |         - |         - |   2048.02 KB |       99.98 |
|                                  |                |                |           |               |               |               |               |          |          |           |           |           |              |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |      4.514 μs |     0.0916 μs |     0.1125 μs |      4.500 μs |     1.00 |     0.03 |         - |         - |         - |     21.09 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     72.052 μs |     1.4308 μs |     2.0972 μs |     71.200 μs |    15.97 |     0.60 |         - |         - |         - |   1978.55 KB |       93.80 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |    279.709 μs |     5.5184 μs |    10.4994 μs |    276.000 μs |    62.01 |     2.74 |         - |         - |         - |    2048.4 KB |       97.11 |
|                                  |                |                |           |               |               |               |               |          |          |           |           |           |              |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **10000**     |     **30.478 μs** |     **1.9075 μs** |     **5.5035 μs** |     **29.700 μs** |     **1.03** |     **0.27** |         **-** |         **-** |         **-** |     **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 10000     |  2,677.697 μs |    59.8558 μs |   154.5069 μs |  2,659.400 μs |    90.74 |    17.27 | 2000.0000 | 1000.0000 |         - |  19627.01 KB |      958.15 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 10000     |  3,900.968 μs |    77.1336 μs |   102.9711 μs |  3,921.200 μs |   132.20 |    24.21 |         - |         - |         - |  32768.02 KB |    1,599.66 |
|                                  |                |                |           |               |               |               |               |          |          |           |           |           |              |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |      6.123 μs |     0.1110 μs |     0.0927 μs |      6.100 μs |     1.00 |     0.02 |         - |         - |         - |     21.14 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |  2,502.418 μs |    52.9507 μs |   148.4793 μs |  2,475.400 μs |   408.77 |    24.85 | 2000.0000 | 1000.0000 |         - |  19627.05 KB |      928.40 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |  4,139.665 μs |    63.3416 μs |    52.8931 μs |  4,136.750 μs |   676.22 |    12.89 |         - |         - |         - |  32768.68 KB |    1,550.03 |
|                                  |                |                |           |               |               |               |               |          |          |           |           |           |              |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **100000**    |     **32.005 μs** |     **1.7038 μs** |     **4.8885 μs** |     **33.600 μs** |     **1.03** |     **0.24** |         **-** |         **-** |         **-** |     **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 100000    | 28,569.189 μs | 1,551.8123 μs | 4,575.5538 μs | 25,986.250 μs |   917.15 |   220.61 | 4000.0000 | 4000.0000 | 3000.0000 | 195492.71 KB |    9,543.50 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 100000    | 31,571.933 μs |   470.7139 μs |   440.3061 μs | 31,444.100 μs | 1,013.55 |   180.92 |         - |         - |         - | 262144.02 KB |   12,797.27 |
|                                  |                |                |           |               |               |               |               |          |          |           |           |           |              |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |      4.595 μs |     0.0935 μs |     0.1710 μs |      4.500 μs |     1.00 |     0.05 |         - |         - |         - |     21.09 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 100000    | 27,796.309 μs | 1,528.3536 μs | 4,506.3853 μs | 25,043.500 μs | 6,056.71 | 1,000.38 | 4000.0000 | 4000.0000 | 3000.0000 | 195494.09 KB |    9,267.87 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 100000    | 31,448.346 μs |   120.6086 μs |   100.7137 μs | 31,460.500 μs | 6,852.48 |   240.65 |         - |         - |         - |  262144.4 KB |   12,427.59 |

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
		sb.AppendLiteral(Environment.NewLine);
    }

    sb.Clear();
    for (int i = 0; i < 1000; i++)
    {
        sb.AppendLiteral(_str);
		sb.AppendLiteral(Environment.NewLine);
    }
}

```

| Method                           | Job            | Runtime        | StrLength | Mean          | Error         | StdDev        | Median        | Ratio    | RatioSD | Gen0      | Gen1      | Gen2      | Allocated    | Alloc Ratio |
|--------------------------------- |--------------- |--------------- |---------- |--------------:|--------------:|--------------:|--------------:|---------:|--------:|----------:|----------:|----------:|-------------:|------------:|
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **100**       |     **32.956 μs** |     **1.2630 μs** |     **3.5828 μs** |     **33.300 μs** |     **1.01** |    **0.16** |         **-** |         **-** |         **-** |     **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 100       |     31.468 μs |     0.6021 μs |     1.0857 μs |     31.300 μs |     0.97 |    0.12 |         - |         - |         - |     426.5 KB |       20.82 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 100       |     38.065 μs |     0.7575 μs |     1.4952 μs |     37.800 μs |     1.17 |    0.14 |         - |         - |         - |    256.02 KB |       12.50 |
|                                  |                |                |           |               |               |               |               |          |         |           |           |           |              |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 100       |      8.983 μs |     0.0499 μs |     0.0389 μs |      9.000 μs |     1.00 |    0.01 |         - |         - |         - |     20.86 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 100       |     19.708 μs |     0.3951 μs |     0.8160 μs |     19.400 μs |     2.19 |    0.09 |         - |         - |         - |    426.88 KB |       20.46 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 100       |     20.721 μs |     0.2907 μs |     0.2577 μs |     20.650 μs |     2.31 |    0.03 |         - |         - |         - |     256.4 KB |       12.29 |
|                                  |                |                |           |               |               |               |               |          |         |           |           |           |              |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **1000**      |     **32.650 μs** |     **0.9519 μs** |     **2.6847 μs** |     **33.550 μs** |     **1.01** |    **0.12** |         **-** |         **-** |         **-** |     **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 1000      |    345.511 μs |     6.6559 μs |     7.1217 μs |    343.700 μs |    10.66 |    1.00 |         - |         - |         - |   3946.66 KB |      192.67 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 1000      |    381.535 μs |     6.8739 μs |     7.0589 μs |    379.800 μs |    11.77 |    1.09 |         - |         - |         - |   2048.02 KB |       99.98 |
|                                  |                |                |           |               |               |               |               |          |         |           |           |           |              |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |      9.057 μs |     0.0853 μs |     0.0756 μs |      9.100 μs |     1.00 |    0.01 |         - |         - |         - |     20.48 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |    329.964 μs |     3.9146 μs |     3.4702 μs |    329.300 μs |    36.43 |    0.47 |         - |         - |         - |   3947.32 KB |      192.70 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |    349.419 μs |     6.8280 μs |     8.1283 μs |    345.600 μs |    38.58 |    0.93 |         - |         - |         - |   2048.02 KB |       99.98 |
|                                  |                |                |           |               |               |               |               |          |         |           |           |           |              |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **10000**     |     **34.751 μs** |     **1.9408 μs** |     **5.5373 μs** |     **33.950 μs** |     **1.03** |    **0.23** |         **-** |         **-** |         **-** |     **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 10000     |  5,194.462 μs |    85.1324 μs |    83.6114 μs |  5,163.050 μs |   153.32 |   24.76 | 2000.0000 | 1000.0000 |         - |  39166.08 KB |    1,912.00 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 10000     |  7,797.529 μs |   148.6160 μs |   131.7442 μs |  7,749.900 μs |   230.15 |   37.19 |         - |         - |         - |  32768.02 KB |    1,599.66 |
|                                  |                |                |           |               |               |               |               |          |         |           |           |           |              |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |      8.869 μs |     0.0899 μs |     0.0751 μs |      8.900 μs |     1.00 |    0.01 |         - |         - |         - |     21.14 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |  5,062.354 μs |    94.4336 μs |    78.8564 μs |  5,058.300 μs |   570.81 |    9.74 | 2000.0000 | 1000.0000 |         - |  39166.13 KB |    1,852.65 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |  8,273.727 μs |    94.1430 μs |    88.0614 μs |  8,281.400 μs |   932.92 |   12.24 |         - |         - |         - |  32768.07 KB |    1,550.00 |
|                                  |                |                |           |               |               |               |               |          |         |           |           |           |              |             |
| **StringBuilderArray**               | **.NET 10.0**      | **.NET 10.0**      | **100000**    |     **35.122 μs** |     **1.8979 μs** |     **5.5960 μs** |     **34.050 μs** |     **1.03** |    **0.23** |         **-** |         **-** |         **-** |     **20.48 KB** |        **1.00** |
| StringBuilder                    | .NET 10.0      | .NET 10.0      | 100000    | 43,075.767 μs | 1,592.8508 μs | 4,696.5569 μs | 40,008.700 μs | 1,258.13 |  245.75 | 4000.0000 | 4000.0000 | 3000.0000 | 390824.95 KB |   19,079.17 |
| DefaultInterpolatedStringHandler | .NET 10.0      | .NET 10.0      | 100000    | 62,925.650 μs |   480.7037 μs |   426.1313 μs | 62,819.500 μs | 1,837.89 |  297.13 |         - |         - |         - | 262144.02 KB |   12,797.27 |
|                                  |                |                |           |               |               |               |               |          |         |           |           |           |              |             |
| StringBuilderArray               | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |      8.933 μs |     0.1804 μs |     0.3014 μs |      8.850 μs |     1.00 |    0.05 |         - |         - |         - |     21.14 KB |        1.00 |
| StringBuilder                    | NativeAOT 10.0 | NativeAOT 10.0 | 100000    | 38,928.962 μs |   718.0704 μs |   599.6213 μs | 38,617.400 μs | 4,362.37 |  154.32 | 4000.0000 | 4000.0000 | 3000.0000 | 390825.94 KB |   18,486.96 |
| DefaultInterpolatedStringHandler | NativeAOT 10.0 | NativeAOT 10.0 | 100000    | 63,399.427 μs |   627.7553 μs |   587.2027 μs | 63,053.000 μs | 7,104.53 |  236.89 |         - |         - |         - |  262144.4 KB |   12,400.03 |

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

    sb.InsertByIndex(99 * _str.Length, _str, StringBuilderArray.IndexDirection.FromStart);

    //in start
    sb.InsertByIndex(0, _str, StringBuilderArray.IndexDirection.FromStart);

    //after 5 str from start
    sb.InsertByIndex(5 * _str.Length, _str, StringBuilderArray.IndexDirection.FromStart);
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

| Method             | Job            | Runtime        | StrLength | Mean         | Error      | StdDev      | Median       | Ratio    | RatioSD | Gen0      | Gen1      | Gen2      | Allocated   | Alloc Ratio |
|------------------- |--------------- |--------------- |---------- |-------------:|-----------:|------------:|-------------:|---------:|--------:|----------:|----------:|----------:|------------:|------------:|
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **100**       |     **3.581 μs** |  **0.1370 μs** |   **0.3997 μs** |     **3.450 μs** |     **1.01** |    **0.16** |         **-** |         **-** |         **-** |     **2.28 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 100       |    13.872 μs |  1.8805 μs |   5.4558 μs |    16.400 μs |     3.92 |    1.60 |         - |         - |         - |    26.43 KB |       11.59 |
|                    |                |                |           |              |            |             |              |          |         |           |           |           |             |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 100       |     1.659 μs |  0.0671 μs |   0.1957 μs |     1.600 μs |     1.01 |    0.17 |         - |         - |         - |     2.28 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 100       |     2.409 μs |  0.0986 μs |   0.2733 μs |     2.300 μs |     1.47 |    0.24 |         - |         - |         - |    26.76 KB |       11.73 |
|                    |                |                |           |              |            |             |              |          |         |           |           |           |             |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **1000**      |     **3.362 μs** |  **0.0688 μs** |   **0.0941 μs** |     **3.400 μs** |     **1.00** |    **0.04** |         **-** |         **-** |         **-** |     **2.28 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 1000      |     9.860 μs |  0.1971 μs |   0.1844 μs |     9.900 μs |     2.94 |    0.10 |         - |         - |         - |   210.39 KB |       92.23 |
|                    |                |                |           |              |            |             |              |          |         |           |           |           |             |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     1.386 μs |  0.0296 μs |   0.0687 μs |     1.400 μs |     1.00 |    0.07 |         - |         - |         - |     2.28 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 1000      |     8.554 μs |  0.1719 μs |   0.3099 μs |     8.500 μs |     6.19 |    0.38 |         - |         - |         - |   210.44 KB |       92.25 |
|                    |                |                |           |              |            |             |              |          |         |           |           |           |             |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **10000**     |     **3.413 μs** |  **0.0696 μs** |   **0.1042 μs** |     **3.400 μs** |     **1.00** |    **0.04** |         **-** |         **-** |         **-** |     **2.28 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 10000     |    69.646 μs |  1.0829 μs |   0.9043 μs |    69.500 μs |    20.42 |    0.66 |         - |         - |         - |  2019.03 KB |      885.05 |
|                    |                |                |           |              |            |             |              |          |         |           |           |           |             |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |     1.401 μs |  0.0327 μs |   0.0927 μs |     1.400 μs |     1.00 |    0.09 |         - |         - |         - |     2.28 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 10000     |    68.360 μs |  0.7078 μs |   0.6620 μs |    68.400 μs |    49.00 |    3.27 |         - |         - |         - |  2019.08 KB |      885.08 |
|                    |                |                |           |              |            |             |              |          |         |           |           |           |             |             |
| **StringBuilderArray** | **.NET 10.0**      | **.NET 10.0**      | **100000**    |     **3.572 μs** |  **0.0731 μs** |   **0.1409 μs** |     **3.500 μs** |     **1.00** |    **0.05** |         **-** |         **-** |         **-** |     **2.28 KB** |        **1.00** |
| StringBuilder      | .NET 10.0      | .NET 10.0      | 100000    | 4,388.211 μs | 89.3243 μs | 260.5633 μs | 4,425.050 μs | 1,230.42 |   86.65 | 2000.0000 | 2000.0000 | 2000.0000 | 20133.02 KB |    8,825.43 |
|                    |                |                |           |              |            |             |              |          |         |           |           |           |             |             |
| StringBuilderArray | NativeAOT 10.0 | NativeAOT 10.0 | 100000    |     1.400 μs |  0.0000 μs |   0.0000 μs |     1.400 μs |     1.00 |    0.00 |         - |         - |         - |     2.28 KB |        1.00 |
| StringBuilder      | NativeAOT 10.0 | NativeAOT 10.0 | 100000    | 4,302.159 μs | 85.9752 μs | 161.4822 μs | 4,273.700 μs | 3,072.97 |  114.13 | 2000.0000 | 2000.0000 | 2000.0000 | 20134.13 KB |    8,825.92 |

</details>
