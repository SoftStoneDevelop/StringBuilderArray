<h1 align="center">
  <a>StringBuilderArray</a>
</h1>

<h3 align="center">

  [![Nuget](https://img.shields.io/nuget/v/Gedaq.Npgsql?logo=StringBuilderArray)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Downloads](https://img.shields.io/nuget/dt/StringBuilderArray.svg)](https://www.nuget.org/packages/StringBuilderArray/)
  [![Stars](https://img.shields.io/github/stars/SoftStoneDevelop/StringBuilderArray?color=brightgreen)](https://github.com/SoftStoneDevelop/StringBuilderArray/stargazers)
  [![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

</h3>

The version of StringBuilder built on an array of strings string[]: uses less memory.

Benchmark:

```C#

        [IterationSetup]
        public void Setup()
        {
            _str = new string('S', StrLength);
        }

        [Benchmark(Baseline = true, Description = "StringBuilder")]
        public void StringBuilder()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }

            var result = sb.ToString();
        }

        [Benchmark(Description = "StringBuilderArray")]
        public void StringBuilderArray()
        {
            var sb = new StringBuilderArray.StringBuilderArray();
            for (int i = 0; i < 1000; i++)
            {
                sb.AppendLine(_str);
            }

            var result = sb.ToString();
        }

```

|             Method |                  Job |              Runtime | StrLength |            Mean | Ratio |      Gen0 |      Gen1 |      Gen2 |     Allocated | Alloc Ratio |
|------------------- |--------------------- |--------------------- |---------- |----------------:|------:|----------:|----------:|----------:|--------------:|------------:|
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |         **5** |        **19.11 μs** |  **1.00** |         **-** |         **-** |         **-** |      **30.98 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |         5 |        31.31 μs |  1.62 |         - |         - |         - |       34.7 KB |        1.12 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |         5 |        24.59 μs |  1.00 |         - |         - |         - |      37.77 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |         5 |        14.66 μs |  0.60 |         - |         - |         - |      39.77 KB |        1.05 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |         **7** |        **20.76 μs** |  **1.00** |         **-** |         **-** |         **-** |      **50.59 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |         7 |        32.11 μs |  1.58 |         - |         - |         - |      38.61 KB |        0.76 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |         7 |        24.95 μs |  1.00 |         - |         - |         - |      57.35 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |         7 |        14.82 μs |  0.60 |         - |         - |         - |      43.68 KB |        0.76 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |        **10** |        **20.49 μs** |  **1.00** |         **-** |         **-** |         **-** |      **56.45 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |        10 |        31.46 μs |  1.49 |         - |         - |         - |      44.47 KB |        0.79 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |        10 |        25.54 μs |  1.00 |         - |         - |         - |      63.21 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |        10 |        15.08 μs |  0.59 |         - |         - |         - |      49.54 KB |        0.78 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |        **50** |        **31.72 μs** |  **1.00** |         **-** |         **-** |         **-** |     **206.36 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |        50 |        34.72 μs |  1.22 |         - |         - |         - |     122.59 KB |        0.59 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |        50 |        30.14 μs |  1.00 |         - |         - |         - |      208.5 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |        50 |        14.49 μs |  0.48 |         - |         - |         - |     127.64 KB |        0.61 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |       **100** |        **50.03 μs** |  **1.00** |         **-** |         **-** |         **-** |      **413.8 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |       100 |        38.52 μs |  0.95 |         - |         - |         - |     220.25 KB |        0.53 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |       100 |        46.52 μs |  1.00 |         - |         - |         - |     415.86 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |       100 |        24.81 μs |  0.57 |         - |         - |         - |      225.3 KB |        0.54 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |      **1000** |       **761.80 μs** |  **1.00** |         **-** |         **-** |         **-** |    **3935.53 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |      1000 |       527.09 μs |  0.71 |         - |         - |         - |    1978.06 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |      1000 |       722.85 μs |  1.00 |         - |         - |         - |    3940.05 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |      1000 |       538.31 μs |  0.77 |         - |         - |         - |    1983.11 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |      **2500** |     **1,901.71 μs** |  **1.00** |         **-** |         **-** |         **-** |    **9804.08 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |      2500 |     1,406.46 μs |  0.75 |         - |         - |         - |    4907.75 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |      2500 |     1,817.16 μs |  1.00 |         - |         - |         - |    9818.19 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |      2500 |     1,331.79 μs |  0.75 |         - |         - |         - |     4912.8 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |      **5000** |     **5,295.15 μs** |  **1.00** | **1000.0000** |         **-** |         **-** |   **19583.76 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |      5000 |     2,739.29 μs |  0.51 |         - |         - |         - |    9790.56 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |      5000 |     4,455.02 μs |  1.00 | 1000.0000 |         - |         - |   19608.41 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |      5000 |     2,670.66 μs |  0.59 |         - |         - |         - |    9795.61 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |     **10000** |     **9,537.17 μs** |  **1.00** | **2000.0000** | **1000.0000** |         **-** |   **39162.77 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |     10000 |     5,241.92 μs |  0.55 |         - |         - |         - |   19556.19 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |     10000 |    11,162.79 μs |  1.00 | 3000.0000 | 2000.0000 |         - |    39212.8 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |     10000 |     5,184.84 μs |  0.47 |         - |         - |         - |   19561.23 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |    **100000** |    **55,240.60 μs** |  **1.00** | **4000.0000** | **4000.0000** | **3000.0000** |  **390802.46 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |    100000 |    20,971.15 μs |  0.38 |         - |         - |         - |  195337.44 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |    100000 |   107,904.39 μs |  1.00 | 5000.0000 | 4000.0000 | 3000.0000 |  390850.55 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |    100000 |    49,812.44 μs |  0.46 |         - |         - |         - |  195342.48 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |    **500000** |   **212,051.93 μs** |  **1.00** | **5000.0000** | **4000.0000** | **4000.0000** | **1953310.77 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |    500000 |   104,972.60 μs |  0.50 |         - |         - |         - |  976587.44 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |    500000 |   525,755.95 μs |  1.00 | 6000.0000 | 5000.0000 | 4000.0000 | 1953358.61 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |    500000 |   247,554.09 μs |  0.47 |         - |         - |         - |  976592.48 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      **StringBuilder** |             **.NET 7.0** |             **.NET 7.0** |   **1071741** |   **288,194.84 μs** |  **1.00** | **5000.0000** | **4000.0000** | **4000.0000** |  **4186664.2 KB** |        **1.00** |
| StringBuilderArray |             .NET 7.0 |             .NET 7.0 |   1071741 |   152,733.12 μs |  0.53 |         - |         - |         - | 2093269.08 KB |        0.50 |
|                    |                      |                      |           |                 |       |           |           |           |               |             |
|      StringBuilder | .NET Framework 4.8.1 | .NET Framework 4.8.1 |   1071741 | 1,037,825.78 μs |  1.00 | 6000.0000 | 5000.0000 | 4000.0000 | 4186723.62 KB |        1.00 |
| StringBuilderArray | .NET Framework 4.8.1 | .NET Framework 4.8.1 |   1071741 |   495,930.11 μs |  0.48 |         - |         - |         - | 2093274.13 KB |        0.50 |
